using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public GameObject blastPrefab;
    public GameObject healthBox;
    public GameObject weaponBox;

    public int HealthPoints;

    [Header("Damage")]
    public int dmg_bullet_percent;

    void Start()
    {
        if (transform.name.Contains("Big"))
        {
            HealthPoints = GameManager.Instance.maxHealthBigAI;
        }
        else
        {
            HealthPoints = GameManager.Instance.maxHealthAI;
        }   
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Bullet"))
        {
            Destroy(collision.gameObject);

            int id = collision.gameObject.GetComponent<Damage>().dmgId;
            dmg_bullet_percent = GameManager.Instance.DamageList[id];

            UpdateHealth(dmg_bullet_percent, collision.transform.position, blastPrefab);
        }
    }

    public void UpdateHealth(int Damage, Vector3 pos, GameObject blastPref)
    {
        HealthPoints -= Damage;

        if (HealthPoints <= 0)
        {
            GameManager.Instance.killCounter++;
            GameManager.Instance.minWaveKills++;

            //For upgrade box
            if (GameManager.Instance.killCounter >= GameManager.Instance.minUpgradeKills)
            {
                GameManager.Instance.minUpgradeKills += 10;
                Instantiate(weaponBox, pos, weaponBox.transform.rotation);
            }

            //For Health box
            //if (GameManager.Instance.killCounter >= GameManager.Instance.minHealthKills)
            //{
            //    GameManager.Instance.minHealthKills += 10;
            //    Instantiate(healthBox, pos, healthBox.transform.rotation);
            //}

            if(GameManager.Instance.minWaveKills == GameManager.Instance.WaveKills[GameManager.Instance.currentWave])
            {
                GameManager.Instance.minWaveKills = 0;
                GameManager.Instance.AiCount = 0;

                GameManager.Instance.ChangeWave();
            }

            //For Light blackout
            if (GameManager.Instance.killCounter > GameManager.Instance.minBlackoutkills) //This will only run once
            {
                GameManager.Instance.TurnLight(false);
                GameManager.Instance.minBlackoutkills = int.MaxValue;
            }

            //GameManager.Instance.AiCount--;

            FlameHit flameHit = GameManager.Instance.FlameHit;

            if(flameHit.TriggerList.Contains(gameObject))
                flameHit.TriggerList.Remove(gameObject);

            GameObject blast = Instantiate(blastPref, pos, Quaternion.identity);
            Destroy(blast, 1f);

            Destroy(gameObject);
        }
    }
}
