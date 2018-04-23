using UnityEngine.SceneManagement;

public class TempTracker {
	public static int PP = 3;
	private static int tbu = 0;
	public static int TBUses {
		get {
			return tbu;
		}
		set {
			if(value > 0 && (SceneManager.GetActiveScene().name == "Daytime" && value >= tbu || SceneManager.GetActiveScene().name != "Daytime")) {
				tbu = value;
			}
		}
	}
	private static int fbu = 0;
	public static int FBUses {
		get {
			return fbu;
		}
		set {
			if(value > 0 && (SceneManager.GetActiveScene().name == "Daytime" && value >= fbu || SceneManager.GetActiveScene().name != "Daytime")) {
				fbu = value;
			}
		}
	}
	public static bool Odor = true;
}
