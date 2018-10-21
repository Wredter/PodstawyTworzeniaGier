using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeXbox : ProjectileXbox
{
    private Vector2 initialVelocity;
    private static int initalStepValue = 40;
    private int step;
    private bool isSticked;
    private GameObject objectToStick;

    // Use this for initialization
    void Start()
    {
        step = initalStepValue + 1;
        hasHit = false;
        isReturnable = true;
        isSticked = false;
    }

    public void Initialise(string axeID, VikingXbox player, string controller)
    {
        name = axeID;
        this.player = player;
        rb2d = GetComponent<Rigidbody2D>();

        float projectileX = Input.GetAxis(controller + "RightHorizontal");
        float projectileY = Input.GetAxis(controller + "RightVertical");
        Vector2 projectileThrow = new Vector2(projectileX, projectileY);
        projectileThrow += new Vector2(Input.GetAxis(controller + "LeftHorizontal"), Input.GetAxis(controller + "LeftVertical")) / 3 * 2;
        projectileThrow += new Vector2(randomSpread * 2 * (Random.value - 0.5f), randomSpread * 2 * (Random.value - 0.5f));
        rb2d.AddForce(projectileThrow * 500);
    }

    private void FixedUpdate()
    {
        if (isSticked)
        {
            gameObject.transform.position = objectToStick.transform.position;
            rb2d.velocity = new Vector2(0, 0);
            step = 0;
        }
        else
        {
            if (step == initalStepValue)
            {
                initialVelocity = rb2d.velocity;
            }
            if (step > 0 && step <= initalStepValue)
            {
                rb2d.velocity = initialVelocity * (4 + step / initalStepValue) / 4;
            }
            if (step == 0)
            {
                rb2d.velocity = new Vector2(0, 0);
            }
            step -= 1;
        }
    }

    public void Stick(GameObject objectToStick)
    {
        this.objectToStick = objectToStick;
        isSticked = true;
    }

    public void Unstick()
    {
        isSticked = false;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
