using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChmurkaScript : MonoBehaviour {

    private float time;
    private float time2;
    private float scl;
    public float doubleT = 3;
    void Start () {
        time = 0;
        time2 = doubleT;
        scl = 0;
	}
	
	// Update is called once per frame
	void Update () {
        time2 -= Time.deltaTime;
        time += time2 * Time.deltaTime;
        scl = time;
        transform.localScale = new Vector3(scl, scl, 1);
        if (scl < 0)
        {
            Destroy(gameObject);
        }
    }
}
