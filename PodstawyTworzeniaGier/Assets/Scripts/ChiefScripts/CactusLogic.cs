using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class CactusLogic : MonoBehaviour
{
    public float ShakeSpeed; //how fast it shakes
    public float ShakesAmount; //how much it shakes
    public float GrowthTime; //seconds
    public float CactusLifetime; //seconds

    private float timer;
    [Header("AudioOpts")]
    [Range(0f, 1f)]
    public float volumeMin;
    [Range(0f, 1f)]
    public float volumeMax;
    [Range(0f, 1f)]
    public float volumeGrowth;
    public List<AudioClip> skillSounds;
    private AudioSource skillSoundSource;

    // Use this for initialization
    void Start()
    {
        skillSoundSource = GetComponent<AudioSource>();
        ShakeSpeed = 10.0f;
        ShakesAmount = .08f;
        GrowthTime = 6f;
        CactusLifetime = 10f;
        timer = 0;
        skillSoundSource.PlayOneShot(skillSounds[0],Random.Range(volumeMin,volumeMax));
    }

    // Update is called once per frame
    void Update()
    {
        if(timer >= CactusLifetime)
            Destroy(gameObject);

        timer += Time.deltaTime;
        var growthFraction = timer / GrowthTime;

        if (timer < GrowthTime)
        {
            //skillSoundSource.loop = true;
            //skillSoundSource.pitch = 1f;
            //skillSoundSource.PlayOneShot(skillSounds[1],volumeGrowth);
            gameObject.GetComponent<Transform>().localScale = new Vector2(growthFraction, growthFraction);
            var oldPosition = gameObject.transform.position;
            gameObject.transform.position = new Vector2(oldPosition.x + (Mathf.Sin(Time.time * ShakeSpeed) * ShakesAmount ), oldPosition.y);
            ShakesAmount -= 0.0002f;
        }
        //else
        //{
        //    skillSoundSource.loop = false;
        //}
    }
}
