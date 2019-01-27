﻿using System.Collections;
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
        VapTimer = VapTime;
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
            if (vapI > VapTime - VapTimer)
            {
                vapI--;
                Debug.Log("vaper spawn");
                Instantiate(chmurka, transform.position, transform.rotation);
            }
        }
        if (controller.Special2() || Input.GetKeyDown(KeyCode.P))
        {
            Debug.Log("vaper ");
            Vap();
        }
    }

    public void Vap()
    {
        if (VapTimer > VapTime)
        {
            Debug.Log("vaper skkill");
            vapI = vapIStart;
            VapTimer = 0;
        }

    }
}
