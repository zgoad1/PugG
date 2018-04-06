using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Song1_Play : MonoBehaviour {

	private static int number = 0;
	private AudioManager am;
	private int index;

	// Use this for initialization
	void Start() {

		index = number;
		number++;

		if(index != 0) {
			Destroy(gameObject);
		} else {
			am = FindObjectOfType<AudioManager>();
			am.Play("Song1");
			am.FadeIn("Song1");

			DontDestroyOnLoad(gameObject);
		}
	}
}
