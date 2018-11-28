using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieScript : MinionBaseXbox
{

    // Use this for initialization
    public float speedMultipalyer;
    [Range(0,20)]
    public float dmgOnContact = 2;
    [Range(0,20)]
    public int poisonDmg = 10;
    [Range(1, 10)]
    public int poisonNumberOfTicks = 4;
    HordeXbox myHorde;
    void Start()
    {
        Initialise();
        rb2d = gameObject.GetComponent<Rigidbody2D>();
        //isInfected = true;
        myHorde = GameObject.Find("ZombieXbox").GetComponent<HordeXbox>();
        
    }
    private new void FixedUpdate()
    {
        base.FixedUpdate();
    }
    void Update()
    {

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log("OnCollisionEnter : czy jestem playerem");

        if (collision.gameObject.GetComponent<MinionBaseXbox>())
        {
            if (!collision.gameObject.GetComponent<ZombieScript>() && !collision.gameObject.GetComponent<MinionBaseXbox>().GetPlayerName().Equals(playerName))
            {
                collision.gameObject.GetComponent<MinionBaseXbox>().DealDamage(dmgOnContact);
                if (collision.gameObject.GetComponent<StatusEfectMenager>())
                {
                    collision.gameObject.GetComponent<StatusEfectMenager>().ApplyPoison(poisonNumberOfTicks, poisonDmg);
                }

                //GameObject pom = Instantiate(gameObject, collision.transform.position, collision.transform.rotation);
                //myHorde.minions.Add(pom);
                //myHorde.minionsWithChief.Add(pom);
                //Instantiate(gameObject,collision.transform.position,collision.transform.rotation);


                //Destroy(collision.gameObject);

            }
        }
    }
}
