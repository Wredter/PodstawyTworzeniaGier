using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTest : MonoBehaviour
{
    public GameObject player;
    public float moveSpeed;
    public GameObject projectile;
    public int maxProjectileCount;
    private Vector2 input;
    private Rigidbody2D rb2d;
    private List<GameObject> projectiles;
    private List<Axe> proj;
    private int projectilesCount;

    // Use this for initialization
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        projectiles = new List<GameObject>();
        proj = new List<Axe>();
        projectilesCount = 0;
        player.name = "player";
    }

    private void FixedUpdate()
    {

        input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        rb2d.velocity = new Vector2(input.x * moveSpeed, input.y * moveSpeed);
        proj.ForEach(i => i.UpdateCounter());
    }

    // Update is called once per frame
    void Update()
    {        
        if (Input.GetKeyDown(KeyCode.Space) && projectiles.Count < maxProjectileCount)
        {
            projectiles.Add(Instantiate(projectile, transform.position, transform.rotation));
            proj.Add(projectiles[projectiles.Count - 1].GetComponent<Axe>());
            proj[proj.Count - 1].Initialise("axe" + projectilesCount, this, input);
            projectilesCount++;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        foreach (GameObject g in projectiles)
        {
            if (collision.gameObject.Equals(g))
            {
                if (g.GetComponent<Projectile>().GetCounter() > 40)
                {
                    ReturnProjectile(g);
                    return;
                }
            }
        }
    }
    public void ReturnProjectile(GameObject p)
    {
        proj.Remove(p.GetComponent<Axe>());
        Destroy(p);
        projectiles.Remove(p);
    }

    public void ReturnProjectile(Projectile p)
    {
        GameObject temp = projectiles.Find(d => d.GetComponent<Projectile>().name.Equals(p.name));
        Destroy(temp);
        projectiles.Remove(temp);
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
