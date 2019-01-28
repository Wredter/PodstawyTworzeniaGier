using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
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
    private List<bool> hasSeledcted;

    private bool choosesGamemode;

    void Start()
    {
        hasMoved = new List<bool>();
        hasSeledcted = new List<bool>();
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
            hasSeledcted.Add(false);
        }
    }

    void Update()
    {
        for (int i = 0; i < controllers.Count; i++)
        {
            #region Change selections
            if (choosesGamemode)
            {
                #region Change active gamemode
                if (!hasMoved[i] && controllers[i].MoveHorizontal() > 0.8)
                {
                    if (currentGamemode = gamemode1)
                    {
                        currentGamemode.GetComponent<Image>().color = Color.white;
                        currentGamemode = gamemode2;
                        currentGamemode.GetComponent<Image>().color = Color.yellow;
                        hasMoved[i] = true;
                    }                    
                }
                else if (!hasMoved[i] && controllers[i].MoveHorizontal() < -0.8)
                {
                    if (currentGamemode = gamemode2)
                    {
                        currentGamemode.GetComponent<Image>().color = Color.white;
                        currentGamemode = gamemode1;
                        currentGamemode.GetComponent<Image>().color = Color.yellow;
                        hasMoved[i] = true;
                    }
                }
                #endregion
            }
            else
            {
                #region Change active map
                if (!hasMoved[i] && controllers[i].MoveHorizontal() > 0.8)
                {
                    if (currentMap = map1)
                    {
                        currentMap.GetComponent<Image>().color = Color.white;
                        currentMap = map2;
                        currentMap.GetComponent<Image>().color = Color.yellow;
                        hasMoved[i] = true;
                    }
                }
                else if (!hasMoved[i] && controllers[i].MoveHorizontal() < -0.8)
                {
                    if (currentMap = map2)
                    {
                        currentMap.GetComponent<Image>().color = Color.white;
                        currentMap = map1;
                        currentMap.GetComponent<Image>().color = Color.yellow;
                        hasMoved[i] = true;
                    }
                }
                #endregion
            }
            if (hasMoved[i] && controllers[i].MoveHorizontal() <= 0.8 && controllers[i].MoveHorizontal() >= -0.8)
            {
                hasMoved[i] = false;
            }
            #endregion
            #region Select button
            if (controllers[i].Select())
            {
                if (choosesGamemode && hasSeledcted[i])
                {
                    choosesGamemode = false;
                    hasSeledcted[i] = true;
                }
                else
                {
                    if(PlayerPrefs.GetInt("PlayersCount") == 2)
                    {
                        SceneManager.LoadScene("SelectionScreen2Players");
                    }
                    else
                    {
                        SceneManager.LoadScene("SelectionScreen4Players");
                    }
                }      
            }
            #endregion
            #region Back button
            if (controllers[i].Select())
            {
                if (choosesGamemode && hasSeledcted[i])
                {
                    choosesGamemode = false;
                    hasSeledcted[i] = true;
                }
                else if(!hasSeledcted[i])
                {
                    SceneManager.LoadScene("TitleScreen");
                }
            }
            #endregion
            #region Unlock buttons
            if(!controllers[i].Select() && !controllers[i].Back() && hasSeledcted[i])
            {
                hasSeledcted[i] = false;
            }
            #endregion
        }
    }
}
