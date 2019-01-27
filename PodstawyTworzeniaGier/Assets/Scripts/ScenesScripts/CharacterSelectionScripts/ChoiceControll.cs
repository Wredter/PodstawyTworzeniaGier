using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChoiceControll : MonoBehaviour {
    public GameObject player1;
    public GameObject player2;
    private GameObject readyPlayer;

    public void NotifyThatChose(GameObject player)
    {
        if(readyPlayer == null)
        {
            readyPlayer = player;
        } else if(readyPlayer.Equals(player))
        {
            return;
        } else
        {
            SceneManager.LoadScene("stachuj");
        }
    }

    public void NotifyThatHadntChose(GameObject player)
    {
        if(readyPlayer.Equals(player))
        {
            readyPlayer = null;
        }
    }
}
