using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour {
    public Text timer;
    public float timeLimit;
    private float startTime;
    void Start()
    {
        startTime = Time.time;
    }

    private void Update()
    {
        float t = timeLimit - Time.time + startTime;
        string minutes = ((int)t / 60).ToString();
        string seconds = (t % 60).ToString("f0");

        timer.text = minutes + ":" + seconds;

        if (t <= 0)
        {
            SceneManager.LoadScene(SceneManager.GetSceneAt(0).name);
        }
    }
}
