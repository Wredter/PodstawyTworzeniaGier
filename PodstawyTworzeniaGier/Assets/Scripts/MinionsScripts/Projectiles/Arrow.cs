﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.UIElements;

public class Arrow : Projectile
{
    private int step;
    private Vector2 initialVelocity;
    [Range(0f, 1f)]
    public float volumeMin;
    [Range(0f, 1f)]
    public float volumeMax;
    public List<AudioClip> skillSounds;
    private AudioSource skillSoundSource;

    void Start()
    {
        step = range + 1;
        isReturnable = false;
        skillSoundSource = GetComponent<AudioSource>();

    }

    public void Initialise(string arrowID, Archer player, float power)
    {
        name = arrowID;
        this.player = player;
        rb2d = GetComponent<Rigidbody2D>();
        
        float projectileX = player.GetChief().GetComponent<Chief>().GetPrevious().x;
        float projectileY = player.GetChief().GetComponent<Chief>().GetPrevious().y;
        Vector2 projectileThrow = new Vector2(projectileX, projectileY);
        Vector2 pom = new Vector2(player.GetChief().GetComponent<Chief>().GetController().LookHorizontal(), player.GetChief().GetComponent<Chief>().GetController().LookVertical());
        var rad = Mathf.Atan2(pom.y, pom.x);
        rb2d.rotation = rad * Mathf.Rad2Deg - 90;
        projectileThrow += new Vector2(randomSpread * 2 * (Random.value - 0.5f), randomSpread * 2 * (Random.value - 0.5f));
        rb2d.AddForce(projectileThrow * 500 * power);
    }

    private void FixedUpdate()
    {
        if (step == range)
        {
            initialVelocity = rb2d.velocity;
        }
        if (step > 0 && step <= range)
        {
            rb2d.velocity = initialVelocity * (4 + step / range) / 4;
        }
        if (step == 0)
        {
            rb2d.velocity = new Vector2(0, 0);
            ((Archer)player).ReturnProjectile(gameObject);
            Destroy(gameObject);
        }
        step -= 1;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(player == null)
        {
            //Debug.Log("Wut");
            Destroy(gameObject);
            return;
        }
        if (!collision.gameObject.GetComponent<MinionBase>() && !collision.gameObject.GetComponent<ChiefBase>() && collision.tag != "Mud")
        {
            //skillSoundSource.PlayOneShot(skillSounds[0], Random.Range(volumeMin, volumeMax));
            ((Archer)player).ReturnProjectile(gameObject);
            //gameObject.GetComponent<Renderer>().enabled = false;
            if(collision.tag != "PointsAddArea")
            {
                Destroy(gameObject);
            }            
        }
    }
}
