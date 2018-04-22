using UnityEngine;

public class EnemyAIGroundCheck : MonoBehaviour {

	private EnemyAI parent;

	public bool onGround = false;

	// Use this for initialization
	void Start () {
		parent = GetComponentInParent<EnemyAI>();
	}

	void OnCollisionEnter2D(Collision2D c) {
		//Debug.Log("AI Ground Check (Layer \"" + LayerMask.LayerToName(gameObject.layer) + "\"): Collided with " + c.gameObject + " on layer \"" + LayerMask.LayerToName(c.gameObject.layer) + "\"");
		if(c.gameObject.layer == LayerMask.NameToLayer("EnemyGround")) {
			onGround = true;
			parent.canMove = true;
		}
	}

	void OnCollisionExit2D(Collision2D c) {
		//Debug.Log("AI Ground Check (Layer \"" + LayerMask.LayerToName(gameObject.layer) + "\"): Left collision with " + c.gameObject + " on layer \"" + LayerMask.LayerToName(c.gameObject.layer) + "\"");
		if(c.gameObject.layer == LayerMask.NameToLayer("EnemyGround")) {
			onGround = false;
			// We're about to run off the edge, so stop moving
			parent.DontFall();
		}
	}
}
