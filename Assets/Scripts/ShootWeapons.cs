using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootWeapons : MonoBehaviour
{
   
    public Transform[] ShootPosition;
    public GameObject Bullet;
    public float speed = 1000;
    public float fireRate;

    private RaycastHit hit;
    private GameObject projectile;
    private float nextFire = 0.0f;
    private Camera viewCamera;

    
    void Start()
    {
        //viewCamera = Camera.main;
    }

    void Update()
    {
        if(PowerManager.isIndicatorOn == false)
        {
            if (Input.GetMouseButton(0) && Time.realtimeSinceStartup > nextFire)
            {
                nextFire = Time.realtimeSinceStartup + fireRate;

                for (int i = 0; i < ShootPosition.Length; i++)
                {
                    projectile = Instantiate(Bullet, ShootPosition[i].position, ShootPosition[i].rotation) as GameObject;
                    projectile.GetComponent<Rigidbody>().velocity = ShootPosition[i].forward * speed;
                }
            }
        }
    }
}
