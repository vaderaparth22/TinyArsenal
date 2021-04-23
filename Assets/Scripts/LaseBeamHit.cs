using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaseBeamHit : MonoBehaviour
{
    public string tagToCheck = "laser";
    public int damage = 30;

    public ParticleSystem hitEffect;
    public GameObject blastPref;

    private EnemyHealth enemyHealth;

    void Start()
    {
        enemyHealth = GetComponent<EnemyHealth>();
    }

    private void OnTriggerEnter(Collider player)
    {
        if(player.CompareTag(tagToCheck) && GameManager.Instance.LaserBeamPlayer.isActive)
        {
            if(enemyHealth)
            {
                enemyHealth.UpdateHealth(damage, transform.position, blastPref);
                hitEffect.Play();
            }
        }
    }
}
