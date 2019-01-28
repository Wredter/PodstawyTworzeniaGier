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
    [Range(0f, 1f)]
    public float volume;
    public List<AudioClip> skillSounds;
    private AudioSource skillSoundSource;

    //Pola potrzebne do kaktusa
    public Transform cactus;
    public float CactusCooldown; //czas blokady po spawnie kaktusa
    public float CactusSpawnDelay; //opóźnienie pojawienia się kaktusa (umożliwia odsunięcie się z miejsca w którym powstanie)
    private bool canSpawnCactus;
    IController controller;

    // Use this for initialization
    void Start ()
    {
        moveX = 0;
        moveY = 0;
        canSpawnCactus = true;
        skillSoundSource = GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update ()
	{
	    moveX = 0;
	    moveY = 0;
        //HandleMovement();
        HandleCactusDrop(KeyCode.K);
        //Debug.Log("KAKTUSSSSSSSS UPDATE");
        if(controller.Special2() && canSpawnCactus)
        {
            Debug.Log("KAKTUSSSSSSSS SPAWN!");
            skillSoundSource.PlayOneShot(skillSounds[Random.Range(0, skillSounds.Count)], volume);
            Vector3 chiefPosition = gameObject.GetComponent<Transform>().position;
            StartCoroutine(CactusSpawnCoroutine(CactusSpawnDelay, chiefPosition));
        }
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

    public void SetController(IController controller)
    {
        this.controller = controller;
    }
}
