using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class vaperScript : MonoBehaviour {
    IController controller;
    public void SetController(IController controller)
    {
        this.controller = controller;
    }

    public GameObject chmurka;
	// Use this for initialization
	void Start () {
        VapTimer = 0;
    }
    public int vapIStart = 5;
    public float VapTime = 5;
    private float VapTimer;
    private float vapDelta;
    private int vapI;
    
	void Update () {
        VapTimer += Time.deltaTime;

        if (vapI > 1)
        {
            if (vapI > VapTime - VapTimer*4)
            {
                vapI--;
                Instantiate(chmurka, transform);
            }
        }

        if (controller.Special2() || Input.GetKeyDown("space"))
        {
            Vap();
        }
    }

    public void Vap()
    {
        if (VapTimer > VapTime)
        {
            vapI = 5;
            VapTimer = 0;
        }

    }
}
