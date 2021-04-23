using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombRoll : MonoBehaviour
{
    private Rigidbody _rigidbody;
    public float forceValue;

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();

        _rigidbody.AddForce(transform.forward * forceValue);
    }
}
