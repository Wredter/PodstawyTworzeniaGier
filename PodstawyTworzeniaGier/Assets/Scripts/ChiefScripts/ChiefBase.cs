using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChiefBase : MonoBehaviour, IPlayerIntegration {
    public float health;
    public GameObject healthBarView;
    public GameObject archersWon;
    public GameObject vikingsWon;

    protected string playerName;
    protected Rigidbody2D rb2d;
    protected GameObject healthBar;
    protected float actualHealth;
	
	void Start () {
        actualHealth = health;
        rb2d = GetComponent<Rigidbody2D>();
        healthBar = Instantiate(healthBarView);
        healthBar.GetComponent<HealthBar>().Initialise(gameObject);
        healthBar.transform.SetParent(transform, false);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Projectile>())
        {
            if (collision.GetComponent<IPlayerIntegration>().GetPlayerName() != playerName)
            {
                DealDamage(collision.GetComponent<Projectile>().damage);
            }
        }
    }

    public void DealDamage(float damage)
    {
        //actualHealth -= damage;
        if (actualHealth < 0)
        {
            if (playerName == "Player1")
            {
                SceneManager.LoadScene("Player2Won");


            }
            else
            {
                SceneManager.LoadScene("Player1Won");
            }
            Destroy(gameObject);
        }
    }

    public float GetActualHealth()
    {
        return actualHealth;
    }

    public string GetPlayerName()
    {
        return playerName;
    }

    public void SetPlayerName(string playerName)
    {
        this.playerName = playerName;
    }
}
