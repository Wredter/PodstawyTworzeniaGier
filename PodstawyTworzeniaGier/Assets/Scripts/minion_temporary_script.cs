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
    private int maxBulletCount = 3;
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
        rb2d.velocity = new Vector2(input.x * moveSpeed, input.y * moveSpeed);
        bullets.ForEach(i => i.UpdateCounter());
    }

    // Update is called once per frame
    void Update()
    {
        input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        
        if (Input.GetKeyDown(KeyCode.Space) && bullets.Count < maxProjectileCount)
        {
            bullets.Add(new Bullet(transform, input, bulletCount, projectile));
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

        public Bullet(Transform transform, Vector2 input, int bulletCount, GameObject projectile)
        {
            gameObject = Instantiate(projectile, transform.position, Quaternion.identity) as GameObject;
            gameObject.GetComponent<Rigidbody2D>().AddForce(input * 1000);
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
