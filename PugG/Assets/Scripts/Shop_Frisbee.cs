using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop_Frisbee : MonoBehaviour {
	private void OnTriggerEnter2D(Collider2D collision) {
		if(TempTracker.PP > 0) {
			TempTracker.PP--;
			TempTracker.FBUses += 3;
			FindObjectOfType<PlatformerCharacter2D>().UpdatePowerups();
			// play purchase sound effect
		} else {
			// play menacing sound effect
		}
	}
}
