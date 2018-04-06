using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisionHandler : MonoBehaviour {

	private Health PlayerHealth;
	private GameObject EndScreen;
	private GameObject Player;
	private GameObject FakePlayer;

	private void Start() {
		PlayerHealth = FindObjectOfType<Health>();
		EndScreen = GameObject.Find("End Screen");
		Player = GameObject.Find("Player");
		FakePlayer = GameObject.Find("Fake Player");
	}

	private void OnTriggerEnter(Collider other) {
		Debug.Log("Hit a trigger");
		if(other.gameObject.tag == "Goal") {
			Debug.Log("Hit goal");
			// Replace the player with a fake so the user can't control it and it can't die
			Transform pos = Player.transform;
			Destroy(Player);
			Instantiate(FakePlayer, pos);
			EndScreen.GetComponent<Animation>().Play();
		}
	}

}
