using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerBarScriptXbox : MonoBehaviour {
    public HordeXbox horde;

	// Use this for initialization
	void Start () {
		
	}

    // Update is called once per frame
    void Update()
    {
        float scale = horde.GetPower();
        if (scale <= 1)
        {
            transform.localScale = new Vector3(scale * 50, 2);
        }

    }
}
