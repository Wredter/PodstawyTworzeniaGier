using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {
    public int cooldown;
   
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
