using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameThrower : MonoBehaviour
{
    public ParticleSystem[] FlameParticles;
    public GameObject PointLight;

    public bool isFlame;

    void Start()
    {
        
    }

    
    void Update()
    {
        if(ControlFreak2.CF2Input.GetMouseButtonDown(0))
        {
            isFlame = true;

            foreach(ParticleSystem ps in FlameParticles)
            {
                var em = ps.emission;
                em.enabled = true;
            }

            PointLight.SetActive(true);
        }
        else if(ControlFreak2.CF2Input.GetMouseButtonUp(0))
        {
            isFlame = false;

            foreach (ParticleSystem ps in FlameParticles)
            {
                var em = ps.emission;
                em.enabled = false;
            }

            PointLight.SetActive(false);
        }
    }
}
