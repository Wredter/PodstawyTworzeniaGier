using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class basicMovement : NetworkBehaviour {
    Rigidbody2D minion;
    public float maxSpeed;
	// Use this for initialization
	void Start () {
        minion = GetComponent<Rigidbody2D>();
       
	}
	
	// Update is called once per frame
	void Update () {

        float moveX = Input.GetAxis("Horizontal");
        float moveY = Input.GetAxis("Vertical");

        if(isLocalPlayer)
            minion.velocity = new Vector2(moveX*maxSpeed,moveY*maxSpeed);
	}
}
