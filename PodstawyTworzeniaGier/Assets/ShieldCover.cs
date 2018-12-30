using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldCover : MonoBehaviour {

    public float maximalShieldTime;
    [Range(0,0.05f)]
    public float shieldScaler;
    public float totalCooldown;
    public float preparationTime;
    private float currentCooldown;
    private float currentShieldTime;
    private float currentPreparationTime;
    private float shieldPreparationAngleByTimeUnit;
    private bool doShield;
    private bool isPreparingToShield;
    private Vector3 basicScale;

	// Use this for initialization
	void Start () {
        doShield = false;
        basicScale = this.transform.localScale;
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
            this.transform.localScale += new Vector3(shieldScaler, shieldScaler, shieldScaler);
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
                this.transform.localScale = basicScale;
            }
        }
        else
        {
            currentCooldown += Time.deltaTime;
        }
        
        
    }
}
