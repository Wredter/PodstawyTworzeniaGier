using UnityEngine;
using UnityEngine.SceneManagement;

public class SpawnControll : MonoBehaviour
{
    public int maxScore;
    public GameObject player1;
    public GameObject player2;
    public GameObject player3;
    public GameObject player4;

    private bool is4;


    void Start()
    {
        if (4 == PlayerPrefs.GetInt("PlayersCount"))
        {
            is4 = true;
        }
        else
        {
            is4 = false;
        }
        if (PlayerPrefs.GetString("GameMode") == "DeathMatch")
        {
            PlayerPrefs.SetInt("Player1score", 5);
            PlayerPrefs.SetInt("Player2score", 5);
            PlayerPrefs.SetInt("Player3score", 5);
            PlayerPrefs.SetInt("Player4score", 5);
        }
        else
        {
            PlayerPrefs.SetInt("Player1score", 0);
            PlayerPrefs.SetInt("Player2score", 0);
            PlayerPrefs.SetInt("Player3score", 0);
            PlayerPrefs.SetInt("Player4score", 0);
        }
    }

    void Update()
    {
        #region Win Conditions
        if (PlayerPrefs.GetString("GameMode") == "DeathMatch")
        {
            if (is4)
            {
                if (PlayerPrefs.GetInt("Player1score") > 0 && PlayerPrefs.GetInt("Player2score") <= 0 && PlayerPrefs.GetInt("Player3score") <= 0 && PlayerPrefs.GetInt("Player4score") <= 0)
                {
                    SceneManager.LoadScene("Player1Won");
                }
                if (PlayerPrefs.GetInt("Player1score") <= 0 && PlayerPrefs.GetInt("Player2score") > 0 && PlayerPrefs.GetInt("Player3score") <= 0 && PlayerPrefs.GetInt("Player4score") <= 0)
                {
                    SceneManager.LoadScene("Player2Won");
                }
                if (PlayerPrefs.GetInt("Player1score") <= 0 && PlayerPrefs.GetInt("Player2score") <= 0 && PlayerPrefs.GetInt("Player3score") > 0 && PlayerPrefs.GetInt("Player4score") <= 0)
                {
                    SceneManager.LoadScene("Player3Won");
                }
                if (PlayerPrefs.GetInt("Player1score") <= 0 && PlayerPrefs.GetInt("Player2score") <= 0 && PlayerPrefs.GetInt("Player3score") <= 0 && PlayerPrefs.GetInt("Player4score") > 0)
                {
                    SceneManager.LoadScene("Player4Won");
                }
            }
            else
            {
                if (PlayerPrefs.GetInt("Player1score") == 0)
                {
                    SceneManager.LoadScene("Player2Won");
                }
                if (PlayerPrefs.GetInt("Player2score") == 0)
                {
                    SceneManager.LoadScene("Player1Won");
                }
            }
        }
        else
        {
            if (is4)
            {
                if (PlayerPrefs.GetInt("Player4score") == maxScore)
                {
                    SceneManager.LoadScene("Player4Won");
                }
                if (PlayerPrefs.GetInt("Player3score") == maxScore)
                {
                    SceneManager.LoadScene("Player3Won");
                }
            }
            if (PlayerPrefs.GetInt("Player2score") == maxScore)
            {
                SceneManager.LoadScene("Player2Won");
            }
            if (PlayerPrefs.GetInt("Player1score") == maxScore)
            {
                SceneManager.LoadScene("Player1Won");
            }
        }
        #endregion
    }

    public void RespawnPlayer1(GameObject toRespawn)
    {
        if (PlayerPrefs.GetString("GameMode") == "DeathMatch")
        {
            PlayerPrefs.SetInt("Player1score", PlayerPrefs.GetInt("Player1score") - 1);
            if (PlayerPrefs.GetInt("Player1score") >= 0)
            {
                player1.GetComponent<PlayerHUD>().ReloadHorde();
            }
            else
            {
                PlayerPrefs.SetInt("Player1score", 0);
            }
        }
        else
        {
            player1.GetComponent<PlayerHUD>().ReloadHorde();
        }
    }
    public void RespawnPlayer2(GameObject toRespawn)
    {
        if (PlayerPrefs.GetString("GameMode") == "DeathMatch")
        {
            PlayerPrefs.SetInt("Player2score", PlayerPrefs.GetInt("Player2score") - 1);
            if (PlayerPrefs.GetInt("Player2score") >= 0)
            {
                player2.GetComponent<PlayerHUD>().ReloadHorde();
            }
            else
            {
                PlayerPrefs.SetInt("Player2Score", 0);
            }
        }
        else
        {
            player2.GetComponent<PlayerHUD>().ReloadHorde();
        }
    }
    public void RespawnPlayer3(GameObject toRespawn)
    {
        if (PlayerPrefs.GetString("GameMode") == "DeathMatch")
        {
            PlayerPrefs.SetInt("Player3score", PlayerPrefs.GetInt("Player3score") - 1);
            if (PlayerPrefs.GetInt("Player3score") >= 0)
            {
                player3.GetComponent<PlayerHUD>().ReloadHorde();
            }
            else
            {
                PlayerPrefs.SetInt("Player3score", 0);
            }
        }
        else
        {
            player3.GetComponent<PlayerHUD>().ReloadHorde();
        }
    }
    public void RespawnPlayer4(GameObject toRespawn)
    {
        if (PlayerPrefs.GetString("GameMode") == "DeathMatch")
        {
            PlayerPrefs.SetInt("Player4score", PlayerPrefs.GetInt("Player4score") - 1);
            if (PlayerPrefs.GetInt("Player4score") >= 0)
            {
                player4.GetComponent<PlayerHUD>().ReloadHorde();
            }
            else
            {
                PlayerPrefs.SetInt("Player4score", 0);
            }
        }
        else
        {
            player4.GetComponent<PlayerHUD>().ReloadHorde();
        }
    }
}
