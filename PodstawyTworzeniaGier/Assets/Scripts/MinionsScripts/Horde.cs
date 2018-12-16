﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Horde : MonoBehaviour, IPlayerIntegration
{
    [Range(0.01f, 0.99f)]
    public float viewSmoothness;
    public string hordeName;
    public GameObject hordeChief;
    public GameObject hordeMinion;
    public int minionsNumber = 30;
    public float spawnRadius = 5;
    public float minionsKS = 280;
    public float minionsK = 100;
    public float maxSpeed = 10;
    public float random = 0.01f;
    protected string deviceSignature;
    protected IController controller;
    protected string playerName;
    public bool isZombie;

    public List<GameObject> minions;
    public List<GameObject> minionsWithChief;
    GameObject chief;

    private Vector2 rightAxisPrevious;

    //divideHorde
    Vector2 divide1, divide2, center;

    void Start()
    {
        switch (deviceSignature)
        {
            case "Joystick1":
            case "Joystick2":
                controller = gameObject.AddComponent(typeof(ControllerXbox)) as ControllerXbox;
                break;
            case "":
                controller = gameObject.AddComponent(typeof(ControllerMouseAndKeyboard)) as ControllerMouseAndKeyboard;
                break;
        }

        controller.SetDeviceSignature(deviceSignature);
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
        chief.GetComponent<Chief>().SetController(controller);
        chief.GetComponent<ChiefBase>().SetPlayerName(playerName);
        minions.ForEach(m => m.GetComponent<MinionBase>().SetController(controller));
        minions.ForEach(m => m.GetComponent<MinionBase>().SetChief(chief));
        minions.ForEach(m => m.GetComponent<MinionBase>().SetPlayerName(playerName));
        if (minions[0].GetComponent<ZombieScript>())
        {
            isZombie = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        minionsWithChief = minionsWithChief.FindAll(m => m != null);
        minions = minions.FindAll(m => m != null);
        if (minions.Count <= 0)
        {
            if (playerName == "Player1")
            {
                SceneManager.LoadScene("Player2Won");


            }
            else
            {
                SceneManager.LoadScene("Player1Won");
            }
        }

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
                        force.Set(force.x / forceR * maxForce, force.y / forceR * maxForce);
                    }
                    if (!float.IsNaN(force.x) && !float.IsNaN(force.y))
                        if(r < 15)
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
                        if (!float.IsNaN(force.x) && !float.IsNaN(force.y))
                            obj.GetComponent<Rigidbody2D>().AddForce(force);
                    }
                }

            }

            foreach (GameObject obj in minionsWithChief)
            {
                obj.GetComponent<Rigidbody2D>().angularVelocity = 0;
                Vector2 velocity = obj.GetComponent<Rigidbody2D>().velocity;
                if (velocity.x * velocity.x + velocity.y * velocity.y > 16)
                {
                    obj.transform.eulerAngles = new Vector3(0, 0, Vector2.SignedAngle(Vector2.up, velocity));
                }
            }

            float moveX = controller.MoveHorizontal();
            float moveY = controller.MoveVertical();

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
            if (controller.Special2() || Input.GetKeyDown(KeyCode.F))
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
            if (isZombie)
            {
                divide -= Time.deltaTime * 25;
            }
            if (divide < 0) divide = 0;

            if (divide > 0) divideHorde();
            else { divideX = 0; divideY = 0; }
            if (controller.Special1() || Input.GetKeyDown(KeyCode.E))
            {
                if (divideCooldownTimer <= 0)
                {
                    divide = 12;
                    if (isZombie) divide = 25;
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

            dashX += controller.LookHorizontal();
            dashY += controller.LookVertical();

            //dashX += (mousePosition.x - chief.transform.position.x)*1;
            //dashY += (mousePosition.y - chief.transform.position.y)*1;

            dashX /= 1000;
            dashY /= 1000;
            float r = Mathf.Sqrt(dashX * dashX + dashY * dashY);
            Vector2 projectileThrow = new Vector2(dashX / r, dashY / r);
            if (!float.IsNaN(projectileThrow.x) && !float.IsNaN(projectileThrow.y))
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

        divideX += controller.LookHorizontal();
        divideY += controller.LookVertical();
        //divideX += mousePosition.x - chief.transform.position.x;
        //divideY += mousePosition.y - chief.transform.position.y;

        divideX /= 30;
        divideY /= 30;
        float angle = Mathf.Deg2Rad * Vector2.SignedAngle(Vector2.up, new Vector2(divideX, divideY));
        if (isZombie)
        {
            angle = Mathf.Deg2Rad * Vector2.SignedAngle(Vector2.right, new Vector2(divideX, divideY));
        }

        Vector2 d;
        divide1 *= 7;
        divide2 *= 7;

        float distance = 3;

        divide1 += new Vector2(divide * Mathf.Cos(angle) + center.x, divide * Mathf.Sin(angle) + center.y) * distance;
        if (!isZombie)
        {
            divide2 += new Vector2(-divide * Mathf.Cos(angle) + center.x, -divide * Mathf.Sin(angle) + center.y) * distance;
        }
        else
        {
            divide2 += new Vector2( center.x, center.y) * 3;
        }
        divide1 /= 10;
        divide2 /= 10;

        float dx, dy, r, r2;
        Vector2 force = new Vector2();
        int i = 1;
        foreach (GameObject obj in minionsWithChief)
        {
            if (i == 1) d = divide1;
            else d = divide2;
            if (!isZombie)
            {
                i *= -1;
            } else
            {
                i += 1;
                if (i > 3) i = 1;
            }


            dx = d.x - obj.transform.position.x;
            dy = d.y - obj.transform.position.y;
            r2 = dx * dx + dy * dy;
            r = Mathf.Sqrt(r2 + 5);
            force.Set(dx * minionsKS * 1f, dy * minionsKS * 1f);
            if (!float.IsNaN(force.x) && !float.IsNaN(force.y))
                obj.GetComponent<Rigidbody2D>().AddForce(force);
        }
    }

    public float GetChargeCooldown()
    {
        return 1;
    }

    public float GetChargeActualValue()
    {
        return dashCooldownTimer;
    }
    public Vector2 GetHordeCenter()
    {
        Vector2 average = new Vector2();
        minionsWithChief.ForEach(m => average += m.GetComponent<Rigidbody2D>().position);
        average /= minionsWithChief.Count;
        Vector2 pom = new Vector2(controller.LookHorizontal(), controller.LookVertical()) * 3;
        Vector2 next = pom * (1 - viewSmoothness) + rightAxisPrevious * viewSmoothness;
        rightAxisPrevious = next;
        return next + average;
    }

    public float GetCooldown()
    {
        if (minions.FindAll(m => m != null).Count > 0)
        {
            if (minions[0].GetComponent<Archer>())
            {
                return minions[0].GetComponent<Archer>().GetCooldown();
            }
        }
        return 0;
    }

    public float GetPower()
    {
        if (minions.FindAll(m => m != null).Count > 0)
        {
            if (minions[0].GetComponent<Archer>())
            {
                return minions[0].GetComponent<Archer>().GetPower();
            }
        }
        return 0;
    }

    public void SetDeviceSignature(string deviceSignature)
    {
        this.deviceSignature = deviceSignature;
    }

    public string GetPlayerName()
    {
        return playerName;
    }

    public void SetPlayerName(string playerName)
    {
        this.playerName = playerName;
    }
}