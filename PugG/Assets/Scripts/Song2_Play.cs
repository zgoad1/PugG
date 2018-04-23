using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Song2_Play : MonoBehaviour {

	// Use this for initialization
	void Start () {
		AudioManager am = GameObject.Find("Audio Manager").GetComponent<AudioManager>();
		am.Play("Song2");
		am.FadeIn("Song2");
	}
}
