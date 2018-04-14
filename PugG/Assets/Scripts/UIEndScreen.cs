using UnityEngine;
using UnityEngine.SceneManagement;
using UnityStandardAssets._2D;

public class UIEndScreen : MonoBehaviour {
	
	// Update is called once per frame
	void Update () {
		if(Input.GetButtonDown("Interact") && GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("EndScreen_Stay")) {
			Debug.Log("Loading daytime scene");
			// Load the daytime scene\
			Restarter.Restart(false);
		}
	}
}
