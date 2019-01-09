using UnityEngine;

public class PlayerHUD : MonoBehaviour {
    private GameObject horde;
    [Space(1)]
    public GameObject archerHorde;
    public GameObject vikingHorde;
    public GameObject zombieHorde;
    public GameObject spartanHorde;
    public bool hasNoHud;
    public GameObject archerHUD;
    public GameObject vikingHUD;
    public GameObject mexican;
    public GameObject snowball;
    public GameObject mage;
    private GameObject activeHUD;
    private string deviceName;
	
	public void Start () {
        if(PlayerPrefs.HasKey(name + "minion"))
        {
            GameObject actualChief = mexican;
            switch(PlayerPrefs.GetString(name + "chief"))
            {
                case "mexican":
                    actualChief = mexican;
                    break;
                case "snowball":
                    actualChief = snowball;
                    break;
                case "mage":
                    actualChief = mage;
                    break;
            }
            Debug.Log(PlayerPrefs.GetString(name + "minion"));
            switch(PlayerPrefs.GetString(name + "minion"))
            {
                case "archers":
                    horde.GetComponent<Horde>().hordeChief = actualChief;
                    horde = Instantiate(archerHorde) as GameObject;
                    if(!hasNoHud) activeHUD = Instantiate(archerHUD) as GameObject;
                    break;
                case "vikings":
                    horde.GetComponent<Horde>().hordeChief = actualChief;
                    horde = Instantiate(vikingHorde) as GameObject;
                    if (!hasNoHud) activeHUD = Instantiate(vikingHUD) as GameObject;
                    break;
                case "zombies":
                    horde.GetComponent<Horde>().hordeChief = actualChief;
                    horde = Instantiate(zombieHorde) as GameObject;
                    horde.name = "ZombieXbox";
                    if (!hasNoHud) activeHUD = Instantiate(vikingHUD) as GameObject;
                    break;
                case "spartans":
                    horde.GetComponent<Horde>().hordeChief = actualChief;
                    horde = Instantiate(spartanHorde) as GameObject;
                    if (!hasNoHud) activeHUD = Instantiate(vikingHUD) as GameObject;
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
        if (!hasNoHud) activeHUD.GetComponent<HUD>().SetHorde(horde.GetComponent<Horde>());
        if (!hasNoHud) activeHUD.transform.SetParent(gameObject.transform);
        if (!hasNoHud) if (gameObject.transform.position.x > 1) activeHUD.transform.position = gameObject.transform.position;
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
                if (!hasNoHud) activeHUD = Instantiate(archerHUD) as GameObject;
                break;
            case "vikings":
                horde = Instantiate(vikingHorde) as GameObject;
                if (!hasNoHud) activeHUD = Instantiate(vikingHUD) as GameObject;
                break;
            case "zombies":
                horde = Instantiate(zombieHorde) as GameObject;
                horde.name = "ZombieXbox";
                if (!hasNoHud) activeHUD = Instantiate(vikingHUD) as GameObject;
                break;
            case "spartans":
                horde = Instantiate(spartanHorde) as GameObject;
                if (!hasNoHud) activeHUD = Instantiate(vikingHUD) as GameObject;
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
        if (!hasNoHud) activeHUD.GetComponent<HUD>().SetHorde(horde.GetComponent<Horde>());
        if (!hasNoHud) activeHUD.transform.SetParent(gameObject.transform);
        if (!hasNoHud) if (gameObject.transform.position.x > 1) activeHUD.transform.position = gameObject.transform.position;
    }
}
