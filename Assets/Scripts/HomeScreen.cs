using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HomeScreen : MonoBehaviour
{
    public GameObject InfoPanel;
    public GameObject StartBtn;

    public void RedirectToMain()
    {
        SceneManager.LoadScene(1);
    }
}
