using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusEfectMenager : MonoBehaviour {
    MinionBaseXbox minionBaseXbox;
    public List<int> poisonDutationTimers = new List<int>();
	// Use this for initialization
	void Start () {
        minionBaseXbox = GetComponent<MinionBaseXbox>();
	}
    public void ApplyPoison(int poisonNumberOfTicks,int poisonDamage)
    {
        if(poisonDutationTimers.Count <= 0)
        {
            poisonDutationTimers.Add(poisonNumberOfTicks);
            StartCoroutine(PoisonDoT(poisonDamage));
        }
        else
        {
            poisonDutationTimers.Add(poisonNumberOfTicks);
        }
    }
    IEnumerator PoisonDoT(int poisonDamage)
    {
        while(poisonDutationTimers.Count > 0)
        {
            for(int i = 0; i < poisonDutationTimers.Count; i++)
            {
                poisonDutationTimers[i] -= 1;
            }
            minionBaseXbox.DealDamage(poisonDamage);
            poisonDutationTimers.RemoveAll(i => i == 0);
            yield return new WaitForSeconds(0.75f);
        }
    }
}
