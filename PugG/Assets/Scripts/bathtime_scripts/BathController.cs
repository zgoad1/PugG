using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BathController : MonoBehaviour {

    public float TIME_LIMIT = 5F;
    public Text timerText;
    private float timer = 0F;

    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {

        this.timer += Time.deltaTime;

        timerText.text = timer.ToString("F0");

        if (this.timer >= TIME_LIMIT)
        {
            SceneManager.LoadScene("Level1");
        }

        if (GameObject.FindWithTag("Coin") == null || GameObject.FindWithTag("Player") ==  null)
        {
            SceneManager.LoadScene("Level1");
        }

		
	}
}
