using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HiddenDuck : MonoBehaviour {

	[SerializeField] private Transform dialogue;
	[SerializeField] private Animator explosion;
	[SerializeField] private PlatformerCharacter2D player;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void OnTriggerStay2D(Collider2D collision) {
		if(Input.GetButtonDown("Interact")) {
			dialogue.gameObject.SetActive(true);
			StartCoroutine("Duck");
		}
	}

	private IEnumerator Duck() {
		yield return new WaitForSeconds(2f);
		explosion.gameObject.SetActive(true);
		explosion.Play("Explosion");
		dialogue.gameObject.SetActive(false);
		GetComponent<SpriteRenderer>().enabled = false;
		player.GetComponent<Health>().TakeDamage(40);
		yield return new WaitForSeconds(0.35f);
		player.PushForce.x = -300f;
		player.PushForce.y = 3f;
	}
}
