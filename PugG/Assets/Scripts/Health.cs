using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityStandardAssets._2D;

public class Health : MonoBehaviour {

	private PlatformerCharacter2D Player;
	[SerializeField] private float gracePeriod = 1f;	// grace period after getting hit in seconds
	private SpriteRenderer sprite;						// sprite to flash when invincible

	public bool invincible = false;
	public const int maxHealth = 100;
	public int currentHealth = maxHealth;
	public RectTransform healthBar;

	private void Start() {
		sprite = GetComponent<SpriteRenderer>();
		Player = FindObjectOfType<PlatformerCharacter2D>();
	}

	public void TakeDamage(int amount) {
		if(!invincible) {
			StartCoroutine("GracePeriod", gracePeriod);
			currentHealth -= amount;
			if(currentHealth <= 0) {
				Player.Die();
			}

			healthBar.sizeDelta = new Vector2(currentHealth, healthBar.sizeDelta.y);
		}
	}

	private IEnumerator GracePeriod(float time) {
		invincible = true;
		StartCoroutine("Flash");
		yield return new WaitForSeconds(time);
		invincible = false;
		StopCoroutine("Flash");
		sprite.enabled = true;
	}

	private IEnumerator Flash() {
		sprite.enabled = false;
		yield return new WaitForSeconds(0.1f);
		sprite.enabled = true;
		yield return new WaitForSeconds(0.2f);
		StartCoroutine("Flash");
	}
}