﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionBaseXbox : MonoBehaviour
{
    public float health;
    public GameObject healthBarView;

    protected Vector2 input;
    protected Rigidbody2D rb2d;
    protected GameObject healthBar;
    protected float actualHealth;
    public bool isZombie;
    protected GameObject chief;
    protected IController controller;

    public void Initialise()
    {
        actualHealth = health;
        rb2d = GetComponent<Rigidbody2D>();
        healthBar = Instantiate(healthBarView);
        healthBar.GetComponent<HealthBarScriptXbox>().Initialise(gameObject);
        healthBar.transform.SetParent(transform, false);
        isZombie = false;
    }

    protected void FixedUpdate()
    {
        input = new Vector2(controller.MoveHorizontal(), controller.MoveVertical());
        rb2d.velocity = new Vector2();
    }

    public void DealDamage(float damage)
    {
        actualHealth -= damage;
        if (actualHealth <= 0)
        {
            //Death
            Destroy(gameObject);
        }
    }

    public float GetActualHealth()
    {
        return actualHealth;
    }

    public Vector2 GetPosition()
    {
        return rb2d.position;
    }

    public Vector2 GetVelocity()
    {
        return rb2d.velocity;
    }

    public GameObject GetChief()
    {
        return chief;
    }

    public void SetChief(GameObject chief)
    {
        this.chief = chief;
    }

    public void SetController(IController controller)
    {
        this.controller = controller;
    }
}