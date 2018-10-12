using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerLocal : NetworkBehaviour {
    public GameObject PlayerUnitPrefab;

    GameObject chief;
	// Use this for initialization
	void Start () {
        if ( !isLocalPlayer )
        {
            return;
        }

        CmdSpawnMyUnit();

		
	}
    bool a = true;
    
	// Update is called once per frame
	void Update () {
		if (chief != null && a && chief.transform.childCount > 0)
        {
            Debug.Log("dfagadsfsadfdsa: " + chief.transform.childCount);
            a = false;
            for (int i = 0; i < chief.transform.childCount; i++)
            {
                //if (chief.hasAuthority)
                NetworkServer.SpawnWithClientAuthority(chief.transform.GetChild(i).gameObject, connectionToClient);
            }
            
        }
	}


    /////////////////// COMANDS ///////////////////
    [Command]
    void CmdSpawnMyUnit()
    {
        GameObject obj = Instantiate(PlayerUnitPrefab);
        chief = obj;
        Debug.Log("liczba dziecków: " + chief.transform.childCount);

        NetworkServer.SpawnWithClientAuthority(obj, connectionToClient);
        
    }
}
