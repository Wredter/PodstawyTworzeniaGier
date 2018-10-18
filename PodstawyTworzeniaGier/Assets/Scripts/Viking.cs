using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Viking : MinionBase
{
    public GameObject projectile;
    public int maxProjectileCount;

    private Dictionary<GameObject, Axe> axes;
    private int projectilesCount;

    // Use this for initialization
    void Start()
    {
        Initialise();
        axes = new Dictionary<GameObject, Axe>();
        projectilesCount = 0;
    }

    private void FixedUpdate()
    {
        callFixedUpdate();
        foreach(Axe g in axes.Values)
        {
            g.UpdateCounter();
        }
    }

    // Update is called once per frame
    void Update()
    {        
        if (Input.GetKeyDown(KeyCode.Space) && axes.Count < maxProjectileCount)
        {
            GameObject temp = (Instantiate(projectile, transform.position, transform.rotation));
            axes.Add(temp, temp.GetComponent<Axe>());
            axes[temp].Initialise("axe" + projectilesCount, this, input);
            projectilesCount++;
        }
    }

    public void ReturnProjectile(GameObject p)
    {
        axes.Remove(p);
    }
}
