using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HordeXbox : MonoBehaviour
{
    public string hordeName;
    public string controller;
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

    //divideHorde
    Vector2 divide1, divide2, center;

    // Use this for initialization
    void Start()
    {
        divide1 = new Vector2();
        divide2 = new Vector2();
        minions = new List<GameObject>();
        minionsWithChief = new List<GameObject>();

        GameObject obj = Instantiate(hordeChief, transform.position, Quaternion.identity, gameObject.transform);

        chief = obj;
        minionsWithChief.Add(chief);
        //gameObject.AddComponent<NetworkTransformChild>();

        for (int i = 0; i < minionsNumber; i++)
        {
            float radius = Mathf.PI * 2 / minionsNumber * i;
            obj = Instantiate(hordeMinion, transform.position + new Vector3(spawnRadius * Mathf.Cos(radius) + Random.value * random, spawnRadius * Mathf.Sin(radius) + Random.value * random, 0), Quaternion.identity, gameObject.transform);
            minions.Add(obj);
            minionsWithChief.Add(obj);

            //gameObject.AddComponent<NetworkTransformChild>();

        }
        Debug.Log("horde dziecki: " + transform.childCount);
        //foreach (var NTC in GetComponents<NetworkTransformChild>())
        //{
        //    int counter = 0;
        //    NTC.target = minionsWithChief[counter].transform;
        //    counter += 1;

        //}
        //gameObject.GetComponent<NetworkTransformChild>().target = chief.transform;

        minionsWithChief.ForEach(m => m.name = hordeName);
        chief.GetComponent<ChiefXbox>().SetController(controller);
        minions.ForEach(m => m.GetComponent<MinionBaseXbox>().SetController(controller));
    }

    // Update is called once per frame
    void Update()
    {
        minionsWithChief = minionsWithChief.FindAll(m => m != null);
        minions = minions.FindAll(m => m != null);

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
            float maxForce = 1500;
            if (divide <= 0)
                foreach (GameObject obj in minions)
                {
                    dx = chief.transform.position.x - obj.transform.position.x;
                    dy = chief.transform.position.y - obj.transform.position.y;
                    r2 = dx * dx + dy * dy;
                    r = Mathf.Sqrt(r2 + 10);
                    force.Set(dx * minionsKS, dy * minionsKS);
                    float forceR = Mathf.Sqrt(force.x * force.x + force.y * force.y);
                    if (forceR > maxForce)
                    {
                        force.Set(force.x/ forceR * maxForce, force.y / forceR * maxForce);
                    }
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
                        r = Mathf.Sqrt(r2 + 10);
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

            float moveX = Input.GetAxis(controller + "LeftHorizontal");
            float moveY = Input.GetAxis(controller + "LeftVertical");

            bool a, d, w, s;
            a = Input.GetKey(KeyCode.A);
            d = Input.GetKey(KeyCode.D);
            s = Input.GetKey(KeyCode.S);
            w = Input.GetKey(KeyCode.W);
            if (a || d || s || w)
            {
                moveX = 0;
                moveY = 0;
                if (a) moveX -= 1;
                if (d) moveX += 1;
                if (s) moveY -= 1;
                if (w) moveY += 1;
            }

            if (divide <= 0)
                chief.GetComponent<Rigidbody2D>().velocity = new Vector2(moveX * maxSpeed, moveY * maxSpeed);
            else
                center = new Vector2(center.x + moveX * maxSpeed * Time.deltaTime,
                   center.y + moveY * maxSpeed * Time.deltaTime);

            //dash
            dashCooldownTimer -= Time.deltaTime;
            dashForce -= Time.deltaTime * 2000;
            if (dashForce < 0)
            {
                dashForce = 0;
                dashX = 0;
                dashY = 0;
            }
            dash();
            if (Input.GetButtonDown(controller + "RB") || Input.GetKeyDown(KeyCode.F))
            {
                if (dashCooldownTimer <= 0)
                {
                    dashForce = 2000;
                    dashCooldownTimer = dashCooldown;
                }
            }

            //divide
            divideCooldownTimer -= Time.deltaTime;
            divide -= Time.deltaTime * 5;
            if (divide < 0) divide = 0;

            if (divide > 0) divideHorde();
            else { divideX = 0; divideY = 0; }

            if (Input.GetButtonDown(controller + "LB") || Input.GetKeyDown(KeyCode.E))
            {
                if (divideCooldownTimer <= 0)
                {
                    divide = 10;
                    center = chief.transform.position;
                    divideCooldownTimer = divideCooldown;
                }


            }
        }
    }

    public float dashCooldown = 1;
    float dashCooldownTimer = 0;
    float dashForce;
    float dashX = 0, dashY = 0;
    public void dash()
    {
        foreach (GameObject obj in minionsWithChief)
        {
            Rigidbody2D rb2d = obj.GetComponent<Rigidbody2D>();
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            dashX *= 999;
            dashY *= 999;

            dashX += Input.GetAxis(controller + "RightHorizontal");
            dashY += Input.GetAxis(controller + "RightVertical");

            //dashX += (mousePosition.x - chief.transform.position.x)*1;
            //dashY += (mousePosition.y - chief.transform.position.y)*1;

            dashX /= 1000;
            dashY /= 1000;
            float r = Mathf.Sqrt(dashX * dashX + dashY * dashY);
            Vector2 projectileThrow = new Vector2(dashX/r, dashY/r);
            rb2d.AddForce(projectileThrow * dashForce);
        }
    }

    public float divideCooldown = 1;
    float divideCooldownTimer = 0;
    float divide;
    float divideX = 0, divideY = 0;
    public void divideHorde()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        divideX *= 29;
        divideY *= 29;

        divideX += Input.GetAxis(controller + "RightHorizontal");
        divideY += Input.GetAxis(controller + "RightVertical");
        //divideX += mousePosition.x - chief.transform.position.x;
        //divideY += mousePosition.y - chief.transform.position.y;

        divideX /= 30;
        divideY /= 30;
        float angle = Mathf.Deg2Rad * Vector2.SignedAngle(Vector2.up, new Vector2(divideX, divideY));


        Vector2 d;
        divide1 *= 6;
        divide2 *= 6;
        divide1 += new Vector2(divide * Mathf.Cos(angle) + center.x, divide * Mathf.Sin(angle) + center.y)*4;
        divide2 += new Vector2(-divide * Mathf.Cos(angle) + center.x, -divide * Mathf.Sin(angle) + center.y)*4;
        divide1 /= 10;
        divide2 /= 10;
        float dx, dy, r, r2;
        Vector2 force = new Vector2();
        int i = 1;
        foreach (GameObject obj in minionsWithChief)
        {
            if (i == 1) d = divide1;
            else d = divide2;
            i *= -1;

            dx = d.x - obj.transform.position.x;
            dy = d.y - obj.transform.position.y;
            r2 = dx * dx + dy * dy;
            r = Mathf.Sqrt(r2 + 5);
            force.Set(dx * minionsKS * 1f, dy * minionsKS * 1f);
            obj.GetComponent<Rigidbody2D>().AddForce(force);
        }
    }
    public Vector2 GetChiefPosition()
    {
        return chief.GetComponent<Rigidbody2D>().position;
    }
}
