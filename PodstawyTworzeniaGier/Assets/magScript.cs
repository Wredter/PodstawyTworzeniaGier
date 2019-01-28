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
    [Range(0,100)]
    public int dmg;
    [Range(0,10)]
    public float speed;
    [Range(0,10)]
    public float fireball_cd;
    [Range(0,10)]
    public float fireball_duration;
    private float timer1;
    private float timer2;

	// Use this for initialization
	void Start () {
        chief = GetComponent<Chief>();
        rb = GetComponent<Rigidbody2D>();
        timer1 = 0;
        timer2 = 0;
	}

    // Update is called once per frame
    void Update()
    {
        timer1 -= Time.deltaTime;
        timer2 -= Time.deltaTime;
        Debug.Log(timer2);
        if (chief.GetController().Special2() && timer2 <= 0)
        {
            timer1 = fireball_duration;
            timer2 = fireball_cd;
            f1 = Instantiate(fireball,new Vector3(rb.transform.position.x, rb.transform.position.y,0),Quaternion.Euler(0,0,0));
            f2 = Instantiate(fireball, new Vector3(rb.transform.position.x, rb.transform.position.y, 0), Quaternion.Euler(0, 0, 180));
            f1.GetComponent<Fireball_scripy>().cast(new Vector2(rb.transform.position.x, rb.transform.position.y), radius, gameObject, true, dmg, speed);
            f2.GetComponent<Fireball_scripy>().cast(new Vector2(rb.transform.position.x, rb.transform.position.y), radius, gameObject, false, dmg, speed);
            //Debug.Log("Wut");
            
        }
        if (timer1 <=0)
        {
            Destroy(f1);
            Destroy(f2);
        }
    }
    public Chief GetChief()
    {
        return chief;
    }
}
