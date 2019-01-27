using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class vaperScript : MonoBehaviour {

    public GameObject chmurka;
	// Use this for initialization
	void Start () {
        VapTimer = 0;
    }

    public float VapTime;
    private float VapTimer;
    
	void Update () {
        VapTimer += Time.deltaTime;
	}

    public void Vap()
    {
        if (VapTimer > VapTime)
        {
            VapTime = 0;
        }
    }
}
