using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Viking : MinionBase
{
    private VikingHordeHandler VikingHordeHandler;

    public GameObject projectile;
    public int cooldown;

    private Dictionary<GameObject, Axe> axes;
    private Horde horde;
    private int projectilesCount;
    private bool hasShot;
    private int counter;
    [Range(0f, 1f)]
    public float volumeMin;
    [Range(0f, 1f)]
    public float volumeMax;
    public List<AudioClip> skillSounds;
    private AudioSource skillSoundSource;

    // Use this for initialization
    void Start()
    {
        VikingHordeHandler = transform.parent.gameObject.GetComponent<VikingHordeHandler>();
        skillSoundSource = GetComponent<AudioSource>();
        counter = 0;
        Initialise();
        axes = new Dictionary<GameObject, Axe>();
        projectilesCount = 0;
        hasShot = false;
    }

    private new void FixedUpdate()
    {
        base.FixedUpdate();
        List<GameObject> toRemove = new List<GameObject>();
        foreach (Axe g in axes.Values)
        {
            g.UpdateCounter();
        }
        foreach (GameObject g in axes.Keys)
        {
            if(g == null)
            {
                toRemove.Add(g);
            } else
            if(g.GetComponent<Axe>().GetCounter() < 0)
            {
                toRemove.Add(g);
                VikingHordeHandler.AddToThrown(g);            
            }
        }
        foreach(GameObject g in toRemove)
        {
            axes.Remove(g);
        }
        counter--;
    }

    void Update()
    {
        if (controller.Shoot())
        {
            Debug.Log("topur pruba");
            if (VikingHordeHandler.CanRemoveAxe() && !hasShot && counter <= 0)
            {
                Debug.Log("topur rzut");
                GameObject temp = (Instantiate(projectile, transform.position, transform.rotation));
                axes.Add(temp, temp.GetComponent<Axe>());
                axes[temp].SetPlayerName(playerName);
                axes[temp].Initialise("axe" + projectilesCount, this, controller);
                skillSoundSource.PlayOneShot(skillSounds[0], Random.Range(volumeMin, volumeMax));
                projectilesCount++;
                VikingHordeHandler.RemoveAxe();
                hasShot = true;
                counter = cooldown;
            }
        }
        if(!controller.Shoot() && hasShot)
        {
            hasShot = false;
        }
    }

    public new void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
        if(collision.gameObject.GetComponent<Axe>())
        {
            if(collision.gameObject.GetComponent<Axe>().GetPlayerName().Equals(playerName) 
                && collision.gameObject.GetComponent<Axe>().GetCounter() < 0)
            {
                VikingHordeHandler.AddAxe();
                //((Viking)(collision.gameObject.GetComponent<Projectile>().GetPlayer())).ReturnProjectile(collision.gameObject);
                Destroy(collision.gameObject);
                return;
            }
        }
        //Return to avaiable axes if player tag matches
    }

    public void ReturnProjectile(GameObject p)
    {
        axes.Remove(p);
    }
}
