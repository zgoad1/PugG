using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UnityStandardAssets._2D
{
    public class Restarter : MonoBehaviour
    {
		private static PlatformerCharacter2D Player = FindObjectOfType<PlatformerCharacter2D>();

		public static void Restart(bool fromCheckpoint) {
			if(!fromCheckpoint)
				SceneManager.LoadScene(SceneManager.GetActiveScene().name);
			else
				Player.transform.position = Player.respawnPoint;
		}
    }
}
