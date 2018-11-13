using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerXbox : MonoBehaviour, IController {
    string deviceSigneture;
    public void SetDeviceSignature(string deviceSigneture)
    {
        this.deviceSigneture = deviceSigneture;
    }

    public float MoveHorizontal()
    {
        return Input.GetAxis(deviceSigneture + "LeftHorizontal");
    }

    public bool Shoot()
    {
        return (Input.GetAxis(deviceSigneture + "T") > 0);
    }

    public bool Special1()
    {
        throw new System.NotImplementedException();
    }

    public bool Special2()
    {
        throw new System.NotImplementedException();
    }

    public float MoveVertical()
    {
        return Input.GetAxis(deviceSigneture + "LeftVertical");
    }

    public float LookHorizontal()
    {
        return Input.GetAxis(deviceSigneture + "RightHorizontal");
    }

    public float LookVertical()
    {
        return Input.GetAxis(deviceSigneture + "RightVertical");
    }
}
