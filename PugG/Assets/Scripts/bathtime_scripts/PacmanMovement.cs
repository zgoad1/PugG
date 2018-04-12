using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PacmanMovement : MonoBehaviour {

    public float speed = 0.4f;
    Vector2 dest = Vector2.zero;

    void Start()
    {
        dest = transform.position;
    }

    void FixedUpdate()
    {
        var x = Input.GetAxis("Horizontal") * Time.deltaTime * 3.0f * speed;
        var z = Input.GetAxis("Vertical") * Time.deltaTime * 3.0f * speed;

        transform.Translate(x, 0, 0);
        transform.Translate(0, z, 0);
    }
}
