using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Weapon : MonoBehaviour
{
	public int currentWeaponId;
    public GameObject[] WeaponCollection;

	void Start ()
	{
        //currentWeaponId = 0;
        WeaponCollection[currentWeaponId].SetActive(true);
	}
}
