using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIPolice : MonoBehaviour
{
    private Rigidbody rb;

    public Transform Target;

    public float forceValue;
    public float RotationSpeed;
    public float distanceToPlayer;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Target = GameManager.Instance.MainPlayer.transform;

        //RotateTowardsPlayer();
    }

    private void Update()
    {
        RotateTowardsPlayer();
        //CheckStoppingDistance();

        transform.Translate(transform.forward * forceValue * Time.deltaTime, Space.World);
    }

    private void RotateTowardsPlayer()
    {
        if (Target == null)
        {
            return;
        }

        Vector3 direction = Target.transform.position - transform.position;
        direction.y = 0f;

        Quaternion newRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, RotationSpeed * Time.deltaTime);
    }

    private void CheckStoppingDistance()
    {
        distanceToPlayer = Vector3.Distance(Target.position, transform.position);

        if(distanceToPlayer <= 20f)
        {
            forceValue = 0f;
        }
    }
}
