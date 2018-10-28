using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHUDScriptXbox : MonoBehaviour {
    public HordeXbox horde;
    [Space(1)]
    public GameObject archerHUD;
    public GameObject vikingHUD;

    private GameObject activeHUD;
	// Use this for initialization
	void Start () {
		if(horde.hordeMinion.GetComponent<ArcherXbox>())
        {
            activeHUD = Instantiate(archerHUD);
        }
        else if(horde.hordeMinion.GetComponent<VikingXbox>())
        {
            activeHUD = Instantiate(vikingHUD);
        }
        activeHUD.GetComponent<HUDScriptXbox>().SetHorde(horde);
        activeHUD.transform.SetParent(gameObject.transform);
        if (gameObject.transform.position.x > 1) activeHUD.transform.position = gameObject.transform.position;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
