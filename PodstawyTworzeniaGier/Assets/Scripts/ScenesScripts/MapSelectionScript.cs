using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapSelectionScript : MonoBehaviour
{
    public Button gamemode1;
    public Button gamemode2;
    public Button map1;
    public Button map2;

    private List<Button> gamemodes;
    private Button currentGamemode;
    private List<Button> maps;
    private Button currentMap;
    private List<IController> controllers;
    private List<bool> hasMoved;

    private bool choosesGamemode;

    void Start()
    {
        choosesGamemode = true;
        gamemodes = new List<Button>
        {
            gamemode1,
            gamemode2
        };
        currentGamemode = gamemode1;
        maps = new List<Button>
        {
            map1,
            map2
        };
        currentMap = map1;
        controllers = new List<IController>();
        for (int i = 0; i < PlayerPrefs.GetInt("PlayersCount"); i++)
        {
            controllers.Add(new ControllerXbox());
            controllers[i].SetDeviceSignature("Joystick" + (i + 1));
            hasMoved.Add(false);
        }
    }

    void Update()
    {
        for (int i = 0; i < controllers.Count; i++)
        {
            if (choosesGamemode)
            {
                #region Change active gamemode
                /*if (!hasMoved[i] && controllers[i].MoveHorizontal() > 0.8)
                {
                    if ()
                    {
                        int pom = currentButton - 1;
                        activeButtons[pom].GetComponent<Image>().color = Color.yellow;
                        activeButtons[currentButton].GetComponent<Image>().color = Color.white;
                        currentButton = pom;
                        hasMoved[i] = true;
                    }
                }
                else if (!hasMoved[i] && controllers[i].MoveHorizontal() < -0.8)
                {
                    if (currentButton != activeButtons.Count - 1)
                    {
                        int pom = currentButton + 1;
                        activeButtons[pom].GetComponent<Image>().color = Color.yellow;
                        activeButtons[currentButton].GetComponent<Image>().color = Color.white;
                        currentButton = pom;
                        hasMoved[i] = true;
                    }
                }*/
                #endregion
            }
            else
            {
                #region Change active gamemode
                #endregion
            }
            if (hasMoved[i] && controllers[i].MoveHorizontal() <= 0.8 && controllers[i].MoveHorizontal() >= -0.8)
            {
                hasMoved[i] = false;
            }
            #region Select button
            if (controllers[i].Select())
            {
                /*
                if (choosesGamemode)
                {
                    choosesGamemode = false;
                }
                if (currentButton == 0 && activeButtons.Count > 1)
                {
                    PlayerPrefs.SetString("Map", "Meadow");
                    PlayerPrefs.SetString("GameMode", "DeathMatch");
                    PlayerPrefs.SetInt("PlayersCount", 2);
                    SceneManager.LoadScene("SelectionScreen2Players");
                }
                else if (currentButton == 1 && activeButtons.Count == 3)
                {
                    PlayerPrefs.SetInt("PlayersCount", 4);
                    SceneManager.LoadScene("");
                }
                else
                {
                    Application.Quit();
                    UnityEditor.EditorApplication.isPlaying = false;
                }*/
            }
            #endregion
        }
    }
}
