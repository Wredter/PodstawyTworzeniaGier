using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class minion_temporary_script : MonoBehaviour {
    public float moveSpeed;
    public GameObject projectile;
    private float maxSpeed = 5f;
    private Vector2 input;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        if (GetComponent<Rigidbody2D>().velocity.magnitude < maxSpeed)
        {
            GetComponent<Rigidbody2D>().AddForce(input * moveSpeed);
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameObject bullet = Instantiate(projectile, transform.position, Quaternion.identity) as GameObject;
            bullet.GetComponent<Rigidbody>().AddForce(transform.forward * 10);
        }
        print(input);
    }
}
