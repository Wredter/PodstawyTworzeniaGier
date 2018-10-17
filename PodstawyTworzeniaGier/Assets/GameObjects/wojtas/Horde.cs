using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Horde : NetworkBehaviour
{
    public int maxSpeed = 10;
    public float spawnRadius = 10;
    public float minionsKS = 100;
    public float minionsK = 400;
    public int minionsNumber = 5;
    public GameObject minionPrefab;
    public GameObject chiefPrefab;
    public GameObject test;

    private GameObject t;
    private NetworkIdentity nId;
    private bool isSpawned;
    private GameObject chief;
    private List<GameObject> minions, minionsWithChief;
    void Start()
    {
        isSpawned = false;
        if (isLocalPlayer)
        {
            //CmdInstantiate();
            CmdTest();
            Debug.Log("spawnTest");
        }
        Debug.Log("ilp: " + isLocalPlayer);
    }
    void Update()
    {
        //Debug.Log("update ilp: " + isLocalPlayer);
        if (isLocalPlayer && isSpawned)
        {
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
                        force.Set(-dx / r * minionsK / r2, -dy / r * minionsK / r2);
                        obj.GetComponent<Rigidbody2D>().AddForce(force);
                    }
                }

            }

            float moveX = Input.GetAxis("Horizontal");
            float moveY = Input.GetAxis("Vertical");
            chief.GetComponent<Rigidbody2D>().velocity = new Vector2(moveX * maxSpeed, moveY * maxSpeed);

            //chief.transform.Translate(Vector2.right * 10 * Time.deltaTime); 
        }
        if (isLocalPlayer && !isSpawned)
        {
            if (Input.GetKey(KeyCode.P))
            {
                CmdSpawn();
                isSpawned = true;
            }
        }

        //test
        if (isLocalPlayer)
        {
            if (Input.GetKey(KeyCode.L))
            {
                t.transform.Translate(Vector2.right * 3f * Time.deltaTime);
            }
        }
    }

    [Command]
    public void CmdTest()
    {
        t = Instantiate(test, transform.position, Quaternion.identity);
        //NetworkServer.SpawnWithClientAuthority(t, connectionToClient);
    }

    [Command]
    public void CmdInstantiate()
    {
        minions = new List<GameObject>();
        minionsWithChief = new List<GameObject>();

        chief = Instantiate(chiefPrefab, transform.position, Quaternion.identity) as GameObject;

        minionsWithChief.Add(chief);

        for (int i = 0; i < minionsNumber; i++)
        {
            float radius = Mathf.PI * 2 / minionsNumber * i;
            GameObject minion = Instantiate(minionPrefab, 
                new Vector3(transform.position.x + spawnRadius * Mathf.Cos(radius) + Random.value, 
                transform.position.y + spawnRadius * Mathf.Sin(radius) + Random.value, 0), Quaternion.identity) as GameObject;

            minions.Add(minion);
            minionsWithChief.Add(minion);
        }
    }

    [Command]
    public void CmdSpawn()
    {
        NetworkServer.SpawnWithClientAuthority(chief, connectionToClient);
        
        for (int i = 0; i < minionsNumber; i++)
        {
            GameObject minion = minions[i];
            NetworkServer.SpawnWithClientAuthority(minion, connectionToClient);
        }
    }
}
