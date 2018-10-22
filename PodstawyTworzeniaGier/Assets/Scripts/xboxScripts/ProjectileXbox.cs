using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProjectileXbox : MonoBehaviour
{
    [Range(0.0f, 0.5f)]
    public float randomSpread;
    public int range;
    public int cooldown;
    public float damage;

    protected MinionBaseXbox player;
    protected Vector2 startingPosition;
    protected Vector2 startingVelocity;
    protected Rigidbody2D rb2d;
    protected bool hasHit;
    protected bool isReturnable;
    protected string controller;

    // Use this for initialization
    void Start()
    {
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (player == null)
        {
            Destroy(gameObject);
            return;
        }
        if (collision.gameObject.name.Equals(player.name))
        {
            if (collision.gameObject.GetComponent<VikingXbox>())
            {
                if (GetCounter() < 0)
                {
                    ((VikingXbox)player).ReturnProjectile(gameObject);
                    Destroy(gameObject);
                    return;
                }
            }
        }
        else
        {
            if (collision.gameObject.GetComponent<MinionBaseXbox>())
            {
                if (isReturnable)
                {
                    if (!hasHit)
                    {
                        collision.gameObject.GetComponent<MinionBaseXbox>().DealDamage(damage);
                        ((AxeXbox)this).Stick(collision.gameObject);
                        hasHit = true;
                    }
                }
                else
                {
                    collision.gameObject.GetComponent<MinionBaseXbox>().DealDamage(damage);
                    ((ArcherXbox)player).ReturnProjectile(gameObject);
                    Destroy(gameObject);
                    return;
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

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
