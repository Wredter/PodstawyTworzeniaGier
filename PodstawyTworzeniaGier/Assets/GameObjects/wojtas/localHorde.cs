using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class localHorde : MonoBehaviour
{
    public GameObject hordeChief;
    public GameObject hordeMinion;
    public int minionsNumber = 30;
    public float spawnRadius = 5;
    public float minionsKS = 280;
    public float minionsK = 100;
    public float maxSpeed = 10;
    public float random = 0.01f;

    List<GameObject> minions;
    List<GameObject> minionsWithChief;
    GameObject chief;


    // Use this for initialization
    void Start()
    {
        minions = new List<GameObject>();
        minionsWithChief = new List<GameObject>();
        
        GameObject obj = Instantiate(hordeChief, transform.position, Quaternion.identity, gameObject.transform);

        chief = obj;
        minionsWithChief.Add(chief);

        for (int i = 0; i < minionsNumber; i++)
        {
            float radius = Mathf.PI * 2 / minionsNumber * i;
            obj = Instantiate(hordeMinion, transform.position + new Vector3(spawnRadius*Mathf.Cos(radius) + Random.value * random, spawnRadius * Mathf.Sin(radius) + Random.value * random, 0), Quaternion.identity, gameObject.transform);
            minions.Add(obj);
            minionsWithChief.Add(obj);
        }
        Debug.Log("horde dziecki: " + transform.childCount);
        
    }

    // Update is called once per frame
    void Update()
    {

        if (chief == null)
        {
            if (transform.childCount > 0)
            {
                chief = transform.GetChild(0).gameObject;
                minionsWithChief.Add(chief);

                for (int i = 1; i < transform.childCount; i++)
                {
                    minions.Add(transform.GetChild(i).gameObject);
                    minionsWithChief.Add(transform.GetChild(i).gameObject);
                }
            }

        }
        else
        {

            Vector2 force = new Vector2();
            float dx, dy, r2, r;
            foreach (GameObject obj in minions)
            {
                dx = chief.transform.position.x - obj.transform.position.x;
                dy = chief.transform.position.y - obj.transform.position.y;
                r2 = dx * dx + dy * dy;
                r = Mathf.Sqrt(r2);
                force.Set(dx * minionsKS, dy * minionsKS);
                obj.GetComponent<Rigidbody2D>().AddForce(force);
            }

            foreach (GameObject obj in minions)
            {
                foreach (GameObject obj2 in minionsWithChief)
                {
                    if (obj != obj2)
                    {
                        dx = obj2.transform.position.x - obj.transform.position.x;
                        dy = obj2.transform.position.y - obj.transform.position.y;
                        r2 = dx * dx + dy * dy;
                        r = Mathf.Sqrt(r2);
                        force.Set(-dx / r * minionsK / r2, -dy / r * minionsK / r2);
                        obj.GetComponent<Rigidbody2D>().AddForce(force);
                    }
                }

            }

            foreach (GameObject obj in minionsWithChief)
            {
                Vector2 velocity = obj.GetComponent<Rigidbody2D>().velocity;
                if (velocity.x * velocity.x + velocity.y * velocity.y > 16)
                {
                    obj.transform.eulerAngles = new Vector3(0, 0, Vector2.SignedAngle(Vector2.up, velocity));
                }
            }

            float moveX = Input.GetAxis("Horizontal");
            float moveY = Input.GetAxis("Vertical");

            chief.GetComponent<Rigidbody2D>().velocity = new Vector2(moveX * maxSpeed, moveY * maxSpeed);


            dashCooldownTimer -= Time.deltaTime;
            dashForce -= Time.deltaTime * 300;
            if (dashForce < 0) dashForce = 0;

            dash();

            if (Input.GetKeyDown(KeyCode.F)) {
                if (dashCooldownTimer <= 0)
                {
                    dashForce = 500;
                    dashCooldownTimer = dashCooldown;
                }
                

            }
        }
    }

    public float dashCooldown = 1;
    float dashCooldownTimer = 0;
    float dashForce;
    public void dash()
    {
        foreach (GameObject obj in minionsWithChief)
        {
            Rigidbody2D rb2d = obj.GetComponent<Rigidbody2D>();
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            float projectileX = mousePosition.x - rb2d.position.x;
            float projectileY = mousePosition.y - rb2d.position.y;
            float r = Mathf.Sqrt(projectileX * projectileX + projectileY * projectileY);
            Vector2 projectileThrow = new Vector2(projectileX / r, projectileY / r);
            rb2d.AddForce(projectileThrow * dashForce);
        }
    }
}
