using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manekin : MinionBase {

    // Use this for initialization
    void Start() {
        
    }
    private void FixedUpdate()
    {
        
    }

    // Update is called once per frame
    void Update () {
        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
	}
}
