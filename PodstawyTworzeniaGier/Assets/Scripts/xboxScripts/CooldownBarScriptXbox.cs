using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CooldownBarScriptXbox : MonoBehaviour {
    public HUDScriptXbox script;

    void FixedUpdate()
    {
        float scale = 1 - script.GetHorde().GetCooldown();
        if (scale <= 1)
        {
            transform.localScale = new Vector3(scale * 50, 2);
        }

    }
}
