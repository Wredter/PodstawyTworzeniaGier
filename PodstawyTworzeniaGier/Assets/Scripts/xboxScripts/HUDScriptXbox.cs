using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUDScriptXbox : MonoBehaviour {
    private HordeXbox horde;

    public void SetHorde(HordeXbox horde)
    {
        this.horde = horde;
    }

    public HordeXbox GetHorde()
    {
        return horde;
    }
}
