using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VikingXbox : MinionBase
{
    public GameObject projectile;
    public int maxProjectileCount;

    private Dictionary<GameObject, AxeXbox> axes;
    private int projectilesCount;

    // Use this for initialization
    void Start()
    {
        Initialise();
        axes = new Dictionary<GameObject, AxeXbox>();
        projectilesCount = 0;
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
        if (Input.GetButtonDown(controller + "X") && axes.Count < maxProjectileCount)
        {
            GameObject temp = (Instantiate(projectile, transform.position, transform.rotation));
            axes.Add(temp, temp.GetComponent<AxeXbox>());
            axes[temp].Initialise("axe" + projectilesCount, this, input);
            projectilesCount++;
        }
    }

    public void ReturnProjectile(GameObject p)
    {
        axes.Remove(p);
    }
}
