using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class magScript : MonoBehaviour {
    public GameObject fireball;
    private GameObject f1;
    private GameObject f2;
    private Chief chief;
    private Rigidbody2D rb;
    [Range(0f,10f)]
    public float radius;
    private float fireball_cd;

	// Use this for initialization
	void Start () {
        chief = GetComponent<Chief>();
        rb = GetComponent<Rigidbody2D>();
        fireball_cd = 0;
	}

    // Update is called once per frame
    void Update()
    {
        fireball_cd -= Time.deltaTime;
        if (chief.GetController().Special2() && fireball_cd <= 0)
        {
            Vector3 chiefPosition = gameObject.GetComponent<Transform>().position;
            f1 = Instantiate(fireball,new Vector3(rb.transform.position.x, rb.transform.position.y,0),Quaternion.Euler(0,0,0));
            f2 = Instantiate(fireball, new Vector3(rb.transform.position.x, rb.transform.position.y, 0), Quaternion.Euler(0, 0, 180));
            fireball_cd = 1;
            f1.GetComponent<Fireball_scripy>().cast(new Vector2(rb.transform.position.x, rb.transform.position.y), radius, gameObject, true);
            f2.GetComponent<Fireball_scripy>().cast(new Vector2(rb.transform.position.x, rb.transform.position.y), radius, gameObject, false);
            //Debug.Log("Wut");
            
        }
    }
}
