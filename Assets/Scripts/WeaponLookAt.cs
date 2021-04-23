using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponLookAt : MonoBehaviour
{
    public Transform[] PlayerGuns;
    private void FixedUpdate()
    {
        Ray rayOrigin = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitinfo;

        if (Physics.Raycast(rayOrigin, out hitinfo))
        {
            if (hitinfo.collider != null)
            {
                Vector3 direction = hitinfo.point - PlayerGuns[0].position;
                PlayerGuns[0].rotation = Quaternion.LookRotation(direction);
                
            }
        }
    }
}
