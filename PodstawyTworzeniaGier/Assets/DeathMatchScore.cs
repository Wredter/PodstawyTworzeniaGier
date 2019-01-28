using UnityEngine;
using UnityEngine.UI;

public class DeathMatchScore : MonoBehaviour
{
    public string player;
    private int score = 0;

    void Update()
    {
        if (PlayerPrefs.GetString("GameMode") == "DeathMatch")
        {
            score = PlayerPrefs.GetInt(player + "score");
            if (score <= 0)
            {
                GetComponent<Text>().text = "0";
            }
            else
            {
                GetComponent<Text>().text = score.ToString();
            }
        }
        else
        {
            score = PlayerPrefs.GetInt(player + "score");
            GetComponent<Text>().text = score.ToString();
        }
    }
}
