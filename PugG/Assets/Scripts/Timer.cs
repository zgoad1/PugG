using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour {
    public Text timer;
    public float timeLimit;
    private float startTime;
	public float timeLeft;
    void Start()
    {
        startTime = Time.time;
    }

    private void Update()
    {
        timeLeft = timeLimit - Time.time + startTime;
        string minutes = ((int)timeLeft / 60).ToString();
        string seconds = (timeLeft % 60).ToString("f0");

        timer.text = minutes + ":" + seconds;

        if (timeLeft <= 0)
        {
            SceneManager.LoadScene(SceneManager.GetSceneAt(0).name);
        }
    }
}
