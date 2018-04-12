using UnityEngine;
using UnityEngine.UI;

public class UITimeText : MonoBehaviour {

    // Use this for initialization
    private float startTime;
    void Start () {
        float t = Time.time - startTime;
        string minutes = ((int)t / 60).ToString();
        string seconds = (t % 60).ToString("f0");
        GetComponent<Text>().text = minutes + ":" + seconds;
	}
}
