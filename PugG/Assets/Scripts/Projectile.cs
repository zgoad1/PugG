using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

     void OnTriggerEnter2D(Collision2D collision)
    {
        Debug.Log("damage taken");
        var hit = collision.gameObject;
        var health = hit.GetComponent<Health>();
        if (health != null)
        {
            health.TakeDamage(10);
        }

        Destroy(gameObject);
    }

}
