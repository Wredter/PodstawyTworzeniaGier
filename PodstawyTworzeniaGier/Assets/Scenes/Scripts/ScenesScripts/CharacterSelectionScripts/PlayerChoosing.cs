using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerChoosing : MonoBehaviour {
    public string deviceSignatue;
    public ChoiceControll choice;
    public Image archersIcon;
    public Image archersImage;
    public Image vikingIcon;
    public Image vikingImage;
    public Image zombiesIcon;
    public Image zombiesImage;
    public Image spartansIcon;
    public Image spartansImage;
    public Text pressX;
    public Text infoForPlayer;
    private IController controller;
    private List<Image> icons;
    private List<Image> images;
    private bool pressedStart;
    private bool movedThisTime;
    private bool block;
    private bool block2;
    private bool confirm;
    private bool confirm2;
    private int selected;
    private float scale;
    private Image highlighted;

	void Start () {
		switch(deviceSignatue)
        {
            case "Joystick1":
            case "Joystick2":
                controller = gameObject.AddComponent(typeof(ControllerXbox)) as ControllerXbox;
                break;
            case "":
                controller = gameObject.AddComponent(typeof(ControllerMouseAndKeyboard)) as ControllerMouseAndKeyboard;
                break;
        }
        controller.SetDeviceSignature(deviceSignatue);
        icons = new List<Image>();
        images = new List<Image>()
        {
            archersImage,
            vikingImage,
            zombiesImage,
            spartansImage
        };
        pressedStart = false;
        confirm = false;
        movedThisTime = false;
        selected = 0;
        block = false;
        block2 = false;
	}
	
	void Update () {
        //Handles pressing buttons
        if(controller.Select())
        {
            if (!pressedStart)
            {
                LoadPrefabs();
                block = true;
                infoForPlayer.text = "Press 'A' to choose character.";
            } else if(!confirm && !block)
            {
                confirm = true;
                block = true;
                infoForPlayer.text = "Press 'A' to be ready";
            } else if(!block)
            {
                confirm2 = true;
                block = true;
                Ready();
                infoForPlayer.text = "Waiting for other player to choose.";
            }
        }
        if(controller.Back())
        {
            if(confirm2 && !block2)
            {
                confirm2 = false;
                block2 = true;
                NotReady();
                infoForPlayer.text = "Press 'A' to be ready";
            } else if(confirm && !block2)
            {
                confirm = false;
                block2 = true;
                infoForPlayer.text = "Press 'A' to choose character";
            }
        }
        if(!controller.Select())
        {
            block = false;
        }
        if (!controller.Back())
        {
            block2 = false;
        }
        //Handles left horizontal for choosing character
        if (pressedStart && Mathf.Abs(controller.MoveVertical()) > 0.5 && !movedThisTime && !confirm)
        {
            ChangeCharacter();            
            movedThisTime = true;
        } else if(Mathf.Abs(controller.MoveVertical()) <= 0.5 && !confirm)
        {
            movedThisTime = false;
        }
	}

    private void LoadPrefabs()
    {
        pressedStart = true;
        Destroy(pressX);
        icons.Add(Instantiate(archersIcon) as Image);
        icons[icons.Count - 1].transform.SetParent(transform);
        //Setting scale
        scale = icons[icons.Count - 1].transform.localScale.x;

        icons[icons.Count - 1].transform.localPosition = new Vector3(-100, 150, 0);
        icons[icons.Count - 1].transform.localScale = new Vector3(scale * 1.25f, scale * 1.25f, scale * 1.25f);
        icons.Add(Instantiate(vikingIcon) as Image);
        icons[icons.Count - 1].transform.SetParent(transform);
        icons[icons.Count - 1].transform.localPosition = new Vector3(-100, 50, 0);
        icons.Add(Instantiate(zombiesIcon) as Image);
        icons[icons.Count - 1].transform.SetParent(transform);
        icons[icons.Count - 1].transform.localPosition = new Vector3(-100, -50, 0);
        icons.Add(Instantiate(spartansIcon) as Image);
        icons[icons.Count - 1].transform.SetParent(transform);
        icons[icons.Count - 1].transform.localPosition = new Vector3(-100, -150, 0);

        highlighted = Instantiate(archersImage) as Image;
        highlighted.transform.SetParent(transform);
        highlighted.transform.localPosition = new Vector3(50, 0, 0);
    }

    private void ChangeCharacter()
    {
        int next;
        if (controller.MoveVertical() > 0)
        {
            next = (selected - 1) % icons.Count;
            if (next == -1)
            {
                next = icons.Count - 1;
            }
        }
        else
        {
            next = (selected + 1) % icons.Count;
        }
        icons[next].transform.localScale = new Vector3(scale * 1.25f, scale * 1.25f, scale * 1.25f);
        icons[selected].transform.localScale = new Vector3(scale * 0.8f, scale * 0.8f, scale * 0.8f);
        selected = next;
        Destroy(highlighted.gameObject);
        highlighted = Instantiate(images[selected]) as Image;
        highlighted.transform.SetParent(transform);
        highlighted.transform.localPosition = new Vector3(50, 0, 0);
    }

    private void Ready()
    {
        PlayerPrefs.SetString(name, HordeNameFromNumber());
        PlayerPrefs.SetString(name + "device", deviceSignatue);
        choice.GetComponent<ChoiceControll>().NotifyThatChose(gameObject);
    }

    private void NotReady()
    {
        PlayerPrefs.DeleteKey(name);
        PlayerPrefs.DeleteKey(name + "device");
        choice.GetComponent<ChoiceControll>().NotifyThatHadntChose(gameObject);
    }

    private string HordeNameFromNumber()
    {
        string s = "";
        switch (selected)
        {
            case 0:
                s = "archers";
                break;
            case 1:
                s = "vikings";
                break;
            case 2:
                s = "zombies";
                break;
            case 3:
                s = "spartans";
                break;
        }
        return s;
    }
}
