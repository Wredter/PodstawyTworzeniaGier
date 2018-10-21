using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarScriptXbox : MonoBehaviour
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

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        rb2d.rotation = 0;
    }

    private void LateUpdate()
    {
        transform.rotation = rotation;
        transform.position = parent.GetComponent<Rigidbody2D>().position + new Vector2(-0.3f, 0.3f);
        transform.localScale = new Vector2((parent.GetComponent<MinionBaseXbox>()).health / 100f, 0.1f);
    }
}
