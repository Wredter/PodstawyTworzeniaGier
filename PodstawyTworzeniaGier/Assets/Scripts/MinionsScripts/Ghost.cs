using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : MonoBehaviour
{
    private int timer;

    public GameObject Bones;
    // Use this for initialization
    void Start()
    {
        StartFading();

    }

    // Update is called once per frame
    void Update()
    {



    }

    public IEnumerator FadeOut()
    {
        for (int i = 0; i < 10; i++)
        {
            var color = GetComponent<SpriteRenderer>().color;
            color.a -= 0.1f;

            var scale = GetComponent<Transform>().localScale;
            GetComponent<Transform>().localScale = new Vector3(scale.x * 0.9f, scale.y * 0.9f, scale.z * 0.9f);
            GetComponent<SpriteRenderer>().color = color;
            yield return new WaitForSeconds(0.2f);

        }
        var tmp = gameObject.transform.position;
        Instantiate(Bones, tmp, new Quaternion(0, 0, 0, 0));
        Destroy(gameObject);
        
    }

    public void StartFading()
    {
        StartCoroutine(FadeOut());
    }
}
