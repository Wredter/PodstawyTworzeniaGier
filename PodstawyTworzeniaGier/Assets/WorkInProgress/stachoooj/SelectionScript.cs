using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectionScript : MonoBehaviour {

    public List<Canvas> chiefCanvases = new List<Canvas>();
    public List<Sprite> chiefs = new List<Sprite>();
    public List<Canvas> minionCanvases = new List<Canvas>();
    public List<Sprite> minions = new List<Sprite>();
    public string deviceSignature;

    private IController controller;
    private bool selected = false;
    private bool selectionChiefs = true;
    private List<GameObject> selectionsChief;
    private int selectedChief;
    private List<GameObject> selectionsMinions;
    private int selectedMinion;

    private bool notMovedHorizontal;

	void Start () {
        switch(deviceSignature)
        {
            case "Joystick1":
            case "Joystick2":
                controller = new ControllerXbox();
                break;
            case "":
                controller = new ControllerMouseAndKeyboard();
                break;
        }
        controller.SetDeviceSignature(deviceSignature);

        for (int i = 0; i < chiefCanvases.Count; i++)
        {
            GameObject newObject = new GameObject();
            Image newImage = newObject.AddComponent<Image>();
            newImage.sprite = chiefs[i];
            newObject.GetComponent<RectTransform>().SetParent(chiefCanvases[i].transform);
            newObject.transform.position = chiefCanvases[i].transform.position;
            newObject.SetActive(true);
        }

        for (int i = 0; i < minionCanvases.Count; i++)
        {
            GameObject newObject = new GameObject();
            Image newImage = newObject.AddComponent<Image>();
            newImage.sprite = minions[i];
            newObject.GetComponent<RectTransform>().SetParent(minionCanvases[i].transform);
            newObject.transform.position = minionCanvases[i].transform.position;
            newObject.SetActive(true);
        }
    }
	
	void Update () {
        //Checks if player moved carousel to left or right
        if(controller.MoveHorizontal() > 0.5 && notMovedHorizontal)
        {
            notMovedHorizontal = false;
            if(selectionChiefs)
            {

            }
            else
            {

            }
        }
        else if (controller.MoveHorizontal() < -0.5 && notMovedHorizontal)
        {
            notMovedHorizontal = false;
            if (selectionChiefs)
            {

            }
            else
            {

            }
        }
        else
        {
            notMovedHorizontal = true;
        }
    }

    private void moveCarousel(int direction, int selectedElement, List<Sprite> elements, List<Canvas> elementsPlaces, List<GameObject> activeElements)
    {
        foreach(GameObject go in activeElements)
        {
            Destroy(go.gameObject);
        }
        activeElements.Clear();

        //Changing selected element to new element
        selectedElement += direction;
        if (selectedElement < 0) selectedElement = elements.Count - 1;
        if (selectedElement == elements.Count) selectedElement = 0;


    }
}
