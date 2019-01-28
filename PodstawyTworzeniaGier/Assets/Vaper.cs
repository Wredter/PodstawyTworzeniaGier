using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vaper : MonoBehaviour
{
    IController controller;
    [Range(0f, 1f)]
    public float volume;
    public List<AudioClip> skillSounds;
    private AudioSource skillSoundSource;
    public void SetController(IController controller)
    {
        this.controller = controller;
    }

    public GameObject chmurka;
    // Use this for initialization
    void Start()
    {
        VapTimer = VapTime;
        skillSoundSource = GetComponent<AudioSource>();
    }
    public int vapIStart = 25;
    public float VapTime = 5;
    public float VapFreq = 5;
    private float VapTimer;
    private float vapDelta;
    private int vapI;

    void Update()
    {
        VapTimer += Time.deltaTime;

        if (vapI > 1)
        {
            if (vapI > vapIStart - VapTimer*VapFreq)
            {
                vapI--;
                Instantiate(chmurka, new Vector3(transform.position.x + Random.Range(-1, 1), transform.position.y + Random.Range(-1, 1), transform.position.z), transform.rotation);
            }
        }


        //controller.Special2() || 
        if (controller.Special2())
        {
            Debug.Log("vaper ");
            
            Vap();
            skillSoundSource.PlayOneShot(skillSounds[Random.Range(0, skillSounds.Count)], volume);
        }
    }

    public void Vap()
    {
        if (VapTimer > VapTime)
        {
            Debug.Log("vaper skkill");
            vapI = vapIStart;
            VapTimer = 0;
        }

    }
}
