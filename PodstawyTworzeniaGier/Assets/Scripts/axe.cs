using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class axe : MonoBehaviour {
    private Rigidbody2D rb2d;
    private Vector2 initialVelocity;
    private float step;

	// Use this for initialization
	void Start () {
        rb2d = GetComponent<Rigidbody2D>();
        initialVelocity = rb2d.velocity;
        step = 1.01f;
	}

    private void FixedUpdate()
    {
        if (step == 1)
        {
            initialVelocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, GetComponent<Rigidbody2D>().velocity.y);
        }
        if (step > 0)
        {
            rb2d.velocity = initialVelocity * step;
            step -= 0.01f;
        }
    }

    // Update is called once per frame
    void Update () {
        
	}
}
