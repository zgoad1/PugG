using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LoadSceneOnClick : MonoBehaviour
{
	public void LoadNext() {
		Debug.Log("Loading daytime scene");
		// Load the daytime scene
		SceneManager.LoadScene("Daytime");
	}

		public void LoadByIndex(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
		Debug.Log("Loading scene, index #" + sceneIndex);
    }

	public void LoadByName(string name) {
		SceneManager.LoadScene(name);
	}
}