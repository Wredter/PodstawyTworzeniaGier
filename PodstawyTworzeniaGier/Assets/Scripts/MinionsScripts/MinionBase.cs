using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionBase : MonoBehaviour, IPlayerIntegration
{
    public float health;
    public GameObject healthBarView;
    public Horde infectedBy = null;
    public GameObject minionGhost;
    protected Vector2 input;
    protected Rigidbody2D rb2d;
    protected GameObject healthBar;
    protected string playerName;
    protected float actualHealth;
    //public bool isInfected;
    protected GameObject chief;
    protected IController controller;
    private int timer=0;

    public void Initialise()
    {
        actualHealth = health;
        rb2d = GetComponent<Rigidbody2D>();
        healthBar = Instantiate(healthBarView);
        healthBar.GetComponent<HealthBar>().Initialise(gameObject);
        healthBar.transform.SetParent(transform, false);
        //isInfected = false;
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
            if (infectedBy != null)
            {
                GameObject obj = Instantiate(infectedBy.hordeMinion, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z), gameObject.transform.rotation);
                infectedBy.minions.Add(obj);
                infectedBy.minionsWithChief.Add(obj);
                obj.GetComponent<IPlayerIntegration>().SetPlayerName(infectedBy.GetComponent<IPlayerIntegration>().GetPlayerName());
            }
            var tmp = gameObject.transform.position;
            Instantiate(minionGhost, tmp, new Quaternion(0, 0, 0, 0));

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
        if (collision.gameObject.GetComponent<Projectile>())
        {
            if (!collision.gameObject.GetComponent<Projectile>().GetPlayerName().Equals(playerName))
            {
                if (collision.GetComponent<Axe>())
                {
                    if (!collision.GetComponent<Axe>().GetHasHit())
                    {
                        DealDamage(collision.gameObject.GetComponent<Projectile>().damage);
                        collision.GetComponent<Axe>().SetHasHit(true);
                    }
                }
                else
                {
                    DealDamage(collision.gameObject.GetComponent<Projectile>().damage);
                    Destroy(collision.gameObject);
                }

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