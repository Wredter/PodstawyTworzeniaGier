using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerLocal : NetworkBehaviour {
    public GameObject PlayerUnitPrefab;
	// Use this for initialization
	void Start () {
        if ( !isLocalPlayer )
        {
            return;
        }

        CmdSpawnMyUnit();

		
	}
    
	// Update is called once per frame
	void Update () {
		
	}


    /////////////////// COMANDS ///////////////////
    [Command]
    void CmdSpawnMyUnit()
    {
        GameObject obj = Instantiate(PlayerUnitPrefab);

        NetworkServer.SpawnWithClientAuthority(obj, connectionToClient);
    }
}
