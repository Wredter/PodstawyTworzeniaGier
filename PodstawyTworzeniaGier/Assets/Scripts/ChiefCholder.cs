using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChiefCholder : MonoBehaviour {
    //public float speedMultipalyer;
	// Use this for initialization
	void Start () {
		
	}
    // Update is called once per frame
    private void FixedUpdate()
    {
        Vector2 velocity = GetComponent<Rigidbody2D>().velocity;
        if (velocity.x * velocity.x + velocity.y * velocity.y > 16)
        {
            transform.eulerAngles = new Vector3(0, 0, Vector2.SignedAngle(Vector2.up, velocity));
        }
    }

    // Update is called once per frame
    void Update () {
		
	}
}
