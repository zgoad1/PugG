using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Song2_Play : MonoBehaviour {

	// Use this for initialization
	void Start () {
		GameObject.Find("Audio Manager").GetComponent<AudioManager>().Play("Song2");
	}
}
