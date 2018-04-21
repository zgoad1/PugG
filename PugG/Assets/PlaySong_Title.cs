using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySong_Title : MonoBehaviour {

	// Use this for initialization
	void Start () {
		GameObject.Find("Audio Manager").GetComponent<AudioManager>().Play("Title theme");
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
