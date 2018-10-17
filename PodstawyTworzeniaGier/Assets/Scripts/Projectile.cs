using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {
    public int cooldown;
    public float damage;
   
    protected PlayerTest player;
    protected Vector2 startingPosition;
    protected Vector2 startingVelocity;

	// Use this for initialization
	void Start () {
	}

    public void Initialise(string projectileID, PlayerTest player, Vector2 input)
    {
        name = projectileID;
        this.player = player;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals(player.tag))
        {
            if (GetCounter() < 0)
            {
                player.ReturnProjectile(gameObject);
                Destroy(gameObject);
                return;
            }
        } 
        else
        {
            if(collision.gameObject.GetComponent<PlayerTest>())
            {
                collision.gameObject.GetComponent<PlayerTest>().DealDamage(damage);
            }
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

    public string GetProjectileID()
    {
        return name;
    }
}
