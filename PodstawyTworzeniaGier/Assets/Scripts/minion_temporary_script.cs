using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class minion_temporary_script : MonoBehaviour
{
    public float moveSpeed;
    public GameObject projectile;
    public int maxProjectileCount;
    private Vector2 input;
    private Rigidbody2D rb2d;
    private int bulletCount;
    private List<Bullet> bullets;

    // Use this for initialization
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        bullets = new List<Bullet>();
        bulletCount = 0;
    }

    private void FixedUpdate()
    {

        input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        rb2d.velocity = new Vector2(input.x * moveSpeed, input.y * moveSpeed);
        bullets.ForEach(i => i.UpdateCounter());
    }

    // Update is called once per frame
    void Update()
    {        
        if (Input.GetKeyDown(KeyCode.Space) && bullets.Count < maxProjectileCount)
        {
            bullets.Add(new Bullet(rb2d, input, bulletCount, projectile));
            bulletCount++;
            
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        foreach(Bullet b in bullets)
        {
            if(b.GetGameObject().name.Equals(other.gameObject.name) && b.GetCounter() > 40)
            {
                Destroy(other.gameObject);
                bullets.Remove(b);
                break;
            }
        }
    }

    class Bullet
    {
        public GameObject projectile;
        private GameObject gameObject;
        private int counter;

        public Bullet(Rigidbody2D rb2d, Vector2 input, int bulletCount, GameObject projectile)
        {
            gameObject = Instantiate(projectile, rb2d.position, Quaternion.identity) as GameObject;
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            float projectileX = mousePosition.x - rb2d.position.x;
            float projectileY = mousePosition.y - rb2d.position.y;
            float r = Mathf.Sqrt(projectileX * projectileX + projectileY * projectileY);
            Vector2 projectileThrow = new Vector2(projectileX/r, projectileY/r) + input;
            gameObject.GetComponent<Rigidbody2D>().AddForce(projectileThrow * 1000);
            gameObject.name = "bullet" + bulletCount;
        }

        public GameObject GetGameObject()
        {
            return gameObject;
        }

        public int GetCounter()
        {
            return counter;
        }

        public void UpdateCounter()
        {
            counter++;
        }
    }
}
