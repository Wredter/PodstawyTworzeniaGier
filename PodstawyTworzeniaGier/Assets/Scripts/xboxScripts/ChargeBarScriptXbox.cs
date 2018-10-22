using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargeBarScriptXbox : MonoBehaviour {

    public HordeXbox horde;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        float scale = 1-horde.GetChargeActualValue();
        if (scale <= 1)
        {
            transform.localScale = new Vector3(scale*50, 5);
        }
        
	}
}
