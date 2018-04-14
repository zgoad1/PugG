using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PickupTracker : MonoBehaviour {
    
    public Text scoreText;
	public static int score;

    private int scoreCount;

    List<GameObject> pickups = new List<GameObject>();

    // Use this for initialization
    void Start () {
        score = 0;
        SetScoreText();
        scoreCount = GameObject.FindGameObjectsWithTag("Pickup").Length;
	}
	
	// Update is called once per frame
	void Update () {
		/*
        if (score >= scoreCount)
        {
            Scene scene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(scene.name);

        }
		*/
        SetScoreText();
        //Debug.Log("Found " + scoreCount + " Pickup Objects!");
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Pickup"))
        {
            if (!pickups.Contains(collision.gameObject)) ;
            {
                Destroy(collision.gameObject);
                score++;
                pickups.Add(collision.gameObject);
            }
        }
    }

    void resetPickupsList()
    {
        pickups.Clear();
    }

    void SetScoreText()
    {
        scoreText.text = "Score: " + score.ToString();
    }
}
