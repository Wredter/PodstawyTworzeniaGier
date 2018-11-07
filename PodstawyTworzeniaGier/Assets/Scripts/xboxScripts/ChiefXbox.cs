using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChiefXbox : MonoBehaviour {
    public GameObject pointer;
    [Range(0.1f,0.99f)]
    public float pointerSmoothness;
    private string controller;
    private GameObject cone;
    private float scale;
    private Stack<float> pointers;
    private Vector2 previous;
    private Vector2 next;

	// Use this for initialization
	void Start () {
        cone = Instantiate(pointer);
        pointers = new Stack<float>();
	}

    private void FixedUpdate()
    {
        float projectileX = Input.GetAxis(controller + "RightHorizontal") * (1.0f-pointerSmoothness) + previous.x * pointerSmoothness;
        float projectileY = Input.GetAxis(controller + "RightVertical") * (1.0f-pointerSmoothness) + previous.y * pointerSmoothness;
        float angle = Mathf.Deg2Rad * Vector2.SignedAngle(Vector2.up, new Vector2(projectileX, projectileY));
        previous = new Vector2(projectileX, projectileY);
        cone.GetComponent<Rigidbody2D>().rotation = angle * Mathf.Rad2Deg;
    }

    private void LateUpdate()
    {
        cone.GetComponent<Rigidbody2D>().position = GetComponent<Rigidbody2D>().position;
        float projectileX = Input.GetAxis(controller + "RightHorizontal");
        float projectileY = Input.GetAxis(controller + "RightVertical");
        float scale = (Mathf.Abs(projectileX) > Mathf.Abs(projectileY)) ? Mathf.Abs(projectileX) : Mathf.Abs(projectileY);
        if (scale > 0.5)
        {
            scale *= 3;
            cone.transform.localScale = new Vector3(scale, scale);
        }
        else
        {
            cone.transform.localScale = new Vector3(0, 0);
        }
    }

    public void SetController(string controller)
    {
        this.controller = controller;
    }
}
