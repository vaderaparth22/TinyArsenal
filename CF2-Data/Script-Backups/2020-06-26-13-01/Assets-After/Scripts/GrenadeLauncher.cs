using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeLauncher : MonoBehaviour
{
    public float forceSpeed;
    public float fireRate;

    public Transform spawnFrom;
    public GameObject grenadePrefab;

    void Start()
    {
        
    }

   
    void FixedUpdate()
    {
        if(ControlFreak2.CF2Input.GetMouseButtonUp(0))
        {
            GameObject g = Instantiate(grenadePrefab, spawnFrom.position, spawnFrom.localRotation);
            g.GetComponent<Rigidbody>().AddForce(spawnFrom.forward * forceSpeed);
        }
    }
}
