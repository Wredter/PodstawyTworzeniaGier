using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnControll : MonoBehaviour {

    public GameObject player1;        
    public GameObject player2;

	void Start () {
		
	}
	
	void Update () {
		
	}

    public void RespawnPlayer1(GameObject toRespawn)
    {        
        if (player1.GetComponent<PlayerHUD>().GetHorde().GetComponent<Horde>().GetMinionsCount() == 0)
        {
            player1.GetComponent<PlayerHUD>().ReloadHorde();
        }
    }
    public void RespawnPlayer2(GameObject toRespawn)
    {
        if (player2.GetComponent<PlayerHUD>().GetHorde().GetComponent<Horde>().GetMinionsCount() == 0)
        {
            player2.GetComponent<PlayerHUD>().ReloadHorde();
        }
    }
}
