﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraXbox : MonoBehaviour {
    public HordeXbox parent;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        GetComponent<Rigidbody2D>().position = parent.GetChiefPosition();
	}
}