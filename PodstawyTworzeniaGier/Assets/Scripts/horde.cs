using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class horde : MonoBehaviour {
    public GameObject hordeChief;
    public GameObject hordeMinion;
    public int minionsNumber = 10;
    public float spawnRadius = 5;
    public float minionsKS = 1;
    public float minionsK = 1;

    List<GameObject> minions;
    List<GameObject> minionsWithChief;
    GameObject chief;

	// Use this for initialization
	void Start () {
        minions = new List<GameObject>();
        minionsWithChief = new List<GameObject>();

        GameObject obj = Instantiate(hordeChief, new Vector3(0, 0, 0), Quaternion.identity);
        obj.transform.parent = GameObject.Find("Horde").transform;
        chief = obj;
        minionsWithChief.Add(chief);

        for (int i = 0; i < minionsNumber; i++)
        {
            float radius = Mathf.PI * 2 / minionsNumber * i;
            obj = Instantiate(hordeMinion, new Vector3(spawnRadius*Mathf.Cos(radius) + Random.value, spawnRadius * Mathf.Sin(radius) + Random.value, 0), Quaternion.identity);
            obj.transform.parent = GameObject.Find("Horde").transform;
            minions.Add(obj);
            minionsWithChief.Add(obj);
        }

	}
	
	// Update is called once per frame
	void Update () {
        Vector2 force = new Vector2();
        float dx, dy, r2, r;
        foreach (GameObject obj in minions)
        {
            dx = chief.transform.position.x - obj.transform.position.x;
            dy = chief.transform.position.y - obj.transform.position.y;
            r2 = dx * dx + dy * dy;
            r = Mathf.Sqrt(r2);
            force.Set(dx * minionsKS, dy * minionsKS);
            obj.GetComponent<Rigidbody2D>().AddForce(force);
        }

        foreach (GameObject obj in minions)
        {
            foreach (GameObject obj2 in minionsWithChief)
            {
                if (obj != obj2)
                {
                    dx = obj2.transform.position.x - obj.transform.position.x;
                    dy = obj2.transform.position.y - obj.transform.position.y;
                    r2 = dx * dx + dy * dy;
                    r = Mathf.Sqrt(r2);
                    force.Set(-dx/r * minionsK / r2, -dy/r * minionsK / r2);
                    obj.GetComponent<Rigidbody2D>().AddForce(force);
                }
            }
                
        }
        chief.GetComponent<Rigidbody2D>().AddForce(new Vector2(1, 0));

    }
}
