using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OtherExecute : MonoBehaviour {

	// Use this for initialization
	void Start () {

		GameObject.Find("Tiles`").transform.Find("Tilemap_Other").gameObject.SetActive(true);
		transform.Find("HiddenDuck").gameObject.SetActive(true);
	}
}
