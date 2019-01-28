using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball_scripy : Projectile {
    private float timecounter = 0;
    private float oldX;
    private float oldY;
    public float offset;
    private float z;
	// Use this for initialization
	void Start () {
        oldX = 0;
        oldY = 0;
        offset = 0;
        z = 0;
	}
	
	// Update is called once per frame
	void Update () {
        timecounter += Time.deltaTime;

        float x = Mathf.Cos(timecounter);
        float y = Mathf.Sin(timecounter);
        Vector2 current = new Vector2(x,y);
        Vector2 old = new Vector2(oldX, oldY);

         z = Vector2.Angle(old, current);

        Debug.Log(z);

        transform.position = new Vector3(x*5,y*5,0);
        transform.RotateAround(new Vector3(0, 0, 0), Vector3.forward, z+offset);
        oldX = x;
        oldY = y;
		
	}
}
