using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadAfterChoosing : MonoBehaviour {
    public enum GameTypes { DeathMatch, KingOfTheHill }
    public GameTypes gameType;
    public int playerCount;
    private int readyPlayers;

    public void Ready()
    {
        readyPlayers++;
        if(readyPlayers == playerCount)
        {
            switch(gameType)
            {
                case GameTypes.DeathMatch:
                    SceneManager.LoadScene("DeathMatch");
                    break;
            }
        }
    }

    public void UnReady()
    {
        readyPlayers--;
    }
}
