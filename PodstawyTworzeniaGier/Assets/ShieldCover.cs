using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldCover : MonoBehaviour {

    public float maximalShieldTime;
    public float totalCooldown;
    private float currentCooldown;
    private float currentShieldTime;
    private float currentRotation;
    private bool doShield;

	// Use this for initialization
	void Start () {
        doShield = false;
        currentCooldown = totalCooldown; //to enable it
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (currentCooldown >= totalCooldown)
            {
                doShield = true;
                currentShieldTime = 0;
                currentCooldown = 0;
                this.transform.Rotate(0, 0, -90);
            }
        }

        if(doShield == true)
        {
            currentShieldTime += Time.deltaTime;
            if(currentShieldTime> maximalShieldTime)
            {
                doShield = false;
                this.transform.Rotate(0, 0, 90);
                currentCooldown = 0;
            }
        }
        else
        {
            currentCooldown += Time.deltaTime;
        }
        
        
    }
}
