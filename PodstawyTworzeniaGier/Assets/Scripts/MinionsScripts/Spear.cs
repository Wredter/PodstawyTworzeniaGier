using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spear : MonoBehaviour
{
    public float duration = 0;
    public float t = 0;

    private Quaternion startRot;

    private void Start()
    {
        startRot = transform.rotation;
    }

    void Update()
    {
        if (t < duration)
        {
            t += Time.deltaTime;
            transform.localRotation = Quaternion.AngleAxis(t / duration * 360f, Vector3.forward); //or transform.right if you want it to be locally based
            Debug.Log(duration);
        }
        else
        {
            duration = -1;
            t = 0.0f;
            transform.localRotation = transform.parent.rotation;
        }
    }
}
