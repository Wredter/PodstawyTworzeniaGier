using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class minion_temporary_script : MonoBehaviour {
    public float moveSpeed;
    public GameObject projectile;
    private Vector2 input;
    private Rigidbody2D rb2d;

	// Use this for initialization
	void Start () {
        rb2d = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
        input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        rb2d.velocity = new Vector2(input.x * moveSpeed,input.y * moveSpeed);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameObject bullet = Instantiate(projectile, transform.position, Quaternion.identity) as GameObject;
            bullet.GetComponent<Rigidbody2D>().AddForce(input * 1000);
        }
    }
}
