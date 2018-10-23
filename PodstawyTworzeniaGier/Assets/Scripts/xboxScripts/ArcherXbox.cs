using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherXbox : MinionBaseXbox
{
    public GameObject projectile;
    public int shootCooldown;

    private Dictionary<GameObject, ArrowXbox> arrows;
    private int projectilesCount;
    private float power;
    private int cooldown;
    private bool charging;
    private static float maxPower = 3;

    // Use this for initialization
    void Start()
    {
        Initialise();
        arrows = new Dictionary<GameObject, ArrowXbox>();
        projectilesCount = 0;
        power = 0.5f;
    }

    private void FixedUpdate()
    {
        CallFixedUpdate();
        foreach (ArrowXbox g in arrows.Values)
        {
            g.UpdateCounter();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (cooldown > 0)
        {
            cooldown--;
        }
        else
        {
            if (Input.GetAxis(controller + "T") > 0 && power < maxPower)
            {
                power += 0.05f;
                charging = true;
            }
            if (Input.GetAxis(controller + "T") == 0 && charging)
            {
                GameObject temp = (Instantiate(projectile, transform.position, transform.rotation));
                arrows.Add(temp, temp.GetComponent<ArrowXbox>());
                arrows[temp].Initialise("arrow" + projectilesCount, this, power, controller);
                projectilesCount++;
                power = 0.5f;
                cooldown = shootCooldown;
                charging = false;
            }
        }
    }

    public float GetCooldown()
    {
        return ((float)cooldown) / ((float)shootCooldown);
    }

    public float GetPower()
    {
        return power / maxPower;
    }

    public void ReturnProjectile(GameObject p)
    {
        arrows.Remove(p);
    }
}
