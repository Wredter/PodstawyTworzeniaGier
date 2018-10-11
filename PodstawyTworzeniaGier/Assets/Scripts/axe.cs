using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class axe : MonoBehaviour {
    private Rigidbody2D rb2d;
    private Vector2 initialVelocity;
    private static int initalStepValue = 40;
    private int step;

	// Use this for initialization
	void Start () {
        rb2d = GetComponent<Rigidbody2D>();
        step = initalStepValue + 1;
	}

    private void FixedUpdate()
    {
        if (step == initalStepValue)
        {
            initialVelocity = rb2d.velocity;
        }
        if (step > 0 && step <= initalStepValue)
        {
            rb2d.velocity = initialVelocity * (4 + step/initalStepValue)/4;
        }
        if (step == 0)
        {
            rb2d.velocity = new Vector2(0, 0);
        }
        step -= 1;
    }

    // Update is called once per frame
    void Update () {
        
	}
}
