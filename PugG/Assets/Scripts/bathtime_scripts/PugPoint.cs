using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PugPoint : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter2D(Collider2D co)
    {
        if (co.name == "Puggy")
            Destroy(gameObject);
    }
}
