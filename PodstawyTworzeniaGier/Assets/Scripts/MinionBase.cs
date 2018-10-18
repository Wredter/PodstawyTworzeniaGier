﻿using System.Collections;
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

    private void FixedUpdate()
    {
        input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        rb2d.velocity = new Vector2(input.x, input.y);
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

    protected void callFixedUpdate()
    {
        FixedUpdate();
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