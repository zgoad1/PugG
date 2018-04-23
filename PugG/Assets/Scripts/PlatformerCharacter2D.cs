using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

//namespace UnityStandardAssets._2D {
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
	public Vector2 PushForce = Vector2.zero;    // Set by other objects to push the player in some direction
	[SerializeField] private Text FrisbeeUses;
	[SerializeField] private Text TennisUses;
	[SerializeField] private Text PugPoints;
	public int reward = 0;				// Amount of Pug Points to award at the end of the level

	// Powerups
	private bool puAirJump = true;          // Whether the player has this powerup
	private bool puLongJump = true;
	private bool puOdor = true;
	private bool setCanAirJumpFalse = false;     // more complicated explanation than it's worth. bear with it
	private bool setCanAirJumpTrue = false;      // ^
	private bool caj = false;               // backing field for the below
	private bool canAirJump {               // Whether this powerup may be used
		get {
			return puAirJump && caj;
		}
		set {
			caj = value;
		}
	}
	private float longJumpVelocity = 20f;
	private bool co = true;
	public bool canOdor {
		get {
			return puOdor && co;
		}
		set {
			co = value;
		}
	}
	public bool odoring = false;

	public Vector3 respawnPoint;         // For determining where the player respawns after death.

	public enum PowerupType {
		DoubleJump = 0,
		LongJump = 1,
		Odor = 2
	}

	public void SetPowerup(PowerupType p, bool enable) {
		switch(p) {
			case PowerupType.DoubleJump:
				puAirJump = enable;
				break;
			case PowerupType.LongJump:
				puLongJump = enable;
				break;
			case PowerupType.Odor:
				puOdor = enable;
				break;
		}
	}

	public void UpdatePowerups() {
		// Get powerups from game controller
		SetPowerup(PowerupType.DoubleJump, TempTracker.TBUses > 0);
		SetPowerup(PowerupType.LongJump, TempTracker.FBUses > 0);
		SetPowerup(PowerupType.Odor, TempTracker.Odor);

		SetPowerupText();
	}

	private void Awake() {
		// Setting up references.
		m_GroundCheck = transform.Find("GroundCheck");
		m_CeilingCheck = transform.Find("CeilingCheck");
		m_Anim = GetComponent<Animator>();
		m_Rigidbody2D = GetComponent<Rigidbody2D>();
		respawnPoint = transform.position;

		// Level ending variables
		PlayerHealth = FindObjectOfType<Health>();
		EndScreen = GameObject.Find("End Screen Root").transform.Find("End Screen").gameObject; // Only way to get an inactive object is to parent a Transform and use its Find() method

		UpdatePowerups();
	}


	private void FixedUpdate() {
		m_Grounded = false;

		// The player is grounded if a circlecast to the groundcheck position hits anything designated as ground [and they're not going upward]
		// This can be done using layers instead but Sample Assets will not overwrite your project settings.
		Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);
		for(int i = 0; i < colliders.Length; i++) {
			if(colliders[i].gameObject != gameObject && m_Rigidbody2D.velocity.y <= 0.05f) {
				m_Grounded = true;
				if(!setCanAirJumpFalse) {
					canAirJump = false;
					setCanAirJumpFalse = true;
					setCanAirJumpTrue = false;
				}
				break;
			} else {
				// If we're not touching the ground
				setCanAirJumpFalse = false;
				if(!setCanAirJumpTrue) {
					canAirJump = true;
					setCanAirJumpTrue = true;
				}
			}
		}
		m_Anim.SetBool("onGround", m_Grounded);

		if(gameObject.transform.position.y < -6) {
			Die();
		}
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
			PushForce = Vector2.Lerp(PushForce, Vector2.zero, 0.1f);

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
		if((m_Grounded || canAirJump) && jump/*&& m_Anim.GetBool("Ground")*/) {
			// Add a vertical force to the player.
			m_Grounded = false;
			m_Anim.SetBool("onGround", false);
			if(Input.GetButton("LongJump") && !canAirJump) {
				if(TempTracker.FBUses > 0) {
					PushForce.x = longJumpVelocity;
					if(!m_FacingRight) PushForce.x *= -1;
				}
				TempTracker.FBUses--;
			}
			m_Rigidbody2D.velocity = new Vector2(m_Rigidbody2D.velocity.x, 0f);
			m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce));
			if(canAirJump) TempTracker.TBUses--;
			UpdatePowerups();
			canAirJump = !canAirJump;
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
			reward = PickupTracker.score / 35 + (int)FindObjectOfType<Timer>().timeLeft / 80;
			PugPoints.text = " + " + reward;
			TempTracker.PP += reward;
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

	void OnTriggerStay2D(Collider2D other) {
		if(other.tag == "Checkpoint") {
			if(Input.GetButtonDown("Interact")) {
				respawnPoint = other.transform.position;
				other.GetComponent<Checkpoint>().DoParticles();
				// sound effect
			}
		}
	}

	public void Die() {
		PickupTracker.score = (int)Mathf.Max(0f, PickupTracker.score - 10);     // subtract 10 from score
		PlayerHealth.currentHealth = Health.maxHealth;                          // set back to full health
		Restarter.Restart(true);                                                // go back to checkpoint
		PlayerHealth.TakeDamage(0);                                             // take 0 damage so invincibility starts
	}

	private void SetPowerupText() {
		if(PugPoints != null) {
			PugPoints.text = ": " + TempTracker.PP;
		}
		FrisbeeUses.text = ": " + TempTracker.FBUses;
		TennisUses.text = ": " + TempTracker.TBUses;
	}

	IEnumerator waitTime() {
		yield return new WaitForSeconds(3);
		m_MaxSpeed = m_RegMaxSpeed;
	}
}
//}
