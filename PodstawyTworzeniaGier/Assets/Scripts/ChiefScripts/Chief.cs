using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chief : MonoBehaviour , IPlayerIntegration{
    public GameObject pointer;
    protected string playerName;
    [Range(0.1f,0.99f)]
    public float pointerSmoothness;
    private IController controller;
    private GameObject cone;
    private float scale;
    private Vector2 previous;
    private Vector2 next;
    protected Rigidbody2D rb2d;

    // Use this for initialization
    void Start () {
        cone = Instantiate(pointer);
        if (cone)
        {
            Debug.Break();
        }
        rb2d = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        float projectileX = controller.LookHorizontal() * (1.0f-pointerSmoothness) + previous.x * pointerSmoothness;
        float projectileY = controller.LookVertical() * (1.0f-pointerSmoothness) + previous.y * pointerSmoothness;

        float angle = Mathf.Deg2Rad * Vector2.SignedAngle(Vector2.up, new Vector2(projectileX, projectileY));
        previous = new Vector2(projectileX, projectileY);
        cone.GetComponent<Rigidbody2D>().rotation = angle * Mathf.Rad2Deg;
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
        if (collision.GetComponent<Projectile>())
        {
            if (collision.GetComponent<IPlayerIntegration>().GetPlayerName() != playerName)
            {
                //DealDamage(collision.GetComponent<Projectile>().damage);
            }
        }
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
