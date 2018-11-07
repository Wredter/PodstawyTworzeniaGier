using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargeBarScriptXbox : MonoBehaviour {

    public HUDScriptXbox script;

	void Update () {
        float scale = 1-script.GetHorde().GetChargeActualValue();
        if (scale <= 1)
        {
            transform.localScale = new Vector3(scale*50, 2);
        }
        
	}
}
