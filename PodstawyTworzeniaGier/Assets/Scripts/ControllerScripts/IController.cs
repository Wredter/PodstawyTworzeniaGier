using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IController {
    void SetDeviceSignature(string deviseSignature);
    bool Shoot();
    bool Special1();
    bool Special2();
    float MoveHorizontal();
    float MoveVertical();
    float LookHorizontal();
    float LookVertical();
}
