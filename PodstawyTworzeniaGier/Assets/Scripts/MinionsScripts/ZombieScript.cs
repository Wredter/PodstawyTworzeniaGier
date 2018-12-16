﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieScript : MinionBase
{

    // Use this for initialization
    public float speedMultipalyer;
    [Range(0,20)]
    public float dmgOnContact = 2;
    [Range(0,20)]
    public int poisonDmg = 10;
    [Range(1, 10)]
    public int poisonNumberOfTicks = 4;
    [Range(0, 2)]
    public float timeBetweenTicksInSec = 0.75f;
    Horde myHorde;
    void Start()
    {
        Initialise();
        rb2d = gameObject.GetComponent<Rigidbody2D>();
        //isInfected = true;

        foreach(GameObject horde in GameObject.FindGameObjectsWithTag("Horde"))
        {
            if (horde.gameObject.GetComponent<Horde>().GetPlayerName().Equals(playerName))
            {
                myHorde = horde.GetComponent<Horde>();
            }
        }
        

        
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

        if (collision.gameObject.GetComponent<MinionBase>())
        {
            if (!collision.gameObject.GetComponent<ZombieScript>() && !collision.gameObject.GetComponent<MinionBase>().GetPlayerName().Equals(playerName))
            {
                collision.gameObject.GetComponent<MinionBase>().DealDamage(dmgOnContact);
                if (collision.gameObject.GetComponent<StatusEfectMenager>())
                {
                    collision.gameObject.GetComponent<StatusEfectMenager>().ApplyPoison(poisonNumberOfTicks, poisonDmg, timeBetweenTicksInSec);
                    collision.gameObject.GetComponent<MinionBase>().infectedBy = myHorde;
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