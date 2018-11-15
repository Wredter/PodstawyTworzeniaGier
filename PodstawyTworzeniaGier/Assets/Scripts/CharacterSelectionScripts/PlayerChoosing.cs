using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerChoosing : MonoBehaviour {
    public string deviceSignatue;
    public Image archers;
    public Image vikings;
    public Image zombies;
    public Image spartans;
    public Text pressX;
    private IController controller;
    private List<Image> selections;
    private bool pressedStart;
    private bool movedThisTime;
    private int selected;
    private float scale;

	void Start () {
		switch(deviceSignatue)
        {
            case "Joystick1":
            case "Joystick2":
                controller = gameObject.AddComponent(typeof(ControllerXbox)) as ControllerXbox;
                controller.SetDeviceSignature(deviceSignatue);
                break;
        }
        selections = new List<Image>();
        pressedStart = false;
        movedThisTime = false;
        selected = 0;
	}
	
	void Update () {
        if(controller.Select() && !pressedStart)
        {
            pressedStart = true;
            Destroy(pressX);
            selections.Add(Instantiate(archers) as Image);
            selections[selections.Count - 1].transform.SetParent(transform);
            //Setting scale
            scale = selections[selections.Count - 1].transform.localScale.x;

            selections[selections.Count - 1].transform.localPosition = new Vector3(-100, 150, 0);
            selections[selections.Count - 1].transform.localScale = new Vector3(scale * 1.25f,scale * 1.25f,scale *1.25f);
            selections.Add(Instantiate(vikings) as Image);
            selections[selections.Count - 1].transform.SetParent(transform);
            selections[selections.Count - 1].transform.localPosition = new Vector3(-100, 50, 0);
            selections.Add(Instantiate(zombies) as Image);
            selections[selections.Count - 1].transform.SetParent(transform);
            selections[selections.Count - 1].transform.localPosition = new Vector3(-100, -50, 0);
            selections.Add(Instantiate(spartans) as Image);
            selections[selections.Count - 1].transform.SetParent(transform);
            selections[selections.Count - 1].transform.localPosition = new Vector3(-100, -150, 0);
        }
        if (pressedStart && Mathf.Abs(controller.MoveVertical()) > 0.5 && !movedThisTime)
        {
            int next;
            movedThisTime = true;
            if(controller.MoveVertical() > 0)
            {
                next = (selected - 1) % selections.Count;
                if(next == -1)
                {
                    next = selections.Count - 1;
                }
            } else
            {
                next = (selected + 1) % selections.Count;
            }
            selections[next].transform.localScale = new Vector3(scale * 1.25f, scale * 1.25f, scale * 1.25f);
            selections[selected].transform.localScale = new Vector3(scale * 0.8f, scale * 0.8f, scale * 0.8f);
            selected = next;
        } else if(Mathf.Abs(controller.MoveVertical()) <= 0.5)
        {
            movedThisTime = false;
        }
	}
}
