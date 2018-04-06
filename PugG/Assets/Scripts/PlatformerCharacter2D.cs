using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

namespace UnityStandardAssets._2D {
	public class PlatformerCharacter2D : MonoBehaviour {
		[SerializeField] private float m_RegMaxSpeed = 10f, m_MaxSpeed = 10f;   // The fastest the player can travel in the x axis.
		[SerializeField] private float m_PowerupSpeed = 20f;                // Horizontal speed when using a speed powerup
		[SerializeField] private float m_JumpForce = 400f;                  // Amount of force added when the player jumps.
		[Range(0, 1)] [SerializeField] private float m_CrouchSpeed = .36f;  // Amount of maxSpeed applied to crouching movement. 1 = 100%
		[SerializeField] private bool m_AirControl = false;                 // Whether or not a player can steer while jumping;
		[SerializeField] private LayerMask m_WhatIsGround;                  // A mask determining what is ground to the character

		// For handling level ending
		private Health PlayerHealth;
		private GameObject EndScreen;

		public Text countText;
		private Transform m_GroundCheck;    // A position marking where to check if the player is grounded.
		const float k_GroundedRadius = .2f; // Radius of the overlap circle to determine if grounded
		private bool m_Grounded;            // Whether or not the player is grounded.
		private Transform m_CeilingCheck;   // A position marking where to check for ceilings
		const float k_CeilingRadius = .01f; // Radius of the overlap circle to determine if the player can stand up
		private Animator m_Anim;            // Reference to the player's animator component.
		private Rigidbody2D m_Rigidbody2D;
		private bool m_FacingRight = false;  // For determining which way the player is currently facing.
		public Vector2 PushForce = Vector2.zero;	// Set by other objects to push the player in some direction
											 //private int count;



		private void Awake() {
			// Setting up references.
			m_GroundCheck = transform.Find("GroundCheck");
			m_CeilingCheck = transform.Find("CeilingCheck");
			m_Anim = GetComponent<Animator>();
			m_Rigidbody2D = GetComponent<Rigidbody2D>();

			// Level ending variables
			PlayerHealth = FindObjectOfType<Health>();
			EndScreen = GameObject.Find("End Screen Root").transform.Find("End Screen").gameObject;	// Only way to get an inactive object is to parent a Transform and use its Find() method
		}


		private void FixedUpdate() {
			m_Grounded = false;

			// The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
			// This can be done using layers instead but Sample Assets will not overwrite your project settings.
			Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);
			for(int i = 0; i < colliders.Length; i++) {
				if(colliders[i].gameObject != gameObject)
					m_Grounded = true;
			}
			m_Anim.SetBool("onGround", m_Grounded);

			if(gameObject.transform.position.y < -6) {
				Scene scene = SceneManager.GetActiveScene();
				SceneManager.LoadScene(scene.name);
			}

			// Set the vertical animation
			//m_Anim.SetFloat("vSpeed", m_Rigidbody2D.velocity.y);
		}


		public void Move(float move, bool crouch, bool jump) {

			if(m_Grounded || m_AirControl) {
				// Reduce the speed if crouching by the crouchSpeed multiplier
				move = (crouch ? move * m_CrouchSpeed : move);

				// The Speed animator parameter is set to the absolute value of the horizontal input.
				m_Anim.SetFloat("velocity", Mathf.Abs(move));

				// Move the character
				m_Rigidbody2D.velocity = new Vector2(move * m_MaxSpeed + PushForce.x, m_Rigidbody2D.velocity.y + PushForce.y);

				// Reduce PushForce
				PushForce = Vector2.Lerp(PushForce, Vector2.zero, 0.2f);

				// If the input is moving the player right and the player is facing left...
				if(move > 0 && !m_FacingRight) {
					// ... flip the player.
					Flip();
				}
				// Otherwise if the input is moving the player left and the player is facing right...
				else if(move < 0 && m_FacingRight) {
					// ... flip the player.
					Flip();
				}
			}
			// If the player should jump...
			if(m_Grounded && jump /*&& m_Anim.GetBool("Ground")*/) {
				// Add a vertical force to the player.
				m_Grounded = false;
				m_Anim.SetBool("onGround", false);
				m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce));
			}
		}


		private void Flip() {
			// Switch the way the player is labelled as facing.
			m_FacingRight = !m_FacingRight;

			// Multiply the player's x local scale by -1.
			Vector3 theScale = transform.localScale;
			theScale.x *= -1;
			transform.localScale = theScale;
		}

		private void OnTriggerEnter2D(Collider2D collision) {
			if(collision.gameObject.name == "Speed") {
				m_MaxSpeed = m_PowerupSpeed;
				collision.gameObject.SetActive(false);
				StartCoroutine("waitTime");
			} else if(collision.gameObject.tag == "Goal") {
				GetComponent<PlatformerCharacter2D>().enabled = false;
				GetComponent<Platformer2DUserControl>().enabled = false;
				m_Rigidbody2D.velocity = new Vector2(0f, 0f);
				m_Rigidbody2D.gravityScale = 0f;
				m_Anim.SetBool("onGround", false);
				m_Anim.SetFloat("velocity", 0f);
				PlayerHealth.invincible = true;
				//Instantiate(FakePlayer, pos);
				EndScreen.SetActive(true);
			}
		}

		IEnumerator waitTime() {
			yield return new WaitForSeconds(3);
			m_MaxSpeed = m_RegMaxSpeed;
		}
	}
}
