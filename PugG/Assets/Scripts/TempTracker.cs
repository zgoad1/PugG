using UnityEngine.SceneManagement;

public class TempTracker {
	private static int cl = 1;
	public static int currentLevel {
		get {
			return cl;
		}
		set {
			if(value > 3 || value < 1) {
				cl = 1;
			} else {
				cl = value;
			}
		}
	}
	public static int PP = 3;
	private static int tbu = 0;
	public static int TBUses {
		get {
			return tbu;
		}
		set {
			if(value >= 0 && (SceneManager.GetActiveScene().name == "Daytime" && value >= tbu || SceneManager.GetActiveScene().name != "Daytime")) {
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
			if(value >= 0 && (SceneManager.GetActiveScene().name == "Daytime" && value >= fbu || SceneManager.GetActiveScene().name != "Daytime")) {
				fbu = value;
			}
		}
	}
	public static bool Odor = true;

	public static void incrementLevel() {
		currentLevel++;
	}
}
