using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Song1_Play : MonoBehaviour {

	private void Start() {
		AudioManager am = FindObjectOfType<AudioManager>();
		am.Play("Song1");
		am.FadeIn("Song1");
	}
}
