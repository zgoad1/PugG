using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop_Frisbee : MonoBehaviour {

	private ParticleSystem parts;

	private void Start() {
		parts = GetComponentInChildren<ParticleSystem>();
		parts.gameObject.SetActive(false);
	}

	private void OnTriggerEnter2D(Collider2D collision) {
		if(TempTracker.PP > 0) {
			TempTracker.PP--;
			TempTracker.FBUses += 3;
			FindObjectOfType<PlatformerCharacter2D>().UpdatePowerups();
			parts.gameObject.SetActive(true);
			parts.Play();
			// play purchase sound effect
		} else {
			// play menacing sound effect
		}
	}
}
