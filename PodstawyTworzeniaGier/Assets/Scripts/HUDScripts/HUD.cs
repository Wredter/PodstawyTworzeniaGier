using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUD : MonoBehaviour {
    private Horde horde;

    public void SetHorde(Horde horde)
    {
        this.horde = horde;
    }

    public Horde GetHorde()
    {
        return horde;
    }
}
