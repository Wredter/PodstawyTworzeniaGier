using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    private Rigidbody2D rb2d;
    private GameObject parent;
    private Quaternion rotation;
    // Use this for initialization
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    public void Initialise(GameObject parentRB2D)
    {
        parent = parentRB2D;
    }

    private void Awake()
    {
        rotation = transform.rotation;
    }

    private void FixedUpdate()
    {
        rb2d.rotation = 0;
    }

    private void LateUpdate()
    {
        transform.rotation = rotation;
        if (parent.GetComponent<MinionBase>())
        {
            transform.position = parent.GetComponent<Rigidbody2D>().position + new Vector2(-0.3f, 0.3f);
            transform.localScale = new Vector2(parent.GetComponent<MinionBase>().GetActualHealth() / parent.GetComponent<MinionBase>().health, 0.1f);
        }
        else
        {
            transform.position = parent.GetComponent<Rigidbody2D>().position + new Vector2(-1f, 0.5f);
            transform.localScale = new Vector2(parent.GetComponent<ChiefBase>().GetActualHealth() / parent.GetComponent<ChiefBase>().health, 0.15f)*6;
        }
    }
}
