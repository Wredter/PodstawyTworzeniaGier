using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTest : MonoBehaviour
{
    public float moveSpeed;
    public float projectilePickupDistance;
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
        rb2d.velocity = new Vector2(input.x * moveSpeed, input.y * moveSpeed);
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

        Stack<GameObject> toReturn = new Stack<GameObject>();
        foreach(var a in axee.Keys)
        {
            if (axee[a].GetCounter() < 0)
            {
                if (Vector2.Distance(axee[a].GetPosition(), rb2d.position) < projectilePickupDistance)
                {
                    toReturn.Push(a);
                }
            }
        }
        while(toReturn.Count > 0)
        {
            var c = toReturn.Pop();
            Destroy(c);
            axee.Remove(c);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (axee.ContainsKey(collision.gameObject))
        {
            if (axee[collision.gameObject].GetCounter() < 0)
            {
                ReturnProjectile(collision.gameObject);
                return;
            }
        }
    }
    public void ReturnProjectile(GameObject p)
    {
        Destroy(p);
        axee.Remove(p);
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
