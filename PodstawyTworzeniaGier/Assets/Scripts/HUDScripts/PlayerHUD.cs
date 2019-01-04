using UnityEngine;

public class PlayerHUD : MonoBehaviour {
    private GameObject horde;
    [Space(1)]
    public GameObject archerHorde;
    public GameObject vikingHorde;
    public GameObject zombieHorde;
    public GameObject spartanHorde;
    public GameObject archerHUD;
    public GameObject vikingHUD;
    private GameObject activeHUD;
    private string deviceName;
	
	public void Start () {
        if(PlayerPrefs.HasKey(name))
        {
            Debug.Log(PlayerPrefs.GetString(name));
            switch(PlayerPrefs.GetString(name))
            {
                case "archers":
                    horde = Instantiate(archerHorde) as GameObject;
                    activeHUD = Instantiate(archerHUD) as GameObject;
                    break;
                case "vikings":
                    horde = Instantiate(vikingHorde) as GameObject;
                    activeHUD = Instantiate(vikingHUD) as GameObject;
                    break;
                case "zombies":
                    horde = Instantiate(zombieHorde) as GameObject;
                    horde.name = "ZombieXbox";
                    activeHUD = Instantiate(vikingHUD) as GameObject;
                    break;
                case "spartans":
                    horde = Instantiate(spartanHorde) as GameObject;
                    activeHUD = Instantiate(vikingHUD) as GameObject;
                    break;
            }
        }
        if(PlayerPrefs.HasKey(name + "device"))
        {
            deviceName = PlayerPrefs.GetString(name + "device");
        }
        horde.GetComponent<Horde>().SetPlayerName(name);
        switch(name)
        {
            case "Player1":
                horde.gameObject.transform.position = new Vector2(-28, -30);
                break;
            case "Player2":
                horde.gameObject.transform.position = new Vector2(45, 40);
                break;
        }
        horde.GetComponent<Horde>().SetDeviceSignature(deviceName);
        activeHUD.GetComponent<HUD>().SetHorde(horde.GetComponent<Horde>());
        activeHUD.transform.SetParent(gameObject.transform);
        if (gameObject.transform.position.x > 1) activeHUD.transform.position = gameObject.transform.position;
    }

    public Vector2 GetHordeCenter()
    {
        return horde.GetComponent<Horde>().GetHordeCenter();
    }

    public GameObject GetHorde()
    {
        return horde;
    }

    public void ReloadHorde()
    {
        Destroy(horde);
        Destroy(activeHUD);
        switch (PlayerPrefs.GetString(name))
        {
            case "archers":
                horde = Instantiate(archerHorde) as GameObject;
                activeHUD = Instantiate(archerHUD) as GameObject;
                break;
            case "vikings":
                horde = Instantiate(vikingHorde) as GameObject;
                activeHUD = Instantiate(vikingHUD) as GameObject;
                break;
            case "zombies":
                horde = Instantiate(zombieHorde) as GameObject;
                horde.name = "ZombieXbox";
                activeHUD = Instantiate(vikingHUD) as GameObject;
                break;
            case "spartans":
                horde = Instantiate(spartanHorde) as GameObject;
                activeHUD = Instantiate(vikingHUD) as GameObject;
                break;
        }
        if (PlayerPrefs.HasKey(name + "device"))
        {
            deviceName = PlayerPrefs.GetString(name + "device");
        }
        horde.GetComponent<Horde>().SetPlayerName(name);
        switch (name)
        {
            case "Player1":
                horde.gameObject.transform.position = new Vector2(-28, -30);
                break;
            case "Player2":
                horde.gameObject.transform.position = new Vector2(45, 40);
                break;
        }
        horde.GetComponent<Horde>().SetDeviceSignature(deviceName);
        activeHUD.GetComponent<HUD>().SetHorde(horde.GetComponent<Horde>());
        activeHUD.transform.SetParent(gameObject.transform);
        if (gameObject.transform.position.x > 1) activeHUD.transform.position = gameObject.transform.position;
    }
}
