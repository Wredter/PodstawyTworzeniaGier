﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
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
    private int selectedChief;
    private int selectedMinion;
    private bool isReady = false;
    private Vector3 minionsRotation = new Vector3(-90, 15, 0);
    private Vector3 chiefsRotation = new Vector3(-90, 15, 0);
    private Vector3 minionsRotationGoal = new Vector3(-90, 15, 0);
    private Vector3 chiefsRotationGoal = new Vector3(-90, 15, 0);

    private bool notMovedHorizontal;
    private bool notMovedVertical;

    void Start()
    {
        switch (deviceSignature)
        {
            case "Joystick1":
            case "Joystick2":
                controller = gameObject.AddComponent(typeof(ControllerXbox)) as ControllerXbox;
                break;
            case "":
                controller = new ControllerMouseAndKeyboard();
                break;
        }
        controller.SetDeviceSignature(deviceSignature);
        selectedChief = 1;
        selectedMinion = 1;
        minionsCarousel.transform.eulerAngles = minionsRotation;
        chiefsCarousel.transform.eulerAngles = chiefsRotation;
        chiefsSpotlight.SetActive(true);
        minionsSpotlight.SetActive(false);
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
        if (controller.Back())
        {
            if (isReady)
            {
                if (selectionChiefs)
                {
                    chiefsSpotlight.SetActive(true);
                    minionsSpotlight.SetActive(false);
                }
                else
                {
                    chiefsSpotlight.SetActive(false);
                    minionsSpotlight.SetActive(true);
                }
                nextSceneLauncher.GetComponent<LoadAfterChoosing>().UnReady();
                isReady = false;
            }
        }
        if (!isReady)
        {
            //Checks if player moved carousel to left or right
            if (controller.MoveHorizontal() > horixontalActivationPoint && notMovedHorizontal)
            {
                notMovedHorizontal = false;
                MoveCarousel(-1);
            }
            else if (controller.MoveHorizontal() < -horixontalActivationPoint && notMovedHorizontal)
            {
                notMovedHorizontal = false;
                MoveCarousel(1);
            }
            else if (controller.MoveHorizontal() <= horixontalActivationPoint && controller.MoveHorizontal() >= -horixontalActivationPoint)
            {
                notMovedHorizontal = true;
            }

            //Checks if player selected other carousel
            if ((controller.MoveVertical() > verticalActivationPoint || controller.MoveVertical() < -verticalActivationPoint) && notMovedVertical)
            {
                notMovedVertical = false;
                selectionChiefs = !selectionChiefs;
                if (selectionChiefs)
                {
                    chiefsSpotlight.SetActive(true);
                    minionsSpotlight.SetActive(false);
                }
                else
                {
                    chiefsSpotlight.SetActive(false);
                    minionsSpotlight.SetActive(true);
                }
            }
            else if ((controller.MoveVertical() <= verticalActivationPoint && controller.MoveVertical() >= -verticalActivationPoint))
            {
                notMovedVertical = true;
            }
        }
    }

    private void MoveCarousel(int direction)
    {
        if (selectionChiefs)
        {
            selectedChief = (selectedChief + direction) % 4;
            if(selectedChief == -1)
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
