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
        return Input.mousePosition.x;
    }

    public float LookVertical()
    {
        return Input.mousePosition.y;
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
        return Input.GetMouseButtonDown(0);
    }

    public bool Special1()
    {
        throw new System.NotImplementedException();
    }

    public bool Special2()
    {
        throw new System.NotImplementedException();
    }
}
