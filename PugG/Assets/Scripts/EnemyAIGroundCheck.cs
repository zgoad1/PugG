using UnityEngine;

public class EnemyAIGroundCheck : MonoBehaviour {

	private EnemyAI parent;

	public bool onGround = false;

	// Use this for initialization
	void Start () {
		parent = GetComponentInParent<EnemyAI>();
	}

	void OnCollisionEnter2D(Collision2D c) {
		Debug.Log("AI Ground Check: Collided with " + c.gameObject + " on layer " + c.gameObject.layer);
		if(c.gameObject.layer == LayerMask.NameToLayer("Ground")) {
			onGround = true;
			parent.canMove = true;
		}
	}

	void OnCollisionExit2D(Collision2D c) {
		if(c.gameObject.layer == LayerMask.NameToLayer("Ground")) {
			onGround = false;
			// We're about to run off the edge, so stop moving
			parent.DontFall();
		}
	}
}
