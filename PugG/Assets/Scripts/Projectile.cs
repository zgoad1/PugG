using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets._2D;
public class Projectile : MonoBehaviour {

	private Health PlayerHP;
	[SerializeField] private int damage = 10;

	void Start() {
		PlayerHP = FindObjectOfType<PlatformerCharacter2D>().GetComponent<Health>();
	}

     void OnTriggerEnter2D(Collider2D collision)
    {
        var hit = collision.gameObject;
        if (PlayerHP != null) {
			Debug.Log("" + damage + " damage taken");
			PlayerHP.TakeDamage(damage);
        }

        Destroy(gameObject);
    }

}
