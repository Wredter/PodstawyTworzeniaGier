using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraMovement : MonoBehaviour {
    public GameObject obj;
    private Vector3 offset;
	// Use this for initialization
	void Start () {
        transform.position = new Vector3(obj.transform.position.x, obj.transform.position.y,-10);

        offset = transform.position - obj.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = obj.transform.position + offset;


    }
}
