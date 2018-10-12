using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {
 
    public GameObject gameObjectType;
    public int cooldown;
    
    protected PlayerTest player;
    protected Vector2 startingPosition;
    protected Vector2 startingVelocity;

	// Use this for initialization
	void Start () {
        
	}

    public void Initialise(string projectileID, PlayerTest player, Vector2 input)
    {
        name = projectileID;
        this.player = player;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void UpdateCounter()
    {
        cooldown++;
    }

    public int GetCounter()
    {
        return cooldown;
    }

    public string GetProjectileID()
    {
        return name;
    }
}
