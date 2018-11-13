using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


public class ControllerXbox : MonoBehaviour, IController {
    string deviceSignature;
    public void SetDeviceSignature(string deviceSignature)
    {
        this.deviceSignature = deviceSignature;
    }

    public float MoveHorizontal()
    {
        return Input.GetAxis(deviceSignature + "LeftHorizontal");
    }

    public bool Shoot()
    {
        return (Input.GetAxis(deviceSignature + "T") > 0);
    }

    public bool Special1()
    {
        return Input.GetButton(deviceSignature + "LB");
    }

    public bool Special2()
    {
        return Input.GetButton(deviceSignature + "RB");
    }

    public float MoveVertical()
    {
        return Input.GetAxis(deviceSignature + "LeftVertical");
    }

    public float LookVertical()
    {
        return Input.GetAxis(deviceSignature + "RightVertical");
    }

    public float LookHorizontal()
    {
        return Input.GetAxis(deviceSignature + "RightHorizontal");
    }
}
