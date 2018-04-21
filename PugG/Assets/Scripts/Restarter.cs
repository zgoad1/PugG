using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UnityStandardAssets._2D {
	public class Restarter : MonoBehaviour {
		private static PlatformerCharacter2D Player;

		public static void Restart(bool fromCheckpoint) {
			Player = FindObjectOfType<PlatformerCharacter2D>();
			if(!fromCheckpoint)
				SceneManager.LoadScene(SceneManager.GetActiveScene().name);
			else
				Player.transform.position = Player.respawnPoint;
		}
	}
}
