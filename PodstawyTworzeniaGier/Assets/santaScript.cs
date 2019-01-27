using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class santaScript : MonoBehaviour {

    public GameObject peleryna;
    public float cooldown = 4;
    public float skillTime = 9;
    float timer = 0;
    float cooldownTimer = 0;
    bool isSkill = false;
	void Start () {
        peleryna.transform.localScale = new Vector3(1, 1, 1);
    }
	
	// Update is called once per frame
	void Update () {
        timer += Time.deltaTime;
        cooldownTimer += Time.deltaTime;
        if (timer > skillTime && isSkill)
        {
            isSkill = false;
            transform.parent.GetComponentInParent<Horde>().snowBallOnOff(false);
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            Debug.Log("snowball");
            skill();
            
        }

        if (isSkill)
        {
            scl = 1f - (timer - skillTime) * (timer) / skillTime / skillTime *4f;
            peleryna.transform.localScale = new Vector3(scl, scl, 1);
        }
        else
        {
            peleryna.transform.localScale = new Vector3(1, 1, 1);
        }
    }
    float scl;

    public void skill()
    {
        if (cooldownTimer > cooldown || true)
        {
            Debug.Log("snowball on");
            cooldownTimer = 0;
            isSkill = true;
            timer = 0;
            transform.parent.GetComponentInParent<Horde>().snowBallOnOff(true);
            
        }
    }
}
