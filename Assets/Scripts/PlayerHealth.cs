using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public GameObject blastPrefab;

    public int HealthPoints;
    

    [Header("Damage")]
    public int dmg_bullet;

    void Start()
    {
        HealthPoints = GameManager.Instance.maxHealthPlayer;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Bullet"))
        {
            Destroy(collision.gameObject);

            if (GameManager.Instance.LaserBeamPlayer.isActive)
                return;

            int id = collision.gameObject.GetComponent<Damage>().dmgId;
            dmg_bullet = GameManager.Instance.DamageListAI[id];

            UpdateHealth(dmg_bullet, collision.transform.position);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("LowerGround"))
        {
            UpdateHealth(100, transform.position);
        }
    }

    public void UpdateHealth(int Damage, Vector3 pos)
    {
        HealthPoints -= Damage;

        GameManager.Instance.UIManager.SetHealthSlider(HealthPoints);
        GameManager.Instance.CameraShaker.Shake();

        if (HealthPoints <= 0)
        {
            GameManager.Instance.CameraShaker.Shake();
            GameManager.Instance.GameOver();
            GameManager.Instance.isImmortal = false;

            GameObject blast = Instantiate(blastPrefab, pos, Quaternion.identity);
            Destroy(blast, 1f);

            gameObject.SetActive(false);
        }
    }
}
