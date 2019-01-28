using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SantaScript : MonoBehaviour {
    IController controller;
    public GameObject peleryna;
    public float cooldown = 8;
    public float skillTime = 4;
    float timer = 0;
    float cooldownTimer = 0;
    bool isSkill = false;
	void Start () {
        peleryna.transform.localScale = new Vector3(0.5f, 0.5f, 1);
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

        if (controller.Special2())
        {
            Debug.Log("snowball");
            skill();
            
        }

        if (isSkill)
        {
            scl = 0.5f - (timer - skillTime) * (timer) / skillTime / skillTime *8f;
            peleryna.transform.localScale = new Vector3(scl, scl, 1);
        }
        else
        {
            peleryna.transform.localScale = new Vector3(0.5f, 0.5f, 1);
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
    public void SetController(IController controller)
    {
        this.controller = controller;
    }
}
