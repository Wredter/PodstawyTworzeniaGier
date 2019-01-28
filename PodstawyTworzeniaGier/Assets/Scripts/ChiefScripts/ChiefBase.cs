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
    protected float actualHealth;
    private IController controller;
	
	void Start () {
        actualHealth = health;
        rb2d = GetComponent<Rigidbody2D>();
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
        if (collision.gameObject.tag == "Mud")
        {
            GetComponentInParent<Horde>().slow = 0.5f;
        }
    }

    public void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Mud")
        {
            GetComponentInParent<Horde>().slow = 0.5f;
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Mud")
        {
            GetComponentInParent<Horde>().slow = 1f;
        }

    }

    public void DealDamage(float damage)
    {
        //Nothing happens
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

    public void SetController(IController controller)
    {
        this.controller = controller;
    }
}
