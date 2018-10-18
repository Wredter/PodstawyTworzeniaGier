using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionBase : MonoBehaviour
{
    public float health;

    protected Vector2 input;
    protected Rigidbody2D rb2d;
	// Use this for initialization
	void Start ()
    {
        
    }

    public void Initialise()
    {
        rb2d = GetComponent<Rigidbody2D>();
        name = "player";
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void DealDamage(float damage)
    {
        health -= damage;
        if (health < 0)
        {
            //Death
            health = 0;
        }
    }

    public GameObject GetGameObject()
    {
        return gameObject;
    }

    public Vector2 GetPosition()
    {
        return rb2d.position;
    }

    public Vector2 GetVelocity()
    {
        return rb2d.velocity;
    }
}
