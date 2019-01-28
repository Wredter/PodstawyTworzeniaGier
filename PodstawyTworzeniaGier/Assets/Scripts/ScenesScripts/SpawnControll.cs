using UnityEngine;
using UnityEngine.SceneManagement;

public class SpawnControll : MonoBehaviour {
    public enum GameTypes { DeathMatch, KingOfTheHill}
    public GameTypes gameType;
    public int maxScore;
    public GameObject player1;        
    public GameObject player2;
    

	void Start () {
        PlayerPrefs.SetInt("Player1score", 0);
        PlayerPrefs.SetInt("Player2score", 0);
    }

    void Update()
    {
        if (PlayerPrefs.GetInt("Player2score") == maxScore)
        {
            SceneManager.LoadScene("Player2Won");
        }
        if (PlayerPrefs.GetInt("Player1score") == maxScore)
        {
            SceneManager.LoadScene("Player1Won");
        }
    }

    public void RespawnPlayer1(GameObject toRespawn)
    {
        if(gameType == GameTypes.DeathMatch)
        {
            PlayerPrefs.SetInt("Player2score", PlayerPrefs.GetInt("Player2score") + 1);            
        }
        if (player1.GetComponent<PlayerHUD>().GetHorde().GetComponent<Horde>().GetMinionsCount() == 0)
        {
            player1.GetComponent<PlayerHUD>().ReloadHorde();
        }
    }
    public void RespawnPlayer2(GameObject toRespawn)
    {
        if (gameType == GameTypes.DeathMatch)
        {
            PlayerPrefs.SetInt("Player1score", PlayerPrefs.GetInt("Player1score") + 1);            
        }
        if (player2.GetComponent<PlayerHUD>().GetHorde().GetComponent<Horde>().GetMinionsCount() == 0)
        {
            player2.GetComponent<PlayerHUD>().ReloadHorde();
        }
    }
}
