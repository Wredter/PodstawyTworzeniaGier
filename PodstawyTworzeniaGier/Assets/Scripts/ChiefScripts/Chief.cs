using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chief : MonoBehaviour , IPlayerIntegration{
    public GameObject pointer;
    //public float slow = 1f;
    //public float maxSpeed = 10;
    protected string playerName;
    [Range(0.1f,0.99f)]
    public float pointerSmoothness;
    protected IController controller;
    private GameObject cone;
    private float scale;
    private Vector2 previous;
    private Vector2 next;
    protected Rigidbody2D rb2d;
    private float points = 0;

    // Use this for initialization
    void Start () {
        cone = Instantiate(pointer);
        rb2d = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        float projectileX = controller.LookHorizontal() * (1.0f-pointerSmoothness) + previous.x * pointerSmoothness;
        float projectileY = controller.LookVertical() * (1.0f-pointerSmoothness) + previous.y * pointerSmoothness;

        float angle = Mathf.Deg2Rad * Vector2.SignedAngle(Vector2.up, new Vector2(projectileX, projectileY));
        previous = new Vector2(projectileX, projectileY);
        cone.GetComponent<Rigidbody2D>().rotation = angle * Mathf.Rad2Deg;

        //float moveX = controller.MoveHorizontal();
        //float moveY = controller.MoveVertical();

        //rb2d.velocity = new Vector2(moveX * maxSpeed * slow, moveY * maxSpeed * slow);
    }

    private void LateUpdate()
    {
        cone.GetComponent<Rigidbody2D>().position = GetComponent<Rigidbody2D>().position;
        float projectileX = controller.LookHorizontal();
        float projectileY = controller.LookVertical();
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
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Mud")
        {
            GetComponentInParent<Horde>().slow = 0.5f;
        }
    }

    public void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Mud")
        {
            GetComponentInParent<Horde>().slow = 0.5f;
        }
        if (collision.gameObject.tag == "PointsAddArea")
        {
            Debug.Log(playerName);
            points += 0.01f;
            Debug.Log(points);
            PlayerPrefs.SetInt(playerName + "score", (int)points);
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Mud")
        {
            GetComponentInParent<Horde>().slow = 1f;
        }

    }
    #region Geters and seters
    public void SetController(IController controller)
    {
        this.controller = controller;
    }

    public IController GetController()
    {
        return controller;
    }

    public Vector2 GetPrevious()
    {
        return previous;
    }
    public string GetPlayerName()
    {
        return playerName;
    }

    public void SetPlayerName(string playerName)
    {
        this.playerName = playerName;
    }
    #endregion
}
