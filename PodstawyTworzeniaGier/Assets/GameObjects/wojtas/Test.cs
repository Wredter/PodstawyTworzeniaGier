using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Test : NetworkBehaviour {

    public GameObject prefab;

    private GameObject t;
	// Use this for initialization
	void Start () {
		if (isLocalPlayer)
        {
            CmdTest();
        }
	}
	
	// Update is called once per frame
	void Update () {
		if (isLocalPlayer)
        {
            if (Input.GetKey(KeyCode.P))
            {
                t.transform.Translate(Vector2.right * 3f * Time.deltaTime);
            }
        }
	}

    [Command]
    public void CmdTest()
    {
        t = Instantiate(prefab, transform.position, Quaternion.identity);
        NetworkServer.SpawnWithClientAuthority(t, connectionToClient);
    }
}
