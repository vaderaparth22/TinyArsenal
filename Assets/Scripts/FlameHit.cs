using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameHit : MonoBehaviour
{
    public FlameThrower FlameThrower;

    public List<GameObject> TriggerList;
    public GameObject blastPref;

    private float timerT;
    public float attackTime;
    public int attackDamage;

    void Start()
    {
        GameManager.Instance.FlameHit = this;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            if(FlameThrower.isFlame)
            {
                if (!TriggerList.Contains(other.gameObject))
                {
                    TriggerList.Add(other.gameObject);
                }
            }
        }
    }

    private void OnTriggerExit(Collider enemy)
    {
        if (enemy.CompareTag("Enemy"))
        {
            if(FlameThrower.isFlame)
            {
                if (TriggerList.Contains(enemy.gameObject))
                {
                    TriggerList.Remove(enemy.gameObject);
                }
            }
        }
    }

    private void Update()
    {
        timerT += Time.unscaledDeltaTime;

        if(timerT >= attackTime && FlameThrower.isFlame)
        {
            Attack();
        }
    }

    private void Attack()
    {
        timerT = 0f;
        EnemyHealth enemyHealth;

        for (int i = 0; i < TriggerList.Count; i++)
        {
            enemyHealth = TriggerList[i].GetComponent<EnemyHealth>();
            enemyHealth.UpdateHealth(attackDamage, enemyHealth.transform.position, blastPref);
        }
    }
}
