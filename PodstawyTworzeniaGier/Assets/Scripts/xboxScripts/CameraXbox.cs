using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraXbox : MonoBehaviour {
    public GameObject parent;
    public bool isPlayer2;
    public GameObject map;
    private SpriteRenderer mapRenderer;
    private Vector2 newPosition;
    private Camera cam;
	void Start () {
        mapRenderer = map.GetComponent<SpriteRenderer>();
        cam = GetComponent<Camera>();
	}
	
	void LateUpdate () {
        newPosition = parent.GetComponent<PlayerHUDScriptXbox>().GetHordeCenter();
        GetComponent<Rigidbody2D>().position = newPosition;
        cam.transform.position = new Vector3(newPosition.x,newPosition.y,-10);
        
        float pomX = cam.ScreenToWorldPoint(new Vector3(cam.pixelWidth, 0, cam.nearClipPlane)).x - cam.ScreenToWorldPoint(new Vector3(cam.pixelWidth / 2, 0, cam.nearClipPlane)).x;
        float pomY = cam.ScreenToWorldPoint(new Vector3(0, cam.pixelHeight, cam.nearClipPlane)).y - cam.ScreenToWorldPoint(new Vector3(0, cam.pixelHeight/2, cam.nearClipPlane)).y;
        
        if (isPlayer2)
        {
            if (cam.ScreenToWorldPoint(new Vector3(cam.pixelWidth, 0, cam.nearClipPlane)).x > mapRenderer.bounds.size.x / 2 - pomX*2)
            {
                newPosition.x = mapRenderer.bounds.size.x / 2 - pomX;
            }
            if (cam.ScreenToWorldPoint(new Vector3(0, 0, cam.nearClipPlane)).x < -mapRenderer.bounds.size.x / 2 - pomX*2)
            {
                newPosition.x = -mapRenderer.bounds.size.x / 2 + pomX;
            }
            if (cam.ScreenToWorldPoint(new Vector3(0, cam.pixelHeight, cam.nearClipPlane)).y > mapRenderer.bounds.size.y / 2)
            {
                newPosition.y = mapRenderer.bounds.size.y / 2 - pomY;
            }
            if (cam.ScreenToWorldPoint(new Vector3(0, 0, cam.nearClipPlane)).y < -mapRenderer.bounds.size.y / 2)
            {
                newPosition.y = -mapRenderer.bounds.size.y / 2 + pomY;
            }
        }
        else
        {
            if (cam.ScreenToWorldPoint(new Vector3(cam.pixelWidth, 0, cam.nearClipPlane)).x > mapRenderer.bounds.size.x / 2)
            {
                newPosition.x = mapRenderer.bounds.size.x / 2 - pomX;
            }
            if (cam.ScreenToWorldPoint(new Vector3(0, 0, cam.nearClipPlane)).x < -mapRenderer.bounds.size.x / 2)
            {
                newPosition.x = -mapRenderer.bounds.size.x / 2 + pomX;
            }
            if (cam.ScreenToWorldPoint(new Vector3(0, cam.pixelHeight, cam.nearClipPlane)).y > mapRenderer.bounds.size.y / 2)
            {
                newPosition.y = mapRenderer.bounds.size.y / 2 - pomY;
            }
            if (cam.ScreenToWorldPoint(new Vector3(0, 0, cam.nearClipPlane)).y < -mapRenderer.bounds.size.y / 2)
            {
                newPosition.y = -mapRenderer.bounds.size.y / 2 + pomY;
            }
        }
        cam.transform.position = new Vector3(newPosition.x, newPosition.y, -10);
        GetComponent<Rigidbody2D>().position = newPosition;
    }
}
