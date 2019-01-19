using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Viking : MinionBase
{
    public GameObject projectile;

    private Dictionary<GameObject, Axe> axes;
    private Horde horde;
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
        List<GameObject> toRemove = new List<GameObject>();
        foreach (Axe g in axes.Values)
        {
            g.UpdateCounter();
        }
        foreach (GameObject g in axes.Keys)
        {
            if(g == null)
            {
                toRemove.Add(g);
            } else
            if(g.GetComponent<Axe>().GetCounter() < 0)
            {
                toRemove.Add(g);
                horde.AddToThrown(g);            
            }
        }
        foreach(GameObject g in toRemove)
        {
            axes.Remove(g);
        }
    }

    void Update()
    {
        if (controller.Shoot() && horde.CanRemoveAxe() && !hasShot)
        {
            GameObject temp = (Instantiate(projectile, transform.position, transform.rotation));
            axes.Add(temp, temp.GetComponent<Axe>());
            axes[temp].SetPlayerName(playerName);
            axes[temp].Initialise("axe" + projectilesCount, this, controller);
            projectilesCount++;
            horde.RemoveAxe();
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
                horde.AddAxe();
                //((Viking)(collision.gameObject.GetComponent<Projectile>().GetPlayer())).ReturnProjectile(collision.gameObject);
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

    public void SetHorde(GameObject g)
    {
        horde = g.GetComponent<Horde>();
        Debug.Log(horde);
    }
}
