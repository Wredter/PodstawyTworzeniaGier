﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Archer : MinionBase
{
    public GameObject projectile;
    public int shootCooldown;

    private Dictionary<GameObject, Arrow> arrows;
    private int projectilesCount;
    private float power;
    private int cooldown;

    // Use this for initialization
    void Start () {
        Initialise();
        arrows = new Dictionary<GameObject, Arrow>();
        projectilesCount = 0;
        power = 0.5f;
    }

    private new void FixedUpdate()
    {
        base.FixedUpdate();
        foreach (Arrow g in arrows.Values)
        {
            g.UpdateCounter();
        }
        if (cooldown > 0)
        {
            cooldown--;
        }
        else
        {
            if (Input.GetKey(KeyCode.Space) && power < 3)
            {
                power += 0.05f;
            }
            if (Input.GetKeyUp(KeyCode.Space))
            {
                GameObject temp = (Instantiate(projectile, transform.position, transform.rotation));
                arrows.Add(temp, temp.GetComponent<Arrow>());
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
