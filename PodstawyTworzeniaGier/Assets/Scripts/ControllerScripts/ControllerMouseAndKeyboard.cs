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
        return Input.GetAxis(deviceSignature + "Horizontal2");
    }

    public float LookVertical()
    {
        return Input.GetAxis(deviceSignature + "Vertical2");
    }

    public float MoveHorizontal()
    {
        return Input.GetAxis(deviceSignature + "Horizontal");
    }

    public float MoveVertical()
    {
        return Input.GetAxis(deviceSignature + "Vertical");
    }

    public bool Select()
    {
        return Input.GetKey(KeyCode.Return);
    }

    public void SetDeviceSignature(string deviceSignature)
    {
        this.deviceSignature = deviceSignature;
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
