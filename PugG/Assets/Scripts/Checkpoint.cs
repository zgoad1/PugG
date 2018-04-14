using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour {

	[SerializeField] private ParticleSystem ps;

	private void Start() {
		ps.Stop();
	}

	public void DoParticles() {
		ps.Play();
	}

	/*

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
		if(other.tag == "Player")
		{
			checkpointSpriteRenderer.sprite = fireHydrant;
			checkpointReached = true;
		}
	}
		*/		
}
