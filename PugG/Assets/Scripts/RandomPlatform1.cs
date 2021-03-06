﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class RandomPlatform1 : MonoBehaviour {

	[SerializeField] private Sprite[] sprites;
	[SerializeField] private Sprite level3Sprite;
	private SpriteRenderer SR;
	private GameObject lamp;
	private GameObject vase;

	// Use this for initialization
	void Start() {
		SR = GetComponent<SpriteRenderer>();
		// choose random shelf sprite 
		int rand = (int)Mathf.Floor(Random.Range(0f, sprites.Length));
		SR.sprite = sprites[rand];

		if(SceneManager.GetActiveScene().name == "Level1") {
			lamp = GameObject.Find("Shelf Lamp");
			vase = GameObject.Find("Shelf Vase");
			float rand1 = -1000000;
			if(Random.Range(0f, 7.5f) < 1f) {
				Instantiate(lamp);
				rand1 = Random.Range(-0.7f, 0.7f);
				lamp.transform.position = transform.position + new Vector3(rand1, 0.47f, 0f);
			}
			if(Random.Range(0f, 7f) < 1f) {
				Instantiate(vase);
				float rand2 = 0f;
				do {
					rand2 = Random.Range(-0.7f, 0.7f);
				} while(Mathf.Abs(rand2 - rand1) < 0.2f);
				vase.transform.position = transform.position + new Vector3(rand2, 0.38f, 0f);
			}
		} else if(SceneManager.GetActiveScene().name == "Level3") {
			SR.sprite = level3Sprite;
		}
	}
}
