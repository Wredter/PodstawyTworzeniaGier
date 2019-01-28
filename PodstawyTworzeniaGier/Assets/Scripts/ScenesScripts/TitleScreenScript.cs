using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleScreenScript : MonoBehaviour
{
    public Button twoPlayers;
    public Button fourPlayers;
    public Button exit;

    private int controllersCount;
    private List<ControllerXbox> controllers;
    private List<bool> hasMoved;
    private List<Button> activeButtons;
    private int currentButton;

    void Start()
    {
        controllers = new List<ControllerXbox>();
        hasMoved = new List<bool>();
        activeButtons = new List<Button>();
        currentButton = 0;
        FillActiveButtonsList(1);
        CheckControllers();
        InvokeRepeating("CheckControllers", 0, .1f);
    }

    void FixedUpdate()
    {        
        for (int i = 0; i < controllers.Count; i++)
        {
            #region Change active button
            if (!hasMoved[i] && controllers[i].MoveVertical() > 0.8)
            {
                if (currentButton != 0)
                {
                    int pom = currentButton - 1;
                    activeButtons[pom].GetComponent<Image>().color = Color.yellow;
                    activeButtons[currentButton].GetComponent<Image>().color = Color.white;
                    currentButton = pom;
                    hasMoved[i] = true;
                }
            }
            else if (!hasMoved[i] && controllers[i].MoveVertical() < -0.8)
            {
                if(currentButton != activeButtons.Count-1)
                {
                    int pom = currentButton + 1;
                    activeButtons[pom].GetComponent<Image>().color = Color.yellow;
                    activeButtons[currentButton].GetComponent<Image>().color = Color.white;
                    currentButton = pom;
                    hasMoved[i] = true;
                }
            }
            else if (hasMoved[i] && controllers[i].MoveVertical() <= 0.8 && controllers[i].MoveVertical() >= -0.8)
            {
                hasMoved[i] = false;
            }
            #endregion
            #region Select button
            if(controllers[i].Select())
            {
                if(currentButton == 0 && activeButtons.Count > 1)
                {
                    PlayerPrefs.SetInt("PlayersCount", 2);
                    SceneManager.LoadScene("MapSelection");
                }
                else if (currentButton == 1 && activeButtons.Count == 3)
                {
                    PlayerPrefs.SetInt("PlayersCount", 4);
                    SceneManager.LoadScene("MapSelection");
                }
                else
                {
                    Application.Quit();
                    UnityEditor.EditorApplication.isPlaying = false;
                }
            }
            #endregion
        }
    }

    #region Checking and setting up controllers
    void CheckControllers()
    {
        controllersCount = 0;
        foreach (string s in Input.GetJoystickNames())
        {
            if (!string.IsNullOrEmpty(s))
            {
                controllersCount++;
            }
        }

        switch (controllersCount)
        {
            case 0:
                FillControllersList(0);
                FillActiveButtonsList(1);
                twoPlayers.interactable = false;
                fourPlayers.interactable = false;
                break;
            case 1:
                FillControllersList(1);
                FillActiveButtonsList(1);
                twoPlayers.interactable = false;
                fourPlayers.interactable = false;
                break;
            case 2:
            case 3:
                FillControllersList(2);
                FillActiveButtonsList(2);
                twoPlayers.interactable = true;
                fourPlayers.interactable = false;
                break;
            default:
                FillControllersList(4);
                FillActiveButtonsList(3);
                twoPlayers.interactable = true;
                fourPlayers.interactable = true;
                break;
        }
    }

    void FillControllersList(int controllersToCreate)
    {
        if (controllers.Count != controllersToCreate)
        {
            hasMoved = new List<bool>();
            controllers = new List<ControllerXbox>();
            for (int i = 0; i < controllersToCreate; i++)
            {
                hasMoved.Add(true);
                controllers.Add(new ControllerXbox());
                controllers[i].SetDeviceSignature("Joystick" + (i + 1));
            }
        }
    }
    #endregion

    #region Filling list of active buttons
    void FillActiveButtonsList(int activeButtonsCount)
    {
        if (activeButtons.Count != activeButtonsCount)
        {
            if (activeButtons.Count > 0)
            {
                activeButtons[currentButton].GetComponent<Image>().color = Color.white;
            }
            activeButtons = new List<Button>();
            switch (activeButtonsCount)
            {
                case 3:
                    activeButtons.Add(twoPlayers);
                    activeButtons.Add(fourPlayers);
                    activeButtons.Add(exit);
                    break;
                case 2:
                    activeButtons.Add(twoPlayers);
                    activeButtons.Add(exit);
                    break;
                case 1:
                    activeButtons.Add(exit);
                    break;
            }
            activeButtons[0].GetComponent<Image>().color = Color.yellow;
        }
    }
    #endregion
}
