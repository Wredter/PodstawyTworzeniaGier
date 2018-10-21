using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.UIElements;

public class ArrowXbox : ProjectileXbox
{
    private int step;
    private Vector2 initialVelocity;

    // Use this for initialization
    void Start()
    {
        step = range + 1;
        isReturnable = false;
    }

    public void Initialise(string arrowID, ArcherXbox player, float power)
    {
        name = arrowID;
        this.player = player;
        rb2d = GetComponent<Rigidbody2D>();
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        float projectileX = mousePosition.x - rb2d.position.x;
        float projectileY = mousePosition.y - rb2d.position.y;
        float r = Mathf.Sqrt(projectileX * projectileX + projectileY * projectileY);
        Vector2 projectileThrow = new Vector2(projectileX / r, projectileY / r);
        projectileThrow += new Vector2(randomSpread * 2 * (Random.value - 0.5f), randomSpread * 2 * (Random.value - 0.5f));
        rb2d.AddForce(projectileThrow * 500 * power);
    }

    private void FixedUpdate()
    {
        if (step == range)
        {
            initialVelocity = rb2d.velocity;
        }
        if (step > 0 && step <= range)
        {
            rb2d.velocity = initialVelocity * (4 + step / range) / 4;
        }
        if (step == 0)
        {
            rb2d.velocity = new Vector2(0, 0);
            ((ArcherXbox)player).ReturnProjectile(gameObject);
            Destroy(gameObject);
        }
        step -= 1;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
