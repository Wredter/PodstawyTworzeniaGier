using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class santaScript : MonoBehaviour {

    public GameObject peleryna;
    public float cooldown = 4;
    public float skillTime = 3;
    float timer = 0;
    float cooldownTimer = 0;
    bool isSkill = false;
	void Start () {
		
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
            skill();
            Debug.Log("snowball");
        }

        if (isSkill)
        {
            scl = 0.2f - (timer - skillTime) * (timer) / skillTime / skillTime *1.3f;
            peleryna.transform.localScale = new Vector3(scl, scl, 1);
        }
        else
        {
            peleryna.transform.localScale = new Vector3(0.2f, 0.2f, 1);
        }
    }
    float scl;

    public void skill()
    {
        if (cooldownTimer > cooldown || true)
        {
            cooldownTimer = 0;
            isSkill = true;
            timer = 0;
            transform.parent.GetComponentInParent<Horde>().snowBallOnOff(true);
            Debug.Log("snowball on");
        }
    }
}
