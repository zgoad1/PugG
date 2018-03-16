using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour {
	
	public Transform player;

	public float lerpFactor = 0.15f;

	// Use this for initialization
	void Start () {

	}
	
	// Must move in FixedUpdate because player moves in FixedUpdate, otherwise there would be jitter
	void FixedUpdate () {
		Vector3 newPos = Vector3.Lerp(transform.position, player.position, lerpFactor);
		newPos.z = -10f;    // z always stays the same, or else the clipping planes make stuff disappear
		transform.position = newPos;
	}
}
