using System.Collections;
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

    private new void FixedUpdate()
    {
        base.FixedUpdate();
        foreach (AxeXbox g in axes.Values)
        {
            g.UpdateCounter();
        }
    }

    void Update()
    {
        if (controller.Shoot() && axes.Count < maxProjectileCount && !hasShot)
        {
            GameObject temp = (Instantiate(projectile, transform.position, transform.rotation));
            axes.Add(temp, temp.GetComponent<AxeXbox>());
            axes[temp].SetPlayerName(playerName);
            axes[temp].Initialise("axe" + projectilesCount, this, controller);
            projectilesCount++;
            hasShot = true;
        }
        if(!controller.Shoot() && hasShot)
        {
            hasShot = false;
        }
    }

    public new void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
        if(collision.gameObject.GetComponent<AxeXbox>())
        {
            if(collision.gameObject.GetComponent<AxeXbox>().GetPlayerName().Equals(playerName) 
                && collision.gameObject.GetComponent<AxeXbox>().GetCounter() < 0)
            {
                ((VikingXbox)(collision.gameObject.GetComponent<ProjectileXbox>().GetPlayer())).ReturnProjectile(collision.gameObject);
                Destroy(collision.gameObject);
                return;
            }
        }
        //Return to avaiable axes if player tag matches
    }

    public void ReturnProjectile(GameObject p)
    {
        axes.Remove(p);
    }
}
