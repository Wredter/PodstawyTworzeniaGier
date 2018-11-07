using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Projectile : MonoBehaviour {
    [Range(0.0f,0.5f)]
    public float randomSpread;
    public int range;
    public int cooldown;
    public float damage;
   
    protected MinionBase player;
    protected Vector2 startingPosition;
    protected Vector2 startingVelocity;
    protected Rigidbody2D rb2d;
    protected bool hasHit;
    protected bool isReturnable;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name.Equals(player.name))
        {
            if (collision.gameObject.GetComponent<Viking>())
            {
                if (GetCounter() < 0)
                {
                    ((Viking)player).ReturnProjectile(gameObject);
                    Destroy(gameObject);
                    return;
                }
            }
        } else
        {
            if (collision.gameObject.GetComponent<MinionBase>())
            {
                if (isReturnable)
                {
                    if (!hasHit)
                    {
                        collision.gameObject.GetComponent<MinionBase>().DealDamage(damage);
                        gameObject.GetComponent<Axe>().Stick(collision.gameObject);
                        hasHit = true;
                    }
                } else
                {
                    collision.gameObject.GetComponent<MinionBase>().DealDamage(damage);
                    ((Archer)player).ReturnProjectile(gameObject);
                    Destroy(gameObject);
                    return;
                }
            }
        }
    }

    public void UpdateCounter()
    {
        cooldown--;
    }

    public int GetCounter()
    {
        return cooldown;
    }

    public Vector2 GetPosition()
    {
        return rb2d.position;
    }

    public string GetProjectileID()
    {
        return name;
    }
}
