﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherXbox : MinionBase
{
    public GameObject projectile;
    public int shootCooldown;

    private Dictionary<GameObject, ArrowXbox> arrows;
    private int projectilesCount;
    private float power;
    private int cooldown;

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
            if (Input.GetButton(controller + "X") && power < 3)
            {
                power += 0.05f;
            }
            if (Input.GetButtonUp(controller + "X"))
            {
                GameObject temp = (Instantiate(projectile, transform.position, transform.rotation));
                arrows.Add(temp, temp.GetComponent<ArrowXbox>());
                arrows[temp].Initialise("arrow" + projectilesCount, this, power);
                projectilesCount++;
                power = 0.5f;
                cooldown = shootCooldown;
            }
        }
    }

    public void ReturnProjectile(GameObject p)
    {
        arrows.Remove(p);
    }
}