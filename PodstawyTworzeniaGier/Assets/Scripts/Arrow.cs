using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : Projectile {
    private int step;
    private Vector2 initialVelocity;
    private int initialStepValue = 40;

	// Use this for initialization
	void Start () {
        step = initialStepValue + 1;
	}

    public void Initialise(string axeID, Archer player, float power)
    {
        name = axeID;
        this.player = player;
        rb2d = GetComponent<Rigidbody2D>();
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        float projectileX = mousePosition.x - rb2d.position.x;
        float projectileY = mousePosition.y - rb2d.position.y;
        float r = Mathf.Sqrt(projectileX * projectileX + projectileY * projectileY);
        Vector2 projectileThrow = new Vector2(projectileX / r, projectileY / r);
        rb2d.AddForce(projectileThrow * 500 * power);
    }

    private void FixedUpdate()
    {
        if (step == initialStepValue)
        {
            initialVelocity = rb2d.velocity;
        }
        if (step > 0 && step <= initialStepValue)
        {
            rb2d.velocity = initialVelocity * (4 + step / initialStepValue) / 4;
        }
        if (step == 0)
        {
            rb2d.velocity = new Vector2(0, 0);
            ((Archer)player).ReturnProjectile(gameObject);
            Destroy(gameObject);
        }
        step -= 1;
    }

    // Update is called once per frame
    void Update () {
		
	}
}
