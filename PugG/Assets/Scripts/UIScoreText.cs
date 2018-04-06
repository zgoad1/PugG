using UnityEngine;
using UnityEngine.UI;

public class UIScoreText : MonoBehaviour {

	// Use this for initialization
	void Start () {
		GetComponent<Text>().text = "" + FindObjectOfType<PickupTracker>().score;
	}
}
