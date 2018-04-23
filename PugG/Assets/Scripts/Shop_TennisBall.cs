using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop_TennisBall : MonoBehaviour {
	private void OnTriggerEnter2D(Collider2D collision) {
		if(TempTracker.PP > 0) {
			TempTracker.PP--;
			TempTracker.TBUses += 3;
			FindObjectOfType<PlatformerCharacter2D>().UpdatePowerups();
			// play purchase sound effect
		} else {
			// play menacing sound effect
		}
	}
}
