using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Viking : MinionBase
{
    public GameObject projectile;
    public int maxProjectileCount;

    private Dictionary<GameObject, Axe> axes;
    private int projectilesCount;
    private bool hasShot;

    // Use this for initialization
    void Start()
    {
        Initialise();
        axes = new Dictionary<GameObject, Axe>();
        projectilesCount = 0;
        hasShot = false;
    }

    private new void FixedUpdate()
    {
        base.FixedUpdate();
        foreach (Axe g in axes.Values)
        {
            g.UpdateCounter();
        }
    }

    void Update()
    {
        if (controller.Shoot() && axes.Count < maxProjectileCount && !hasShot)
        {
            GameObject temp = (Instantiate(projectile, transform.position, transform.rotation));
            axes.Add(temp, temp.GetComponent<Axe>());
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
        if(collision.gameObject.GetComponent<Axe>())
        {
            if(collision.gameObject.GetComponent<Axe>().GetPlayerName().Equals(playerName) 
                && collision.gameObject.GetComponent<Axe>().GetCounter() < 0)
            {
                ((Viking)(collision.gameObject.GetComponent<Projectile>().GetPlayer())).ReturnProjectile(collision.gameObject);
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
