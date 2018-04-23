using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floaty : MonoBehaviour {

	private Vector3 newPos;
	private float iy;
	[SerializeField] private float amplitude = 0.1f;
	[SerializeField] private float period = 2f;
	private float phaseShift;

	// Use this for initialization
	void Start () {
		newPos = transform.position;
		iy = transform.position.y;
		phaseShift = Random.Range(0f, period);
	}
	
	// Update is called once per frame
	void Update () {
		newPos.y = iy + amplitude * Mathf.Sin(2f * Mathf.PI / period * Time.time + phaseShift);
		transform.position = newPos;
	}
}
