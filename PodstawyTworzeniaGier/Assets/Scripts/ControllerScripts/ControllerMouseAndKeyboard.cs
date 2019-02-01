using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerMouseAndKeyboard : MonoBehaviour, IController
{
    private string deviceSignature;

    public bool Back()
    {
        return Input.GetKey(KeyCode.Escape);
    }

    public float LookHorizontal()
    {
        if (Input.GetKey(KeyCode.LeftArrow)) return -1;
        if (Input.GetKey(KeyCode.RightArrow)) return 1;
        return 0;
        //return Input.GetAxis(deviceSignature + "Horizontal2");
    }

    public float LookVertical()
    {
        if (Input.GetKey(KeyCode.DownArrow)) return -1;
        if (Input.GetKey(KeyCode.UpArrow)) return 1;
        return 0;
        //return Input.GetAxis(deviceSignature + "Vertical2");
    }

    public float MoveHorizontal()
    {
        if (Input.GetKey(KeyCode.A)) return -1;
        if (Input.GetKey(KeyCode.D)) return 1;
        return 0;
        //return Input.GetAxis(deviceSignature + "Horizontal");
    }

    public float MoveVertical()
    {
        if (Input.GetKey(KeyCode.S)) return -1;
        if (Input.GetKey(KeyCode.W)) return 1;
        return 0;
        //return Input.GetAxis(deviceSignature + "Vertical");
    }

    public bool Select()
    {
        return Input.GetKey(KeyCode.Return);
    }

    public void SetDeviceSignature(string deviceSignature)
    {
        this.deviceSignature = deviceSignature;
    }

    public bool Block()
    {
        return Input.GetKey(KeyCode.LeftControl);
    }

    public bool Shoot()
    {
        return Input.GetKey(KeyCode.Space);
    }

    public bool Special1()
    {
        return Input.GetKey(KeyCode.E);
    }

    public bool Special2()
    {
        return Input.GetKey(KeyCode.Q);
    }
}
