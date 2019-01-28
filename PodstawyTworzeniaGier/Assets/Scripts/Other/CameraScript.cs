using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public GameObject parent;
    public GameObject map;
    private SpriteRenderer mapRenderer;
    private Vector2 newPosition;
    private Camera cam;
    void Start()
    {
        mapRenderer = map.GetComponent<SpriteRenderer>();
        cam = GetComponent<Camera>();
    }

    void LateUpdate()
    {
        newPosition = parent.GetComponent<PlayerHUD>().GetHordeCenter();
        GetComponent<Rigidbody2D>().position = newPosition;
        cam.transform.position = new Vector3(newPosition.x, newPosition.y, -10);

        float pomX = cam.ScreenToWorldPoint(new Vector3(cam.pixelWidth, 0, cam.nearClipPlane)).x - cam.ScreenToWorldPoint(new Vector3(cam.pixelWidth / 2, 0, cam.nearClipPlane)).x;
        float pomY = cam.ScreenToWorldPoint(new Vector3(0, cam.pixelHeight, cam.nearClipPlane)).y - cam.ScreenToWorldPoint(new Vector3(0, cam.pixelHeight / 2, cam.nearClipPlane)).y;
        switch (transform.parent.name)
        {
            case "Player1":
                if (cam.ScreenToWorldPoint(new Vector3(cam.pixelWidth, 0, cam.nearClipPlane)).x > mapRenderer.bounds.size.x / 2)
                {
                    newPosition.x = mapRenderer.bounds.size.x / 2 - pomX;
                }
                if (cam.ScreenToWorldPoint(new Vector3(0, 0, cam.nearClipPlane)).x < -mapRenderer.bounds.size.x / 2)
                {
                    newPosition.x = -mapRenderer.bounds.size.x / 2 + pomX;
                }
                if (cam.ScreenToWorldPoint(new Vector3(0, cam.pixelHeight, cam.nearClipPlane)).y > mapRenderer.bounds.size.y / 8)
                {
                    newPosition.y = mapRenderer.bounds.size.y / 2 - pomY;
                }
                if (cam.ScreenToWorldPoint(new Vector3(0, 0, cam.nearClipPlane)).y < -mapRenderer.bounds.size.y / 8 * 7)
                {
                    newPosition.y = -mapRenderer.bounds.size.y / 2 + pomY;
                }
                break;
            case "Player2":
                if (cam.ScreenToWorldPoint(new Vector3(cam.pixelWidth, 0, cam.nearClipPlane)).x > mapRenderer.bounds.size.x / 2 - pomX * 2)
                {
                    newPosition.x = mapRenderer.bounds.size.x / 2 - pomX;
                }
                if (cam.ScreenToWorldPoint(new Vector3(0, 0, cam.nearClipPlane)).x < -mapRenderer.bounds.size.x / 2 - pomX * 2)
                {
                    newPosition.x = -mapRenderer.bounds.size.x / 2 + pomX;
                }
                if (cam.ScreenToWorldPoint(new Vector3(0, cam.pixelHeight, cam.nearClipPlane)).y > mapRenderer.bounds.size.y / 8)
                {
                    newPosition.y = mapRenderer.bounds.size.y / 2 - pomY;
                }
                if (cam.ScreenToWorldPoint(new Vector3(0, 0, cam.nearClipPlane)).y < -mapRenderer.bounds.size.y / 8 * 7)
                {
                    newPosition.y = -mapRenderer.bounds.size.y / 2 + pomY;
                }
                break;
            case "Player3":
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
                break;
            case "Player4":
                if (cam.ScreenToWorldPoint(new Vector3(cam.pixelWidth, 0, cam.nearClipPlane)).x > mapRenderer.bounds.size.x / 2 - pomX * 2)
                {
                    newPosition.x = mapRenderer.bounds.size.x / 2 - pomX;
                }
                if (cam.ScreenToWorldPoint(new Vector3(0, 0, cam.nearClipPlane)).x < -mapRenderer.bounds.size.x / 2 - pomX * 2)
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
                break;
        }
        cam.transform.position = new Vector3(newPosition.x, newPosition.y, -10);
        GetComponent<Rigidbody2D>().position = newPosition;
    }
}
