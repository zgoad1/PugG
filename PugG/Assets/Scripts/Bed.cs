﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Bed : MonoBehaviour {
	private void OnTriggerStay2D(Collider2D other) {
		if(other.tag == "Player") {
			if(Input.GetButtonDown("Interact")) {
				Debug.Log("Loading level #" + TempTracker.currentLevel);
				SceneManager.LoadScene("Level" + TempTracker.currentLevel);
			}
		}
	}
}
