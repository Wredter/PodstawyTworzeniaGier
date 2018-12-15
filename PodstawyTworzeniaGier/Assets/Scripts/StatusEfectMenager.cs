using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusEfectMenager : MonoBehaviour {
    MinionBase minionBaseXbox;
    public List<int> poisonDutationTimers = new List<int>();
	// Use this for initialization
	void Start () {
        minionBaseXbox = GetComponent<MinionBase>();
	}
    public void ApplyPoison(int poisonNumberOfTicks,int poisonDamage, float timeBetweenTicks)
    {
        if(poisonDutationTimers.Count <= 0)
        {
            poisonDutationTimers.Add(poisonNumberOfTicks);
            StartCoroutine(PoisonDoT(poisonDamage,timeBetweenTicks));
        }
        else
        {
            poisonDutationTimers.Add(poisonNumberOfTicks);
        }
    }
    IEnumerator PoisonDoT(int poisonDamage, float timebetweenTicks)
    {
        while(poisonDutationTimers.Count > 0)
        {
            for(int i = 0; i < poisonDutationTimers.Count; i++)
            {
                poisonDutationTimers[i] -= 1;
            }
            minionBaseXbox.DealDamage(poisonDamage);
            poisonDutationTimers.RemoveAll(i => i == 0);
            yield return new WaitForSeconds(timebetweenTicks);
        }
    }
}
