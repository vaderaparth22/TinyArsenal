using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBeamPlayer : MonoBehaviour
{
    [Header("[Beyblade]")]
    public bool isActive;
    public Transform HeadTransform;
    public float swordRotateSpeed;
    public float maxBladeActiveTimer = 5f;
    private float Timer;

    private void OnEnable()
    {
        isActive = true;
        swordRotateSpeed = 0f;
        Timer = 0f;
        GameManager.Instance.isImmortal = true;
    }

    private void OnDisable()
    {
        isActive = false;
        swordRotateSpeed = 0f;
        Timer = 0f;
        GameManager.Instance.isImmortal = false;
    }

    void Start()
    {
        
    }

    void Update()
    {
        if (isActive)
        {
            RotateSwords();
            CalculateTimeToDeactivate();
        }
    }

    private void RotateSwords()
    {
        HeadTransform.Rotate(transform.up * swordRotateSpeed * Time.smoothDeltaTime);
    }

    private void CalculateTimeToDeactivate()
    {
        Timer += Time.deltaTime;

        if (Timer >= maxBladeActiveTimer)
        {
            Timer = 0f;

            isActive = false;
            swordRotateSpeed = 0f;

            gameObject.SetActive(false);
        }
    }
}
