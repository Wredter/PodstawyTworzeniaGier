using UnityEngine;
using UnityEngine.UI;

public class DeathMatchScore : MonoBehaviour {
    public string player;
    private int score = 0;

	void Update () {
		if(PlayerPrefs.GetInt(player + "score") != score)
        {
            score = PlayerPrefs.GetInt(player + "score");
            GetComponent<Text>().text = score.ToString();
            Debug.Log(PlayerPrefs.GetInt(player + "score"));
        }
	}
}
