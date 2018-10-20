using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldCover : MonoBehaviour {

    public float maximalShieldTime;
    public float totalCooldown;
    public float preparationTime;
    private float currentCooldown;
    private float currentShieldTime;
    private float currentPreparationTime;
    private float shieldPreparationAngleByTimeUnit;
    private bool doShield;
    private bool isPreparingToShield;

	// Use this for initialization
	void Start () {
        doShield = false;
        currentCooldown = totalCooldown; //to enable it
        shieldPreparationAngleByTimeUnit = 90 / preparationTime;
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (currentCooldown >= totalCooldown)
            {
                isPreparingToShield = true;
                currentShieldTime = 0;
                currentCooldown = 0;
                currentPreparationTime = 0;
            }
        }


        if(isPreparingToShield == true)
        {
            this.transform.Rotate(0, 0, -shieldPreparationAngleByTimeUnit * Time.deltaTime);
            currentPreparationTime += Time.deltaTime;


            if(currentPreparationTime >= preparationTime)
            {
                //fix additional rotation
                transform.localEulerAngles = new Vector3(0, 0, -90);
                isPreparingToShield = false;
                doShield = true;
            }


        }

        if(doShield == true)
        {
            currentShieldTime += Time.deltaTime;
            if(currentShieldTime> maximalShieldTime)
            {
                doShield = false;
                //this.transform.Rotate(0, 0, 90);
                transform.localEulerAngles = new Vector3(0, 0, 0);
                currentCooldown = 0;
            }
        }
        else
        {
            currentCooldown += Time.deltaTime;
        }
        
        
    }
}
