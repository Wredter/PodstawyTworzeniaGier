using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bone : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        StartCoroutine(DestroyBones());
    }

    // Update is called once per frame
    void Update()
    {

    }

    public IEnumerator DestroyBones()
    {
        yield return new WaitForSeconds(10f);
        Destroy(gameObject);
    }

}
