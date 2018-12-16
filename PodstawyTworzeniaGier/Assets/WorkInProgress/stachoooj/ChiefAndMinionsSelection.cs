using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChiefAndMinionsSelection : MonoBehaviour
{
    #region public
    public string Player1Controller;
    public string Player2Controller;
    #endregion

    private Image chief0;
    private Image chief1;
    private Image chief2;
    private Image minion0;
    private Image minion1;
    private Image minion2;
    private Image minion3;

    private Image p1cs;
    private Image p1csh;
    private Image p2cs;
    private Image p2csh;

    private Image p1ms;
    private Image p1msh;
    private Image p2ms;
    private Image p2msh;

    #region Controller
    private IController controller1;
    private IController controller2;
    private Dictionary<string, bool> checkpoints;
    private Dictionary<string, int> selections;
    #endregion

    void Start()
    {
        chief0 = GameObject.Find("Chief0").GetComponent<Image>();
        chief1 = GameObject.Find("Chief1").GetComponent<Image>();
        chief2 = GameObject.Find("Chief2").GetComponent<Image>();
        minion0 = GameObject.Find("Minion0").GetComponent<Image>();
        minion1 = GameObject.Find("Minion1").GetComponent<Image>();
        minion2 = GameObject.Find("Minion2").GetComponent<Image>();
        minion3 = GameObject.Find("Minion3").GetComponent<Image>();

        p1cs = GameObject.Find("Player1ChiefSelector").GetComponent<Image>();
        p1csh = GameObject.Find("Player1HalfChiefSelector").GetComponent<Image>();
        p2cs = GameObject.Find("Player2ChiefSelector").GetComponent<Image>();
        p2csh = GameObject.Find("Player2HalfChiefSelector").GetComponent<Image>();

        p1ms = GameObject.Find("Player1MinionSelector").GetComponent<Image>();
        p1msh = GameObject.Find("Player1HalfMinionSelector").GetComponent<Image>();
        p2ms = GameObject.Find("Player2MinionSelector").GetComponent<Image>();
        p2msh = GameObject.Find("Player2HalfMinionSelector").GetComponent<Image>();

        PlayerPrefs.SetString("Player1Device", Player1Controller);
        PlayerPrefs.SetString("Player2Device", Player2Controller);
        controller1 = GetController(Player1Controller);
        controller1.SetDeviceSignature(Player1Controller);
        controller2 = GetController(Player2Controller);
        controller2.SetDeviceSignature(Player2Controller);

        checkpoints = new Dictionary<string, bool>
        {
            { "Player1C", true },
            { "Player2C", true },
            { "Player1M", false },
            { "Player2M", false },
            { "Player1R", false },
            { "Player2R", false },
            { "Player1T", false },
            { "Player2T", false },
            { "Player1N", false },
            { "Player2N", false },
            { "Player1P", false },
            { "Player2P", false }
        };

        selections = new Dictionary<string, int>
        {
            { "Player1C", 0 },
            { "Player2C", 0 },
            { "Player1M", 0 },
            { "Player2M", 0 }
        };

        p1cs.transform.position = chief0.transform.position;
        p1csh.transform.position = chief0.transform.position;
        p2cs.transform.position = chief0.transform.position;
        p2csh.transform.position = chief0.transform.position;

        p1ms.transform.position = minion0.transform.position;
        p1msh.transform.position = minion0.transform.position;
        p2ms.transform.position = minion0.transform.position;
        p2msh.transform.position = minion0.transform.position;
    }

    void Update()
    {
        CheckController(controller1, "Player1");
        CheckController(controller2, "Player2");
        if (checkpoints["Player1R"] && checkpoints["Player2R"])
        {
            SceneManager.LoadScene("stachuj");
        }
    }

    private void CheckController(IController controller, string player)
    {
        if (checkpoints[player + "R"])
        {
            if (controller.Back() && checkpoints[player + "P"])
            {
                checkpoints[player + "R"] = false;
                checkpoints[player + "P"] = false;
            }
            if (!controller.Back()) checkpoints[player + "P"] = true;
        }
        else if (checkpoints[player + "M"])
        {
            if (Mathf.Abs(controller.MoveHorizontal()) > 0.5 && !checkpoints[player + "T"])
            {
                Debug.Log("oko");
                ChangeMinion(controller, player);
                checkpoints[player + "T"] = true;
            }
            else if (Mathf.Abs(controller.MoveHorizontal()) <= 0.5) checkpoints[player + "T"] = false;
            if (controller.Select() && checkpoints[player + "N"])
            {
                checkpoints[player + "R"] = true;
                checkpoints[player + "N"] = true;
            }
            if (!controller.Select()) checkpoints[player + "N"] = false;
            if (controller.Back() && checkpoints[player + "P"])
            {
                checkpoints[player + "M"] = false;
                checkpoints[player + "P"] = false;
            }
            if (!controller.Back()) checkpoints[player + "P"] = true;
        }
        else if (checkpoints[player + "C"])
        {
            if (Mathf.Abs(controller.MoveHorizontal()) > 0.5 && !checkpoints[player + "T"])
            {
                ChangeChief(controller, player);
                checkpoints[player + "T"] = true;
            }
            else if (Mathf.Abs(controller.MoveHorizontal()) <= 0.5) checkpoints[player + "T"] = false;
            if (controller.Select() && checkpoints[player + "N"])
            {
                checkpoints[player + "M"] = true;
                checkpoints[player + "N"] = false;
            }
            if (!controller.Select()) checkpoints[player + "N"] = true;
        }
    }

    private void ChangeChief(IController controller, string player)
    {
        int pom = selections[player + "C"];
        if (controller.MoveHorizontal() > 0)
        {
            if (++pom == 3)
            {
                pom = 0;
            }
        }
        else
        {
            if (--pom == -1)
            {
                pom = 2;
            }
        }
        selections[player + "C"] = pom;
        Vector2 pomPosition = new Vector2();
        switch (pom)
        {
            case 0:
                pomPosition = chief0.transform.position;
                break;
            case 1:
                pomPosition = chief1.transform.position;
                break;
            case 2:
                pomPosition = chief2.transform.position;
                break;
        }
        if (player == "Player1")
        {
            p1cs.transform.position = pomPosition;
            p1csh.transform.position = pomPosition;
        }
        else
        {
            p2cs.transform.position = pomPosition;
            p2csh.transform.position = pomPosition;
        }
    }

    private void ChangeMinion(IController controller, string player)
    {
        int pom = selections[player + "M"];
        if (controller.MoveHorizontal() > 0)
        {
            if (++pom == 4)
            {
                pom = 0;
            }
        }
        else
        {
            if (--pom == -1)
            {
                pom = 3;
            }
        }
        selections[player + "M"] = pom;
        Vector2 pomPosition = new Vector2();
        switch (pom)
        {
            case 0:
                pomPosition = minion0.transform.position;
                break;
            case 1:
                pomPosition = minion1.transform.position;
                break;
            case 2:
                pomPosition = minion2.transform.position;
                break;
            case 3:
                pomPosition = minion3.transform.position;
                break;
        }
        if (player == "Player1")
        {
            p1ms.transform.position = pomPosition;
            p1msh.transform.position = pomPosition;
        }
        else
        {
            p2ms.transform.position = pomPosition;
            p2msh.transform.position = pomPosition;
        }
    }

    private IController GetController(string deviceSignature)
    {
        switch (deviceSignature)
        {
            case "":
                return new ControllerMouseAndKeyboard();
            default:
                return new ControllerXbox();
        }
    }
}
