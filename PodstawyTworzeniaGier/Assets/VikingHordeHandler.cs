using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VikingHordeHandler : MonoBehaviour {

    private List<GameObject> minions;
    private LinkedList<GameObject> axes;
    public int axesPerViking;
    public int axeRespawnRate;
    private int axeCount;

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

    public void SetMinions(List<GameObject> minions)
    {
        this.minions = minions;
    }
}
