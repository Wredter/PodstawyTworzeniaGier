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

    // Use this for initialization
    void Start () {
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name.Equals(gameObject.name))
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
                collision.gameObject.GetComponent<MinionBase>().DealDamage(damage);
            }
        }
    }

    // Update is called once per frame
    void Update () {
		
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
