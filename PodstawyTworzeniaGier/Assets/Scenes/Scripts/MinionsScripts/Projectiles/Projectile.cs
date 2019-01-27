using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Projectile : MonoBehaviour, IPlayerIntegration
{
    [Range(0.0f, 0.5f)]
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
    protected string controller;
    protected string playerName;

    // Use this for initialization
    void Start()
    {
    }

    /*public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name.Equals(player.name))
        {
            
        }
        else
        {
            if(collision.gameObject.GetComponent<ChiefBaseXbox>())
            {
                if (isReturnable)
                {
                    if (!hasHit)
                    {
                        collision.gameObject.GetComponent<ChiefBaseXbox>().DealDamage(damage);
                        ((AxeXbox)this).Stick(collision.gameObject);
                        hasHit = true;
                    }
                }
                else
                {
                    collision.gameObject.GetComponent<ChiefBaseXbox>().DealDamage(damage);
                    ((ArcherXbox)player).ReturnProjectile(gameObject);
                    Destroy(gameObject);
                    return;
                }
            }
        }
    }*/

    public void UpdateCounter()
    {
        cooldown--;
    }

    public int GetCounter()
    {
        return cooldown;
    }

    public string GetPlayerName()
    {
        return playerName;
    }

    public Vector2 GetPosition()
    {
        return rb2d.position;
    }

    public string GetProjectileID()
    {
        return name;
    }

    public MinionBase GetPlayer()
    {
        return player;
    }

    public void SetPlayerName(string playerName)
    {
        this.playerName = playerName;
    }
}
