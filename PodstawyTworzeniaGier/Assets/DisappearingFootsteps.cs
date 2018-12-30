using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisappearingFootsteps : MonoBehaviour
{

    Sprite leftFootSprite = null;
    Sprite rightFootSprite = null;
    private float currentAddInterval;
    public float footLifetime;
    public float addIntervalTime;
    private bool isNextFootRight;
    public GameObject rightFoot;
    public GameObject leftFoot;


    // Use this for initialization
    void Start()
    {
        leftFootSprite = Resources.Load<Sprite>("Assets/Graphics/PrimitiveTestGraphics/left_foot.png");
        rightFootSprite = Resources.Load<Sprite>("Assets/Graphics/PrimitiveTestGraphics/right_foot.png");
        currentAddInterval = Random.Range(0, 100) * addIntervalTime / 100f;
        isNextFootRight = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentAddInterval >= addIntervalTime)
        {
            GameObject obj = null;
            if (isNextFootRight)
            {
                obj = Instantiate(rightFoot);
            }
            else
            {
                obj = Instantiate(leftFoot);
            }
            isNextFootRight = !isNextFootRight;
            obj.transform.position = gameObject.transform.position;
            obj.transform.rotation = gameObject.transform.rotation;
            Destroy(obj, footLifetime);
            currentAddInterval = 0;
        }       
        currentAddInterval += Time.deltaTime;
    }
}
