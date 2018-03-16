using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour {

	private Rigidbody2D rb;
	private Vector3 motion = Vector3.zero;
	private float speed = 0.03f;
	private float inputH = 0f;
	private float inputV = 0f;
	private bool onGround = false;

	public float accel = 0.1f;		//public so it can be edited while testing
	public float decelAir = 0.02f;
	public float decelGround = 0.2f;
	public float gravity = -0.001f;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    // Physics updates
    void FixedUpdate() {
		// get horizontal input
		inputH = Input.GetAxisRaw("Horizontal");
		// if moving horizontally, apply acceleration
		if(inputH != 0f) {
			motion.x = Mathf.Lerp(motion.x, inputH * speed, accel);
		// else, apply deceleration, which is lower in air than on ground
		} else {
			if(onGround)
				motion.x = Mathf.Lerp(motion.x, 0f, decelGround);
			else
				motion.x = Mathf.Lerp(motion.x, 0f, decelAir);
		}

		motion.y += gravity;

		// Move player 
		transform.position += motion;
    }

	void OnCollisionEnter2D(Collision2D coll) {
		// use angle between self and collision point to determine whether hitting ground
		ContactPoint2D cPoint = coll.contacts[0];
		// this is currently bugged; ground collisions are detected if the player hits a wall on their left
		float angle = -Mathf.Atan2(transform.position.y - cPoint.point.y, transform.position.x - cPoint.point.x);
		if(angle >= -3 * Mathf.PI / 4 && angle <= Mathf.PI / 4) {
			motion.y = 0f;
			Debug.Log("Hit ground");
		}
		Debug.Log("Hit angle: " + angle);

	}

	// Every frame of collision
	void OnCollisionStay2D(Collision2D coll) {
		OnCollisionEnter2D(coll);
	}
}
