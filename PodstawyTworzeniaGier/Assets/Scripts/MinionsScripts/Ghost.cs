using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : MonoBehaviour
{
    private float move = 30f;
    private Vector3 tmp;
    public GameObject Bones;
    // Use this for initialization
    void Start()
    {
        tmp = gameObject.transform.position;
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
            GetComponent<Rigidbody2D>().AddForce(new Vector2(move, 5), ForceMode2D.Impulse);
            move = -1.3f * move;
            GetComponent<SpriteRenderer>().color = color;
            yield return new WaitForSeconds(0.2f);

        }

        Instantiate(Bones, tmp, new Quaternion(0, 0, 0, 0));
        Destroy(gameObject);

    }

    public void StartFading()
    {
        StartCoroutine(FadeOut());
    }
}
