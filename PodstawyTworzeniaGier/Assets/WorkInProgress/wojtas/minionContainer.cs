using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class minionContainer : MonoBehaviour {

    public GameObject sprite;
	void Start () {
        Instantiate(sprite, gameObject.transform.position, Quaternion.identity, gameObject.transform);
    }
	
	// Update is called once per frame
	void Update () {
        Vector2 velocity = GetComponent<Rigidbody2D>().velocity;
        if (velocity.x*velocity.x + velocity.y * velocity.y > 16)
        {
            transform.eulerAngles = new Vector3(0, 0, Vector2.SignedAngle(Vector2.up, velocity));
        }
            
    }
}
