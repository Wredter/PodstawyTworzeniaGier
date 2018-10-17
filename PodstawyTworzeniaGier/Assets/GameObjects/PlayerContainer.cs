using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerContainer : NetworkBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (isLocalPlayer)
        {
            if (Input.GetKey(KeyCode.D)) transform.Translate(Vector2.right * 10 * Time.deltaTime);
            if (Input.GetKey(KeyCode.A)) transform.Translate(Vector2.left * 10 * Time.deltaTime);
            if (Input.GetKey(KeyCode.W)) transform.Translate(Vector2.up * 10 * Time.deltaTime);
            if (Input.GetKey(KeyCode.S)) transform.Translate(Vector2.down * 10 * Time.deltaTime);
        }

    }
}
