﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BathController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if(GameObject.FindWithTag("Coin") == null)
        {
            SceneManager.LoadScene("Level1");
        }
		
	}
}
