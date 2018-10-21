using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionBaseXbox : MonoBehaviour
{
    public float health;
    public GameObject healthBarView;

    protected Vector2 input;
    protected Rigidbody2D rb2d;
    protected GameObject healthBar;
    protected string controller;
    // Use this for initialization
    void Start()
    {

    }

    public void Initialise()
    {
        rb2d = GetComponent<Rigidbody2D>();
        healthBar = Instantiate(healthBarView);
        healthBar.GetComponent<HealthBarScriptXbox>().Initialise(gameObject);
        healthBar.transform.SetParent(transform, false);
    }

    private void FixedUpdate()
    {
        input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        rb2d.velocity = new Vector2();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void DealDamage(float damage)
    {
        health -= damage;
        if (health < 0)
        {
            //Death
            Destroy(gameObject);
        }
    }

    protected void CallFixedUpdate()
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

    public void SetController(string controller)
    {
        this.controller = controller;
    }
}