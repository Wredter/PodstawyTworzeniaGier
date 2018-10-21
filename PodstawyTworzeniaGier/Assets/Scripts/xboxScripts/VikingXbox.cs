﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VikingXbox : MinionBaseXbox
{
    public GameObject projectile;
    public int maxProjectileCount;

    private Dictionary<GameObject, AxeXbox> axes;
    private int projectilesCount;
    private bool hasShot;

    // Use this for initialization
    void Start()
    {
        Initialise();
        axes = new Dictionary<GameObject, AxeXbox>();
        projectilesCount = 0;
        hasShot = false;
    }

    private void FixedUpdate()
    {
        CallFixedUpdate();
        foreach (AxeXbox g in axes.Values)
        {
            g.UpdateCounter();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis(controller + "T") > 0 && axes.Count < maxProjectileCount && !hasShot)
        {
            GameObject temp = (Instantiate(projectile, transform.position, transform.rotation));
            axes.Add(temp, temp.GetComponent<AxeXbox>());
            axes[temp].Initialise("axe" + projectilesCount, this, controller);
            projectilesCount++;
            hasShot = true;
        }
        if(Input.GetAxis(controller + "T") == 0 && hasShot)
        {
            hasShot = false;
        }
    }

    public void ReturnProjectile(GameObject p)
    {
        axes.Remove(p);
    }
}
