using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {
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
        if (collision.gameObject.GetComponent<Viking>())
        {
            if (GetCounter() < 0)
            {
                ((Viking)player).ReturnProjectile(gameObject);
                Destroy(gameObject);
                return;
            }
        } 
        else
        {
            //TODO dopisanie zadawania obrażeń wodzowi itp
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
