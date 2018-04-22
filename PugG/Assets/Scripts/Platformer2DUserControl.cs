using System;
using System.Collections;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

//namespace UnityStandardAssets._2D
//{
[RequireComponent(typeof(PlatformerCharacter2D))]
public class Platformer2DUserControl : MonoBehaviour {
	private PlatformerCharacter2D m_Character;
	private bool m_Jump;
	private bool canOdor;
	private ParticleSystem[] parts;

	private void Awake() {
		m_Character = GetComponent<PlatformerCharacter2D>();
		parts = GetComponentsInChildren<ParticleSystem>();
		foreach(ParticleSystem p in parts) {
			p.gameObject.SetActive(false);
		}
	}


	private void Update() {
		if(!m_Jump) {
			// Read the jump input in Update so button presses aren't missed.
			m_Jump = CrossPlatformInputManager.GetButtonDown("Jump");
		}
		if(Input.GetButtonDown("Odor") && m_Character.canOdor) {
			StartCoroutine("OdorCooldown"); 
		}
	}

	private IEnumerator OdorCooldown() {
		foreach(ParticleSystem p in parts) {
			p.gameObject.SetActive(true);
			p.Play();
		}
		m_Character.canOdor = false;
		m_Character.odoring = true;
		yield return new WaitForSeconds(5);
		m_Character.odoring = false;
		yield return new WaitForSeconds(5);
		m_Character.canOdor = true;
		foreach(ParticleSystem p in parts) {
			p.gameObject.SetActive(false);
		}
	}

	private void FixedUpdate() {
		// Read the inputs.
		bool crouch = Input.GetKey(KeyCode.LeftControl);
		float h = CrossPlatformInputManager.GetAxis("Horizontal");
		// Pass all parameters to the character control script.
		m_Character.Move(h, crouch, m_Jump);
		m_Jump = false;
	}
}
//}
