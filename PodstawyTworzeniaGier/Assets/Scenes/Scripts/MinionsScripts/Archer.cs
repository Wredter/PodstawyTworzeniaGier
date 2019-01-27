using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Archer : MinionBase
{
    public GameObject projectile;
    public int shootCooldown;
    [Range(0f, 1f)]
    public float volume;
    public List<AudioClip> skillSounds;
    private AudioSource skillSoundSource;

    #region private fields
    private Dictionary<GameObject, Arrow> arrows;
    private int projectilesCount;
    private float power;
    private int cooldown;
    private bool charging;
    private static float maxPower = 3;
    #endregion

    void Start()
    {
        Initialise();
        arrows = new Dictionary<GameObject, Arrow>();
        skillSoundSource = GetComponent<AudioSource>();
        projectilesCount = 0;
        power = 0.5f;
    }

    private new void FixedUpdate()
    {
        base.FixedUpdate();
        foreach (Arrow g in arrows.Values)
        {
            g.UpdateCounter();
        }
        if (cooldown > 0)
        {
            cooldown--;
        }
        else
        {
            if (controller.Shoot() && power < maxPower)
            {
                power += 0.05f;
                charging = true;
            }
            if (!controller.Shoot() && charging)
            {
                GameObject temp = (Instantiate(projectile, transform.position, transform.rotation));
                arrows.Add(temp, temp.GetComponent<Arrow>());
                arrows[temp].SetPlayerName(playerName);
                arrows[temp].Initialise("arrow" + projectilesCount, this, power);
                skillSoundSource.PlayOneShot(skillSounds[0], volume);
                projectilesCount++;
                power = 0.5f;
                cooldown = shootCooldown;
                charging = false;

            }
        }
        if (power > maxPower)
        {
            power = maxPower;
        }
    }

    public new void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
        //Not really much more to do
    }

    public float GetCooldown()
    {
        return ((float)cooldown) / ((float)shootCooldown);
    }

    public float GetPower()
    {
        return power / maxPower;
    }

    public void ReturnProjectile(GameObject p)
    {
        arrows.Remove(p);
    }
}
