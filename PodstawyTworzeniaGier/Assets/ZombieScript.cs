using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieScript : MinionBaseXbox
{

    // Use this for initialization
    public float speedMultipalyer;
    public float dmg;
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
        if (!collision.gameObject.GetComponent<ZombieScript>()&&collision.gameObject.GetComponent<MinionBaseXbox>())
        {
            Destroy(collision.gameObject);
            Debug.Log("OnCollisionEnter : zniszczyles mnie");
            GameObject pom = Instantiate(gameObject, collision.transform.position, collision.transform.rotation);
            myHorde.minions.Add(pom);
            myHorde.minionsWithChief.Add(pom);
            //Instantiate(gameObject,collision.transform.position,collision.transform.rotation);
        }
    }
}
