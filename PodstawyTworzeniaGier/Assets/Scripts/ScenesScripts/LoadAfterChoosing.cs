using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadAfterChoosing : MonoBehaviour
{
    private int playerCount;
    private int readyPlayers;

    public void Start()
    {
        playerCount = PlayerPrefs.GetInt("PlayersCount");
    }

    public void Ready()
    {
        readyPlayers++;
        if (readyPlayers == playerCount)
        {
            switch (playerCount)
            {
                case 2:
                    switch (PlayerPrefs.GetString("GameMode"))
                    {
                        case "DeathMatch":
                            switch (PlayerPrefs.GetString("Map"))
                            {
                                case "Meadow":
                                    SceneManager.LoadScene("DeathMatchMeadow2Players");
                                    break;
                                case "Desert":
                                    SceneManager.LoadScene("DeathMatchDesert2Players");
                                    break;
                            }
                            break;
                        case "KingOfTheHill":
                            switch (PlayerPrefs.GetString("Map"))
                            {
                                case "Meadow":
                                    SceneManager.LoadScene("KingOfTheHillMeadow2Players");
                                    break;
                                case "Desert":
                                    SceneManager.LoadScene("KingOfTheHillDesert2Players");
                                    break;
                            }
                            break;
                    }
                    SceneManager.LoadScene("DeathMatchMeadow2Players");
                    break;
                case 4:
                    switch (PlayerPrefs.GetString("GameMode"))
                    {
                        case "DeathMatch":
                            switch (PlayerPrefs.GetString("Map"))
                            {
                                case "Meadow":
                                    SceneManager.LoadScene("DeathMatchMeadow4Players");
                                    break;
                                case "Desert":
                                    SceneManager.LoadScene("DeathMatchDesert4Players");
                                    break;
                            }
                            break;
                        case "KingOfTheHill":
                            switch (PlayerPrefs.GetString("Map"))
                            {
                                case "Meadow":
                                    SceneManager.LoadScene("KingOfTheHillMeadow4Players");
                                    break;
                                case "Desert":
                                    SceneManager.LoadScene("KingOfTheHillDesert4Players");
                                    break;
                            }
                            break;
                    }
                    SceneManager.LoadScene("KingOfTheHill");
                    break;
            }
        }
    }

    public void UnReady()
    {
        readyPlayers--;
    }
}
