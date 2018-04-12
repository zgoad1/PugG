using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour {

	public Sprite fireHydrant;
	private SpriteRenderer checkpointSpriteRenderer;
	public bool checkpointReached;

	void Start () 
	{
		checkpointSpriteRenderer = GetComponent<SpriteRenderer> ();
	}

	void Update () 
	{
		
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if other.tag = "PlatformerCharacter2D"
		{
			checkpointSpriteRenderer.sprite = fireHydrant;
			checkpointReached = true;
		}
	}
				
}
