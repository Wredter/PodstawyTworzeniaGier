using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class magScript : MonoBehaviour {
    public GameObject fireball;
    private Chief chief;
    private Rigidbody2D rb;
    [Range(0f,10f)]
    public float radius;

	// Use this for initialization
	void Start () {
        chief = GetComponent<Chief>();
        rb = GetComponent<Rigidbody2D>();
	}

    // Update is called once per frame
    void Update()
    {
        if (chief.GetController().Special2())
        {
            Debug.Log("FireBall");
            Vector3 chiefPosition = gameObject.GetComponent<Transform>().position;
            fireball = Instantiate(fireball,transform);
            fireball.GetComponent<Fireball_scripy>().cast(new Vector2(rb.transform.position.x, rb.transform.position.y),radius);
        }
    }
}
