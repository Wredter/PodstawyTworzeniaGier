using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChiefXbox : MonoBehaviour {
    public GameObject pointer;
    private string controller;
    private GameObject cone;
    private float scale;
	// Use this for initialization
	void Start () {
        cone = Instantiate(pointer);
	}
	
	// Update is called once per frame
	void Update () {

	}

    private void LateUpdate()
    {
        cone.GetComponent<Rigidbody2D>().position = GetComponent<Rigidbody2D>().position;
        float projectileX = Input.GetAxis(controller + "RightHorizontal");
        float projectileY = Input.GetAxis(controller + "RightVertical");
        float angle = Mathf.Deg2Rad * Vector2.SignedAngle(Vector2.up, new Vector2(projectileX, projectileY));
        cone.GetComponent<Rigidbody2D>().rotation = angle*Mathf.Rad2Deg;
        float scale = (Mathf.Abs(projectileX) > Mathf.Abs(projectileY)) ? Mathf.Abs(projectileX) : Mathf.Abs(projectileY);
        scale *= 3;
        cone.transform.localScale = new Vector3(scale, scale);
    }

    public void SetController(string controller)
    {
        this.controller = controller;
    }
}
