using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VikingHordeHandler : MonoBehaviour {

    private List<GameObject> minions;
    private LinkedList<GameObject> axes;
    public int axesPerViking;
    public int axeRespawnRate;
    private int axeCount;
    private int axeRespawnCounter = 0;

    void Start () {
        axes = new LinkedList<GameObject>();
        axeCount = minions.Count * axesPerViking;
    }
	
	// Update is called once per frame
	void Update () {


        if (minions.Count > 0)
        {
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

    public void FixedUpdate()
    {
        axeRespawnCounter++;
        if (minions.Count > 0)
        {
            minions = minions.FindAll(m => m != null);
            if (minions.Count > 0)
                if (minions[0].GetComponent<Viking>())
                {
                    //respawn axes
                    if (axeRespawnCounter % axeRespawnRate == 0)
                    {
                        AddAxe();
                    }
                }
        }
    }

    #region axe management
    public void AddAxe()
    {
        if (axeCount < axesPerViking * minions.Count)
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
        if (axeCount > 0)
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

    public void SetMinions(List<GameObject> minions)
    {
        this.minions = minions;
    }
}
