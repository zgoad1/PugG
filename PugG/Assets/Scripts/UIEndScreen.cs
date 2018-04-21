using UnityEngine;
using UnityEngine.SceneManagement;

public class UIEndScreen : MonoBehaviour {

	public GameObject song;

	private void Start() {
		Destroy(song);
	}

	// Update is called once per frame
	void Update () {
		if(Input.GetButtonDown("Interact") && GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("EndScreen_Stay")) {
			Debug.Log("Loading daytime scene");
			// Load the daytime scene
			SceneManager.LoadScene("Daytime");
		}
	}
}
