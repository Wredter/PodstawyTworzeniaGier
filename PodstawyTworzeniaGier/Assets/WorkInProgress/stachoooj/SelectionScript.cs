using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectionScript : MonoBehaviour
{

    public List<Canvas> chiefCanvases = new List<Canvas>();
    public List<Sprite> chiefs = new List<Sprite>();
    public List<Canvas> minionCanvases = new List<Canvas>();
    public List<Sprite> minions = new List<Sprite>();
    public Sprite selectionFrame;
    public string deviceSignature;

    private IController controller;
    private bool selected = false;
    private bool selectionChiefs = true;
    private List<GameObject> selectionsChief = new List<GameObject>();
    private int selectedChief;
    private List<GameObject> selectionsMinion = new List<GameObject>();
    private int selectedMinion;
    private GameObject frame;

    private bool notMovedHorizontal;
    private bool notMovedVertical;

    void Start()
    {
        switch (deviceSignature)
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
            selectionsChief.Add(newObject);
        }
        selectionsChief[0].transform.localScale = new Vector2(1, 1);
        selectionsChief[1].transform.localScale = new Vector2(1.25f, 1.25f);
        selectionsChief[2].transform.localScale = new Vector2(1f, 1);

        for (int i = 0; i < minionCanvases.Count; i++)
        {
            GameObject newObject = new GameObject();
            Image newImage = newObject.AddComponent<Image>();
            newImage.sprite = minions[i];
            newObject.GetComponent<RectTransform>().SetParent(minionCanvases[i].transform);
            newObject.transform.position = minionCanvases[i].transform.position;
            newObject.SetActive(true);
            selectionsMinion.Add(newObject);
        }
        selectionsMinion[0].transform.localScale = new Vector2(1, 1);
        selectionsMinion[1].transform.localScale = new Vector2(1.25f, 1.25f);
        selectionsMinion[2].transform.localScale = new Vector2(1, 1);

        frame = new GameObject();
        CreateFrame();
    }

    void Update()
    {
        //Checks if player moved carousel to left or right
        if (controller.MoveHorizontal() > 0.5 && notMovedHorizontal)
        {
            notMovedHorizontal = false;
            if (selectionChiefs)
            {
                selectedChief = MoveCarousel(1, selectedChief, chiefs, chiefCanvases, selectionsChief);
            }
            else
            {
                selectedMinion = MoveCarousel(1, selectedMinion, minions, minionCanvases, selectionsMinion);
            }
        }
        else if (controller.MoveHorizontal() < -0.5 && notMovedHorizontal)
        {
            notMovedHorizontal = false;
            if (selectionChiefs)
            {
                selectedChief = MoveCarousel(-1, selectedChief, chiefs, chiefCanvases, selectionsChief);
            }
            else
            {
                selectedMinion = MoveCarousel(-1, selectedMinion, minions, minionCanvases, selectionsMinion);
            }
        }
        else if(controller.MoveHorizontal() <= .5 && controller.MoveHorizontal() >= -.5)
        {
            notMovedHorizontal = true;
        }

        //Checks if player selected other carousel
        if((controller.MoveVertical() > .5 || controller.MoveVertical() < -.5) && notMovedVertical)
        {
            notMovedVertical = false;
            selectionChiefs = !selectionChiefs;
            CreateFrame();
        }
        else if((controller.MoveVertical() <= .5 && controller.MoveVertical() >= -.5))
        {
            notMovedVertical = true;
        }
    }

    private int MoveCarousel(int direction, int selectedElement, List<Sprite> elements, List<Canvas> elementsPlaces, List<GameObject> activeElements)
    {
        foreach (GameObject go in activeElements)
        {
            Destroy(go.gameObject);
        }
        activeElements.Clear();

        //Changing selected element to new element
        selectedElement += direction;
        if (selectedElement < 0) selectedElement = elements.Count - 1;
        if (selectedElement == elements.Count) selectedElement = 0;

        List<Sprite> spritesForPlaces = new List<Sprite>(3);
        if (selectedElement == 0)
        {
            spritesForPlaces.Add(elements[elements.Count - 1]);
            spritesForPlaces.Add(elements[selectedElement]);
            spritesForPlaces.Add(elements[selectedElement + 1]);
        }
        else if (selectedElement == elements.Count - 1)
        {
            spritesForPlaces.Add(elements[selectedElement - 1]);
            spritesForPlaces.Add(elements[selectedElement]);
            spritesForPlaces.Add(elements[0]);
        }
        else
        {
            spritesForPlaces.Add(elements[selectedElement - 1]);
            spritesForPlaces.Add(elements[selectedElement]);
            spritesForPlaces.Add(elements[selectedElement + 1]);
        }

        activeElements.Add(CreateImage(spritesForPlaces[0], elementsPlaces[0]));
        activeElements.Add(CreateImage(spritesForPlaces[1], elementsPlaces[1]));
        activeElements.Add(CreateImage(spritesForPlaces[2], elementsPlaces[2]));

        activeElements[0].transform.localScale = new Vector2(1, 1);
        activeElements[1].transform.localScale = new Vector2(1.25f, 1.25f);
        activeElements[2].transform.localScale = new Vector2(1, 1);

        CreateFrame();

        return selectedElement;
    }

    private GameObject CreateImage(Sprite sprite, Canvas canvas)
    {
        GameObject newObject = new GameObject();
        Image newImage = newObject.AddComponent<Image>();
        newImage.sprite = sprite;
        newObject.GetComponent<RectTransform>().SetParent(canvas.transform);
        newObject.transform.position = canvas.transform.position;
        newObject.SetActive(true);
        return newObject;
    }

    private void CreateFrame()
    {
        Destroy(frame.gameObject);
        Canvas canvas = null;
        if (selectionChiefs)
        {
            canvas = chiefCanvases[1];
        }
        else
        {
            canvas = minionCanvases[1];
        }
        GameObject newObject = new GameObject();
        Image newImage = newObject.AddComponent<Image>();
        newImage.sprite = selectionFrame;
        newObject.GetComponent<RectTransform>().SetParent(canvas.transform);
        newObject.transform.position = canvas.transform.position;
        newObject.SetActive(true);
        newObject.transform.localScale = new Vector2(1.6f, 2.8f);
        frame = newObject;
    }
}
