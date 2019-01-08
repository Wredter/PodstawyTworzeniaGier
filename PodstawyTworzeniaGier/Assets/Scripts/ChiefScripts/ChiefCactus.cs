using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.Experimental.UIElements;

public class ChiefCactus : MonoBehaviour
{
    float moveX;
    float moveY;
    public float maxSpeed;

    //Pola potrzebne do kaktusa
    public Transform cactus;
    public float CactusCooldown; //czas blokady po spawnie kaktusa
    public float CactusSpawnDelay; //opóźnienie pojawienia się kaktusa (umożliwia odsunięcie się z miejsca w którym powstanie)
    private bool canSpawnCactus;

    // Use this for initialization
    void Start ()
    {
        moveX = 0;
        moveY = 0;
        maxSpeed = 10;
        CactusCooldown = 5f;
        CactusSpawnDelay = 2f;
        canSpawnCactus = true;

    }
	
	// Update is called once per frame
	void Update ()
	{
	    moveX = 0;
	    moveY = 0;
        HandleMovement();
        HandleCactusDrop(KeyCode.K);
        Debug.Log("KAKTUSSSSSSSS UPDATE");
    }

    void HandleMovement()
    {
        bool a, d, w, s;
        a = Input.GetKey(KeyCode.A);
        d = Input.GetKey(KeyCode.D);
        s = Input.GetKey(KeyCode.S);
        w = Input.GetKey(KeyCode.W);

        if (a || d || s || w)
        {
            moveX = 0;
            moveY = 0;
            if (a) moveX -= 1;
            if (d) moveX += 1;
            if (s) moveY -= 1;
            if (w) moveY += 1;
        }

        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(moveX * maxSpeed, moveY * maxSpeed);
        HandleCactusDrop(KeyCode.Space);
    }

    void HandleCactusDrop(KeyCode key)
    {
        bool button = Input.GetKeyDown(key);

        if (button && canSpawnCactus)
        {
            Debug.Log("KAKTUSSSSSSSS SPAWN!");
            Vector3 chiefPosition = gameObject.GetComponent<Transform>().position;
            StartCoroutine(CactusSpawnCoroutine(2f, chiefPosition));
        }
    }

    //blokuje postawienie nowego kaktusa
    public IEnumerator CactusAfterSpawnCoroutine(float time)
    {
        yield return new WaitForSeconds(time);
        canSpawnCactus = true;
    }

    //spawn kaktusa z opoznieniem
    public IEnumerator CactusSpawnCoroutine(float time, Vector3 chiefPosition)
    {
        StartCoroutine(CactusAfterSpawnCoroutine(CactusCooldown));
        canSpawnCactus = false;
        yield return new WaitForSeconds(time);
        Transform cactusInstance;
        cactusInstance = Instantiate(cactus);
        cactusInstance.GetComponent<Transform>().position = new Vector3(chiefPosition.x, chiefPosition.y);
        
    }
}
