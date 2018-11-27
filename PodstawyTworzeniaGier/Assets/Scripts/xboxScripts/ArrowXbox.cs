using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.UIElements;

public class ArrowXbox : ProjectileXbox
{
    private int step;
    private Vector2 initialVelocity;

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

        float projectileX = player.GetChief().GetComponent<ChiefXbox>().GetPrevious().x;
        float projectileY = player.GetChief().GetComponent<ChiefXbox>().GetPrevious().y;
        Vector2 projectileThrow = new Vector2(projectileX, projectileY);
        Vector2 pom = new Vector2(player.GetChief().GetComponent<ChiefXbox>().GetController().LookHorizontal(), player.GetChief().GetComponent<ChiefXbox>().GetController().LookVertical());
        var rad = Mathf.Atan2(pom.y, pom.x);
        rb2d.rotation = rad * Mathf.Rad2Deg - 90;
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(player == null)
        {
            Destroy(gameObject);
            return;
        }
        if (!collision.gameObject.GetComponent<MinionBaseXbox>() && !collision.gameObject.GetComponent<ChiefBaseXbox>())
        {
            ((ArcherXbox)player).ReturnProjectile(gameObject);
            Destroy(gameObject);
        }
    }
}
