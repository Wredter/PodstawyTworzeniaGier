using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTest : MonoBehaviour
{
    public float health;
    public GameObject projectile;
    public int maxProjectileCount;

    private Vector2 input;
    private Rigidbody2D rb2d;
    private Dictionary<GameObject, Axe> axee;
    private int projectilesCount;

    // Use this for initialization
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        axee = new Dictionary<GameObject, Axe>();
        projectilesCount = 0;
        name = "player";
    }

    private void FixedUpdate()
    {

        input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        rb2d.velocity = new Vector2(input.x, input.y);
        foreach(Axe g in axee.Values)
        {
            g.UpdateCounter();
        }
    }

    // Update is called once per frame
    void Update()
    {        
        if (Input.GetKeyDown(KeyCode.Space) && axee.Count < maxProjectileCount)
        {
            GameObject temp = (Instantiate(projectile, transform.position, transform.rotation));
            axee.Add(temp, temp.GetComponent<Axe>());
            axee[temp].Initialise("axe" + projectilesCount, this, input);
            projectilesCount++;
        }
    }

    public void DealDamage(float damage)
    {
        health -= damage;
        if (health < 0)
        {
            //Death
            health = 0;
        }
    }

    public void ReturnProjectile(GameObject p)
    {
        axee.Remove(p);
    }

    public GameObject GetGameObject()
    {
        return gameObject;
    }

    public Vector2 GetPosition()
    {
        return rb2d.position;
    }

    public Vector2 GetVelocity()
    {
        return rb2d.velocity;
    }
}
