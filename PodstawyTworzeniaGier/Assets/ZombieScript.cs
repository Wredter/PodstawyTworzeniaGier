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
        rb2d = gameObject.GetComponent<Rigidbody2D>();
        isZombie = true;
        myHorde = GameObject.Find("ZombieXbox").GetComponent<HordeXbox>();
        
    }

    // Update is called once per frame
    //private void FixedUpdate()
    //{
    //    input = new Vector2(Input.GetAxis("Horizontal") * speedMultipalyer, Input.GetAxis("Vertical") * speedMultipalyer);
    //    rb2d.velocity = new Vector2(input.x, input.y);
    //}
    void Update()
    {

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log("OnCollisionEnter : czy jestem playerem");
        if (collision.gameObject.GetComponent<MinionBaseXbox>().isZombie == false)
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
