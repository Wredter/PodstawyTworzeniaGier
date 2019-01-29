using UnityEngine;
using UnityEngine.UI;

public class Description : MonoBehaviour
{
    public SelectionScript script;
    private Text text;
    private string chief;
    private string minion;

    void Start()
    {
        text = gameObject.GetComponent<Text>();
        text.text = ComposeText();
    }

    void FixedUpdate()
    {
        text.text = ComposeText();
    }

    string ComposeText()
    {
        SetNames();
        return chief + " " + minion;
    }

    void SetNames()
    {
        switch(script.selectedChief)
        {
            case 0:
                chief = "THE POPE was always the father of all people. His devotion to protection became almost legendary as he managed to develop the ultimate technique of covering his worshipers under his clothes. So The Great Gods Of Hordes couldn't resist to lavish him with the most devoted worshipers:";
                break;
            case 1:
                chief = "At this time MEXICANO was travelling through time and space looking for an ultimate artefact: the first tequila bottle ever created. Unfortunately a great goal requires great power and centuries of the search resulted only in a tremendous exhaustion. Finally The Great Gods Of Hordes showed their mercy. Mexico took over the control of his new unique horde:";
                break;
            case 2:
                chief = "HEEEERE'S VAPER! #VapeNation #GoGreen His raps hotter than sun, his vape rips clouds thicc as fog.  Fighting for freedom in internet, while sitting in front of computer in his basement. Rapping about how hard life was when he was a kid. Checkout his next collab with his new homies:";
                break;
            case 3:
                chief = "THE MAGE - The real incarnation of flames known from the legends of Abadyn Dag Yudun. When he learned how to manipulate fire, burn projectiles and deal damage, The Great Gods Of Hordes couldn't wait much longer. Now there is very little enemies can against do with his new horde of";
                break;
        }
        switch (script.selectedMinion)
        {
            case 0:
                minion = "VIKINGS, barbarians from the north. Their raw strength and incredible resistance to pain makes them great warriors. Making their way into battle with quick charge, they are able to cause massive damage by throwing axes at enemy.";
                break;
            case 1:
                minion = "ARCHERS, high elves from wilderness. Wisdom and knowledge is not the only weapon of archer, long years of training and defending forests from intruders gave archers experience to master archery. They are also able to dodge attack with quick regrouping.";
                break;
            case 2:
                minion = "SPARTANS, brave warriors from Sparta. Years of harsh training and countless battles makes Spartans one of the greatest army unit that ever existed. Their vicious attacks each one faster than previous one deals incredible amount of damage to enemy. Charging into battle Spartan in a real threat.";
                break;
            case 3:
                minion = "ZOMBIES, brrglgghgh brgghlg. Brghfhhgl harms hgggrgwwrgg hg wrr, ghhrwwrrhg infects gwwrhghl and also ghhw gh. Ghwrlgh ghghlrlg wwrhgh charge wrghhlgh.";
                break;
        }
    }
}
