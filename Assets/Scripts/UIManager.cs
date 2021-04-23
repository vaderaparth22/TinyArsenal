using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameObject GameOverScreen;

    public Image healthSlider;
    public Text healthText;


    void Start()
    {
        healthText.text = GameManager.Instance.maxHealthPlayer.ToString();
    }

    public void SetHealthSlider(int hp)
    {
        int maxHealth = GameManager.Instance.maxHealthPlayer;

        float total = ((float)(hp * 1f) / 100);

        if (total <= 0f)
        {
            total = 0f;
        }

        healthSlider.fillAmount = total;
        healthText.text = hp.ToString();
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(0);
    }
}