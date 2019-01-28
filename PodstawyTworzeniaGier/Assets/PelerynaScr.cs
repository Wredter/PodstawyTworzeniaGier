using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PelerynaScr : MonoBehaviour {

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.GetComponent<Arrow>() != null || col.gameObject.GetComponent<Axe>() != null)
        {
            Destroy(gameObject);
        }
    }
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
