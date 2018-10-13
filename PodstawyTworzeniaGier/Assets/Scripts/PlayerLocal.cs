using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerLocal : NetworkBehaviour {
    public GameObject hordePrefab, minionPrefab, chiefPrefab;
    public int minionsNumber = 5;
    public float spawnRadius = 3;

    // Use this for initialization
    void Start () {
        Debug.Log("is local Plaeyr: " + isLocalPlayer);
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
        /*

        //chief
        GameObject chief = Instantiate(chiefPrefab);

        //minions
        List<GameObject>  minions = new List<GameObject>();
        for (int i = 0; i < minionsNumber; i++)
        {
            float radius = Mathf.PI * 2 / minionsNumber * i;
            GameObject minion = Instantiate(minionPrefab, new Vector3(spawnRadius * Mathf.Cos(radius) + Random.value, spawnRadius * Mathf.Sin(radius) + Random.value, 0), Quaternion.identity);
            minions.Add(minion);
        }

        //horde
        GameObject horde = Instantiate(hordePrefab);

        chief.transform.parent = horde.transform;

        foreach (GameObject minion in minions)
        {
            minion.transform.parent = horde.transform;
        }


        //spawn
        NetworkServer.SpawnWithClientAuthority(chief, connectionToClient);
        foreach (GameObject minion in minions)
        {
            NetworkServer.SpawnWithClientAuthority(minion, connectionToClient);
        }

        NetworkServer.SpawnWithClientAuthority(horde, connectionToClient);

    */
        GameObject chief = Instantiate(chiefPrefab);
        NetworkServer.SpawnWithClientAuthority(chief, connectionToClient);
    }
}
