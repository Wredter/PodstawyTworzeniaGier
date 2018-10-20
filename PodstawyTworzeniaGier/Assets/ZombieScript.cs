using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieScript : MinionBase
{

    // Use this for initialization
    public float speedMultipalyer;
    public float dmg;
    void Start()
    {
        rb2d = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        input = new Vector2(Input.GetAxis("Horizontal") * speedMultipalyer, Input.GetAxis("Vertical") * speedMultipalyer);
        rb2d.velocity = new Vector2(input.x, input.y);
    }
    void Update()
    {

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("OnCollisionEnter : czy jestem playerem");
        if (collision.gameObject.GetComponent<MinionBase>() && collision.gameObject.GetComponent<localHorde>().hordeName != gameObject.GetComponent<localHorde>().hordeName)
        {
            Destroy(collision.gameObject);
            Debug.Log("OnCollisionEnter : zniszczyles mnie");
            Instantiate(gameObject,collision.transform.position,collision.transform.rotation);
        }
    }
}
