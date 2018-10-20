using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class LocalPlayerEntity : NetworkBehaviour {

    public GameObject MyHordePrefab;

	// Use this for initialization
	void Start () {
        if (!isLocalPlayer)
        {
            return;
        }
        CmdSpawnMyHorde();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    //////////////   CMD   ////////////////
    [Command]
    void CmdSpawnMyHorde()
    {
        GameObject obj = Instantiate(MyHordePrefab);


        NetworkServer.SpawnWithClientAuthority(obj, connectionToClient);
    }
}
