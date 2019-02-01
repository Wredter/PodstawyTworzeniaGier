using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SelectionScript : MonoBehaviour
{
    private float horixontalActivationPoint = 0.99f;
    private float verticalActivationPoint = 0.99f;

    public string deviceSignature;
    public string player;
    public GameObject nextSceneLauncher;
    public GameObject minionsCarousel;
    public GameObject chiefsCarousel;
    public GameObject chiefsSpotlight;
    public GameObject minionsSpotlight;

    private IController controller;
    private bool selected = false;
    private bool selectionChiefs = true;
    public int selectedChief;
    public int selectedMinion;
    private bool isReady = false;
    private Vector3 minionsRotation = new Vector3(-90, 15, 0);
    private Vector3 chiefsRotation = new Vector3(-90, 15, 0);
    private Vector3 minionsRotationGoal = new Vector3(-90, 15, 0);
    private Vector3 chiefsRotationGoal = new Vector3(-90, 15, 0);
    private bool backBlock;

    private bool notMovedHorizontal;
    private bool notMovedVertical;

    private bool hasLB;
    private bool hasRB;
    private bool hasLT;
    private bool hasRT;

    void Start()
    {
        backBlock = false;
        deviceSignature = PlayerPrefs.GetString(player + "Controller");
        switch (deviceSignature)
        {
            case "Joystick1":
            case "Joystick2":
            case "Joystick3":
            case "Joystick4":
                controller = gameObject.AddComponent(typeof(ControllerXbox)) as ControllerXbox;
                break;
            case "":
                Debug.Log(deviceSignature);
                controller = gameObject.AddComponent(typeof(ControllerMouseAndKeyboard)) as ControllerMouseAndKeyboard;
                break;
        }
        controller.SetDeviceSignature(deviceSignature);
        selectedChief = 1;
        selectedMinion = 1;
        minionsCarousel.transform.eulerAngles = minionsRotation;
        chiefsCarousel.transform.eulerAngles = chiefsRotation;
        chiefsSpotlight.SetActive(true);
        minionsSpotlight.SetActive(true);
        hasLB = false;
        hasRB = false;
        hasLT = false;
        hasRT = false;
    }

    void FixedUpdate()
    {
        if (minionsRotation.y != minionsRotationGoal.y)
        {
            minionsRotation = Vector3.MoveTowards(minionsRotation, minionsRotationGoal, 1);
        }
        minionsCarousel.transform.eulerAngles = minionsRotation;
        if (chiefsRotation.y != chiefsRotationGoal.y)
        {
            chiefsRotation = Vector3.MoveTowards(chiefsRotation, chiefsRotationGoal, 1);
        }
        chiefsCarousel.transform.eulerAngles = chiefsRotation;
    }

    void Update()
    {
        if (controller.Select())
        {
            PlayerPrefs.SetInt("Player1score", 0);
            PlayerPrefs.SetInt("Player2score", 0);
            PlayerPrefs.SetInt("Player3score", 0);
            PlayerPrefs.SetInt("Player4score", 0);
            if (!isReady)
            {
                nextSceneLauncher.GetComponent<LoadAfterChoosing>().Ready();
                string chief = "";
                string minion = "";
                switch (selectedChief)
                {
                    case 0:
                        chief = "pope";
                        break;
                    case 1:
                        chief = "mexican";
                        break;
                    case 2:
                        chief = "vaper";
                        break;
                    case 3:
                        chief = "mage";
                        break;
                }
                PlayerPrefs.SetString(player + "chief", chief);
                switch (selectedMinion)
                {
                    case 0:
                        minion = "vikings";
                        break;
                    case 1:
                        minion = "archers";
                        break;
                    case 2:
                        minion = "spartans";
                        break;
                    case 3:
                        minion = "zombies";
                        break;
                }
                PlayerPrefs.SetString(player + "minion", minion);
            }
            isReady = true;
            chiefsSpotlight.SetActive(false);
            minionsSpotlight.SetActive(false);
        }
        if (controller.Back() && !backBlock)
        {
            if (isReady)
            {
                chiefsSpotlight.SetActive(true);
                minionsSpotlight.SetActive(true);
                nextSceneLauncher.GetComponent<LoadAfterChoosing>().UnReady();
                isReady = false;
                backBlock = true;
            }
            else
            {
                SceneManager.LoadScene("MapSelection");
            }
        }
        if (!controller.Back() && backBlock == true)
        {
            backBlock = false;
        }
        if (!isReady)
        {
            if (controller.Special1() && !hasLB)
            {
                MoveCarousel(1, true);
                hasLB = true;
            }
            else if (!controller.Special1() && hasLB)
            {
                hasLB = false;
            }
            if (controller.Special2() && !hasRB)
            {
                MoveCarousel(-1, true);
                hasRB = true;
            }
            else if (!controller.Special2() && hasRB)
            {
                hasRB = false;
            }
            if (controller.Block() && !hasLT)
            {
                MoveCarousel(1, false);
                hasLT = true;
            }
            else if (!controller.Block() && hasLT)
            {
                hasLT = false;
            }
            if (controller.Shoot() && !hasRT)
            {
                MoveCarousel(-1, false);
                hasRT = true;
            }
            else if (!controller.Shoot() && hasRT)
            {
                hasRT = false;
            }
        }
    }

    private void MoveCarousel(int direction, bool selectionChiefs)
    {
        if (selectionChiefs)
        {
            selectedChief = (selectedChief + direction) % 4;
            if (selectedChief == -1)
            {
                selectedChief = 3;
            }
            if (direction == -1)
            {
                chiefsRotationGoal += new Vector3(0, 30, 0);
            }
            else
            {
                chiefsRotationGoal += new Vector3(0, -30, 0);
            }
        }
        else
        {
            selectedMinion = (selectedMinion + direction) % 4;
            if (selectedMinion == -1)
            {
                selectedMinion = 3;
            }
            if (direction == -1)
            {
                minionsRotationGoal += new Vector3(0, 30, 0);
            }
            else
            {
                minionsRotationGoal += new Vector3(0, -30, 0);
            }
        }
    }
}
