using UnityEngine;

public class Spartan : MinionBase
{
    public float attackCooldown;
    public float nextAttackSpeedUp;
    public float damage;
    public GameObject spear;

    private bool hasAttacked;
    private float actualAttackCooldown;
    private float attackTimer;

    void Start()
    {
        Initialise();
        hasAttacked = false;
        actualAttackCooldown = attackCooldown;
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<MinionBase>())
        {
            if (collision.gameObject.GetComponent<MinionBase>().GetPlayerName() != GetPlayerName() && !hasAttacked)
            {
                collision.gameObject.GetComponent<MinionBase>().DealDamage(damage);
                hasAttacked = true;
                Debug.Log((Time.time - attackTimer) * Time.timeScale);
                Debug.Log(actualAttackCooldown);
                if (Time.time - attackTimer < actualAttackCooldown + 0.1)
                {
                    attackTimer = Time.time;
                    actualAttackCooldown *= nextAttackSpeedUp;
                    Invoke("ResetCooldown", actualAttackCooldown);
                    gameObject.GetComponentInChildren<Spear>().duration = actualAttackCooldown;
                }
                else
                {
                    attackTimer = Time.time;
                    actualAttackCooldown = attackCooldown;
                    Invoke("ResetCooldown", actualAttackCooldown);
                    gameObject.GetComponentInChildren<Spear>().duration = actualAttackCooldown;
                }
            }
        }
    }

    private void ResetCooldown()
    {
        hasAttacked = false;
    }
}
