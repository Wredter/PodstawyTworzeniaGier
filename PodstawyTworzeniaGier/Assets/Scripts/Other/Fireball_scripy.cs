﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball_scripy : MonoBehaviour {
    private float timecounter = 0;
    private float oldX;
    private float oldY;
    private Vector2 posOffset;
    public float offset;
    private float radius;
    private Rigidbody2D rb2d;
    private float z;
    // Use this for initialization
    void Start() {
        oldX = 0;
        oldY = 0;
        z = 0;
        rb2d = GetComponent<Rigidbody2D>();
    }
    public void cast(Vector2 offset,float radius)
    {
        posOffset = offset;
        this.radius = radius;
    }
	
	// Update is called once per frame
	void Update () {
        timecounter += Time.deltaTime;

        float x = Mathf.Cos(timecounter);
        float y = Mathf.Sin(timecounter);
        Vector2 current = new Vector2(x,y);
        Vector2 old = new Vector2(oldX, oldY);

         z = Vector2.Angle(old, current);

        transform.position = new Vector3((x*radius)+posOffset.x,(y*radius)+posOffset.y,0);
        transform.RotateAround(new Vector3(0, 0, 0), Vector3.forward, z);
        oldX = x;
        oldY = y;
		
	}
    public void OnTriggerEnter2D(Collider2D col)
    {

    }
}
