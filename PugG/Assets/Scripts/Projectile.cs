using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

	private Health PlayerHP;
	[SerializeField] private int damage = 10;

	void Start() {
		PlayerHP = GetComponent<Health>();
	}

     void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("" + damage + " damage taken");
        var hit = collision.gameObject;
        if (PlayerHP != null)
        {
            PlayerHP.TakeDamage(damage);
        }

        Destroy(gameObject);
    }

}
