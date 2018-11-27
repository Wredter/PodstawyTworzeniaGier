using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionBaseXbox : MonoBehaviour, IPlayerIntegration
{
    public float health;
    public GameObject healthBarView;

    protected Vector2 input;
    protected Rigidbody2D rb2d;
    protected GameObject healthBar;
    protected string playerName;
    protected float actualHealth;
    public bool isInfected;
    protected GameObject chief;
    protected IController controller;

    public void Initialise()
    {
        actualHealth = health;
        rb2d = GetComponent<Rigidbody2D>();
        healthBar = Instantiate(healthBarView);
        healthBar.GetComponent<HealthBarScriptXbox>().Initialise(gameObject);
        healthBar.transform.SetParent(transform, false);
        isInfected = false;
    }

    protected void FixedUpdate()
    {
        //input = new Vector2(controller.MoveHorizontal(), controller.MoveVertical());
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

    public void OnTriggerEnter2D(Collider2D collision)
    {
        //Deal damage if trigger is not owned by player
        if(collision.gameObject.GetComponent<ProjectileXbox>())
        {
            if (!collision.gameObject.GetComponent<ProjectileXbox>().GetPlayerName().Equals(playerName))
            {
                DealDamage(collision.gameObject.GetComponent<ProjectileXbox>().damage);
            }
        }
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

    public void SetPlayerName(string playerName)
    {
        this.playerName = playerName;
    }

    public string GetPlayerName()
    {
        return playerName;
    }
}