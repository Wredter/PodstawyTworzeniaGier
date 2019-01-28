using UnityEngine;
using UnityEngine.SceneManagement;

public class NextScene : MonoBehaviour
{
    void FixedUpdate()
    {
        if (Input.GetButton("Joystick1A") || Input.GetButton("Joystick2A") || Input.GetKey(KeyCode.KeypadEnter))
        {
            SceneManager.LoadScene("SelectionScreen" + PlayerPrefs.GetInt("PlayersCount") + "Players");
        }
    }
}
