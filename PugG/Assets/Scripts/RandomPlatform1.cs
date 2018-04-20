using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomPlatform1 : MonoBehaviour {

	[SerializeField] private Sprite[] sprites;
	private SpriteRenderer SR;

	// Use this for initialization
	void Start () {
		SR = GetComponent<SpriteRenderer>();
		// choose random shelf sprite 
		int rand = (int)Mathf.Floor(Random.Range(0f, sprites.Length));
		SR.sprite = sprites[rand];
	}
}
