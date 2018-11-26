using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChiefBaseXbox : MonoBehaviour, IPlayerIntegration {
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
        healthBar.GetComponent<HealthBarScriptXbox>().Initialise(gameObject);
        healthBar.transform.SetParent(transform, false);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<ProjectileXbox>())
        {
            if (collision.GetComponent<IPlayerIntegration>().GetPlayerName() != playerName)
            {
                DealDamage(collision.GetComponent<ProjectileXbox>().damage);
            }
        }
    }

    public void DealDamage(float damage)
    {
        actualHealth -= damage;
        if (actualHealth < 0)
        {
            if (gameObject.name == "archers")
            {
                SceneManager.LoadScene("VikingsWon");


            }
            else
            {
                SceneManager.LoadScene("ArchersWon");
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
