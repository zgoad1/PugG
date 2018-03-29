using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Health : MonoBehaviour {

	[SerializeField] private float gracePeriod = 1f;	// grace period after getting hit in seconds
	private SpriteRenderer sprite;						// sprite to flash when invincible
	private bool invincible = false;

	public const int maxHealth = 100;
	public int currentHealth = maxHealth;
	public RectTransform healthBar;

	private void Start() {
		sprite = GetComponent<SpriteRenderer>();
	}

	public void TakeDamage(int amount) {
		if(!invincible) {
			StartCoroutine("GracePeriod", gracePeriod);
			currentHealth -= amount;
			if(currentHealth <= 0) {
				currentHealth = 0;
				Scene scene = SceneManager.GetActiveScene();
				SceneManager.LoadScene(scene.name);
			}

			healthBar.sizeDelta = new Vector2(currentHealth, healthBar.sizeDelta.y);
		} else {
			Debug.Log("I'M INVINCIBLE");
		}
	}

	private IEnumerator GracePeriod(float time) {
		Debug.Log("INVINCIBILITY START");
		invincible = true;
		StartCoroutine("Flash");
		yield return new WaitForSeconds(time);
		invincible = false;
		StopCoroutine("Flash");
		sprite.enabled = true;
		Debug.Log("INVINCIBILITY END");
	}

	private IEnumerator Flash() {
		sprite.enabled = false;
		yield return new WaitForSeconds(0.1f);
		sprite.enabled = true;
		yield return new WaitForSeconds(0.2f);
		StartCoroutine("Flash");
	}
}