using System.Collections;
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
    public float slow = 1f;
    public int axesPerViking;
    public int axeRespawnRate;
    private int axeCount;
    private LinkedList<GameObject> axes;
    private int axeRespawnCounter = 0;
    //Audio
    [Range(0f,1f)]
    public float volume;
    public List<AudioClip> skillSounds;
    private AudioSource skillSoundSource;
    //public AudioClip skillSound;
    //
    public List<GameObject> minions;
    public List<GameObject> minionsWithChief;
    GameObject chief;

    private Vector2 rightAxisPrevious;

    //divideHorde
    Vector2 divide1, divide2, center;

    //spartan dash
    GameObject[,] pociong;
    void Start()        
    {
        
        axes = new LinkedList<GameObject>();
        switch (deviceSignature)
        {
            case "Joystick1":
            case "Joystick2":
            case "Joystick3":
            case "Joystick4":
                controller = gameObject.AddComponent(typeof(ControllerXbox)) as ControllerXbox;
                break;
            case "":
                controller = gameObject.AddComponent(typeof(ControllerMouseAndKeyboard)) as ControllerMouseAndKeyboard;
                break;
        }

        skillSoundSource = GetComponent<AudioSource>();

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
        //Debug.Log("horde dziecki: " + transform.childCount);
        //foreach (var NTC in GetComponents<NetworkTransformChild>())
        //{
        //    int counter = 0;
        //    NTC.target = minionsWithChief[counter].transform;
        //    counter += 1;

        //}
        //gameObject.GetComponent<NetworkTransformChild>().target = chief.transform;

        minionsWithChief.ForEach(m => m.name = hordeName);
        chief.GetComponent<Chief>().SetController(controller);
        if (chief.GetComponent<ChiefCactus>())
        {
            chief.GetComponent<ChiefCactus>().SetController(controller);
        }
        if (chief.GetComponent<vaperScript>())
        {
            chief.GetComponent<vaperScript>().SetController(controller);
        }
        if (chief.GetComponent<santaScript>())
        {
            chief.GetComponent<santaScript>().SetController(controller);
        }
        chief.GetComponent<ChiefBase>().SetPlayerName(playerName);
        minions.ForEach(m => m.GetComponent<MinionBase>().SetController(controller));
        minions.ForEach(m => m.GetComponent<MinionBase>().SetChief(chief));
        minions.ForEach(m => m.GetComponent<MinionBase>().SetPlayerName(playerName));
        if (minions[0].GetComponent<ZombieScript>())
        {
            isZombie = true;
        }

        pociong = new GameObject[4, 100];

        if(minions[0].GetComponent<Viking>())
        {
            foreach (GameObject g in minions)
            {
                g.GetComponent<Viking>().SetHorde(gameObject);
            }
        }
        axeCount = minions.Count * axesPerViking;
    }

    // Update is called once per frame
    void Update()
    {
        minionsWithChief = minionsWithChief.FindAll(m => m != null);
        minions = minions.FindAll(m => m != null);
        if (minions.Count <= 0)
        {
            switch(playerName)
            {
                case "Player1":
                    FindObjectOfType<SpawnControll>().RespawnPlayer1(gameObject);
                    break;
                case "Player2":
                    FindObjectOfType<SpawnControll>().RespawnPlayer2(gameObject);
                    break;
                case "Player3":
                    break;
                case "Player4":
                    break;
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
                            if (r < 25)
                                obj.GetComponent<Rigidbody2D>().AddForce(force);
                    }
                if (!isSnowBall)
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
                                force.Set(-dx / r * minionsK / r2 * (obj.GetComponent<CircleCollider2D>().radius / 1.4f), -dy / r * minionsK / r2 * (obj.GetComponent<CircleCollider2D>().radius / 1.4f));
                                if (!float.IsNaN(force.x) && !float.IsNaN(force.y))
                                    obj.GetComponent<Rigidbody2D>().AddForce(force);
                            }
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
                chief.GetComponent<Rigidbody2D>().velocity = new Vector2(moveX * maxSpeed * slow, moveY * maxSpeed * slow);
            else
                center = new Vector2(center.x + moveX * maxSpeed * Time.deltaTime,
                   center.y + moveY * maxSpeed * Time.deltaTime);

            if (controller.Special1())
            {
                if (minions.Count > 0)
                {
                    if (minions[0].GetComponent<Viking>())
                    {
                        if (dashCooldownTimer <= 0)
                        {
                            skillSoundSource.PlayOneShot(skillSounds[Random.Range(0,skillSounds.Count)],volume);
                            dashForce = 2000;
                            dashCooldownTimer = dashCooldown;
                        }
                    }
                    else if (minions[0].GetComponent<Archer>())
                    {
                        if (divideCooldownTimer <= 0)
                        {
                            
                            divide = 12;
                            if (isZombie) divide = 25;
                            center = chief.transform.position;
                            divideCooldownTimer = divideCooldown;
                            skillSoundSource.PlayOneShot(skillSounds[Random.Range(0, skillSounds.Count)], volume);
                        }
                    }
                    else if (minions[0].GetComponent<ZombieScript>())
                    {
                        if (divideCooldownTimer <= 0)
                        {
                            skillSoundSource.PlayOneShot(skillSounds[Random.Range(0, skillSounds.Count)], volume);
                            divide = 12;
                            if (isZombie) divide = 25;
                            center = chief.transform.position;
                            divideCooldownTimer = divideCooldown;
                        }
                    }
                    else if (minions[0].GetComponent<Spartan>())
                    {
                        if (spartanTimer <= 0)
                        {
                            skillSoundSource.PlayOneShot(skillSounds[Random.Range(0, skillSounds.Count)], volume);
                            spartanX = chief.transform.position.x + controller.LookHorizontal() * 15;
                            spartanY = chief.transform.position.y + controller.LookVertical() * 15;
                            isSpartanDash = true;
                            spartanTimer = spartanCooldown;
                        }
                    }
                }
            }

            if (isSnowBall)
            {
                snowBall();
            }
            else
            {
                foreach (GameObject obj in minionsWithChief)
                {
                    obj.GetComponent<CircleCollider2D>().radius += Time.deltaTime / 10;
                    obj.GetComponent<CircleCollider2D>().radius *= 1.05f;
                    if (obj.GetComponent<CircleCollider2D>().radius > 1.4)
                    {
                        obj.GetComponent<CircleCollider2D>().enabled = true;
                        obj.GetComponent<CircleCollider2D>().radius = 1.4f;
                    }
                }
            }

            //spartan
            spartanTimer -= Time.deltaTime;
            if (spartanTimer <= spartanCooldown - spartanTime)
            {
                isSpartanDash = false;
            }
            if (isSpartanDash)
            {
                Debug.Log("SPARTAN DASH");
                spartanDash();
            }

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

            if(minions.Count > 0) {
                if (minions[0].GetComponent<Viking>())
                {
                    foreach (GameObject g in axes)
                    {
                        if (g == null)
                        {

                        }
                    }
                    //Remove additional axes
                    while (axes.Count + axeCount > minions.Count * axesPerViking)
                    {
                        List<GameObject> toRemove = new List<GameObject>();
                        foreach (GameObject g in axes)
                        {
                            if (g == null)
                            {
                                toRemove.Add(g);
                            }
                        }
                        foreach (GameObject g in toRemove)
                        {
                            axes.Remove(g);
                        }
                        if (axes.Count > 0)
                        {
                            GameObject g = axes.First.Value;
                            axes.RemoveFirst();
                            Destroy(g);
                        }
                        else
                        {
                            axeCount--;
                        }
                    }
                }
            }
        }
    }

    public void FixedUpdate()
    {
        axeRespawnCounter++;
        if (minions[0].GetComponent<Viking>())
        {
            //respawn axes
            if (axeRespawnCounter % axeRespawnRate == 0)
            {
                AddAxe();
            }
        }
    }

    bool isSpartanDash = false;
    public float spartanCooldown = 5;
    float spartanTimer = 0;
    public float spartanTime = 2;

    public float dashCooldown = 1;
    float dashCooldownTimer = 0;
    float dashForce;
    float dashX = 0, dashY = 0;
    public void dash()
    {
        float dx, dy, r2, r3;
        foreach (GameObject obj in minionsWithChief)
        {
            dx = obj.transform.position.x - chief.transform.position.x;
            dy = obj.transform.position.y - chief.transform.position.y;
            r2 = dx * dx + dy * dy;
            r3 = Mathf.Sqrt(r2);
            if (r3 > 25)
            {
                continue;
            }

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

    float spartanX, spartanY;
    Vector2 spforce = new Vector2();
    public void spartanDash()
    {
        float dx, dy, r2, r;
        foreach (GameObject obj in minionsWithChief)
        {
            dx = obj.transform.position.x - chief.transform.position.x;
            dy = obj.transform.position.y - chief.transform.position.y;
            r2 = dx * dx + dy * dy;
            r = Mathf.Sqrt(r2);
            if (r > 25)
            {
                continue;
            }

            /*dx = spartanX - (obj.transform.position.x);
            dy = spartanY - (obj.transform.position.y);
            r = Mathf.Sqrt(dx * dx + dy * dy + 0.1f);
            spforce.Set(spartanX*800, spartanY*800);
            //obj.transform.Translate(spforce);
            //spforce.Set(dx, dy);
            obj.GetComponent<Rigidbody2D>().AddForce(spforce);*/

            dx = spartanX - obj.transform.position.x;
            dy = spartanY - obj.transform.position.y;
            r2 = dx * dx + dy * dy;
            r = Mathf.Sqrt(r2 + 5);
            spforce.Set(dx * minionsKS * 0.5f, dy * minionsKS * 0.5f);
            if (!float.IsNaN(spforce.x) && !float.IsNaN(spforce.y))
                obj.GetComponent<Rigidbody2D>().AddForce(spforce);
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
            divide2 += new Vector2(center.x, center.y) * 3;
        }
        divide1 /= 10;
        divide2 /= 10;

        float dx, dy, r, r2, r21, r22;
        Vector2 force = new Vector2();
        int i = 1;
        foreach (GameObject obj in minionsWithChief)
        {
            dx = obj.transform.position.x - chief.transform.position.x;
            dy = obj.transform.position.y - chief.transform.position.y;
            r2 = dx * dx + dy * dy;
            r = Mathf.Sqrt(r2);
            if (r > 25)
            {
                continue;
            }

            if (i == 1) d = divide1;
            else d = divide2;
            if (!isZombie)
            {
                i *= -1;
            }
            else
            {
                i += 1;
                if (i > 3) i = 1;
            }

            if (!isZombie)
            {
                d = divide1;
                dx = d.x - obj.transform.position.x;
                dy = d.y - obj.transform.position.y;
                r21 = dx * dx + dy * dy;
                d = divide2;
                dx = d.x - obj.transform.position.x;
                dy = d.y - obj.transform.position.y;
                r22 = dx * dx + dy * dy;

                if (r21 > r22)
                {
                    d = divide2;
                }
                else
                {
                    d = divide1;
                }
            }
            else
            {
                d = divide1;
                dx = d.x - obj.transform.position.x;
                dy = d.y - obj.transform.position.y;
                r21 = dx * dx + dy * dy;
                d = divide2;
                dx = d.x - obj.transform.position.x;
                dy = d.y - obj.transform.position.y;
                r22 = dx * dx + dy * dy;

                if (r21 / 6f > r22)
                {
                    d = divide2;
                }
                else
                {
                    d = divide1;
                }
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
    public void snowBallOnOff(bool v)
    {
        isSnowBall = v;
        Debug.Log("horde snowball: " + isSnowBall);
        if (isSnowBall)
        {
            foreach (GameObject obj in minions)
            {
                obj.GetComponent<CircleCollider2D>().enabled = false;
            }
        }
        else
        {
            float dx, dy;
            foreach (GameObject obj in minionsWithChief)
            {
                //obj.GetComponent<CircleCollider2D>().enabled = true;
                obj.GetComponent<CircleCollider2D>().radius = 0f;
                dx = chief.transform.position.x + Random.Range(-1000, 1000) - obj.transform.position.x;
                dy = chief.transform.position.y + Random.Range(-1000, 1000) - obj.transform.position.y;
                obj.GetComponent<Rigidbody2D>().AddForce(new Vector3(-dx, -dy, obj.transform.position.z));
            }
        }
    }
    bool isSnowBall = false;
    public float snowBallR = 0.2f;
    public void snowBall()
    {
        Debug.Log("horde snowball");
        float dx, dy, rx = 0, ry = 0;
        foreach (GameObject minion in minions)
        {
            rx = 0; ry = 0;
            dx = chief.transform.position.x + Random.Range(-1, 1) - minion.transform.position.x;
            dy = chief.transform.position.y + Random.Range(-1, 1) - minion.transform.position.y;
            minion.GetComponent<Rigidbody2D>().AddForce(new Vector3(chief.transform.position.x + rx, chief.transform.position.y + ry, minion.transform.position.z));

        }
    }

    #region getters and setters
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

    public int GetMinionsCount()
    {
        return minions.Count;
    }
    #endregion

    #region axe management
    public void AddAxe()
    {
        if(axeCount <  axesPerViking * minions.Count)
        {
            axeCount++;
        }
    }

    public bool CanRemoveAxe()
    {
        if (axeCount > 0)
        {
            return true;
        }
        return false;
    }

    public void RemoveAxe()
    {
        if(axeCount > 0)
        {
            axeCount--;
        }
    }

    public void AddToThrown(GameObject axe)
    {
        axes.AddLast(axe);
    }

    public int GetAxeCount()
    {
        return axeCount;
    }
    #endregion
}
