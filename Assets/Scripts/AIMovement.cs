using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIMovement : MonoBehaviour
{
    private NavMeshAgent navMeshAgent;

    public Transform Target;

    public float RotationSpeed;

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();

        if (navMeshAgent != null)
        {
            navMeshAgent.updateRotation = false;

            if(transform.name.Equals("Enemy2"))
            {
                navMeshAgent.speed = GameManager.Instance.RedAISpeed;
            }
            else if (transform.name.Contains("Big"))
            {
                navMeshAgent.speed = GameManager.Instance.BigAISpeed;
            }
            else
            {
                navMeshAgent.speed = GameManager.Instance.NormalAISpeed;
            }
        }

        Target = GameManager.Instance.MainPlayer.transform;
    }

    void Update()
    {
        if(Target == null)
        {
            return;
        }

        navMeshAgent.destination = Target.position;

        Rotate_Character();   
    }

    //private void Move_Character()
    //{
    //    Vector3 direction_norm = (Target.transform.position - transform.position).normalized;

    //    Vector3 movement = direction_norm * 0.3f;
    //    _movement = movement.normalized * Mathf.Clamp01(movement.magnitude);
    //    _movement.y = 0f;

    //    rb.AddForce(_movement * MovementSpeed);
    //}

    private void Rotate_Character()
    {
        Vector3 direction = Target.transform.position - transform.position;
        direction.y = 0f;

        Quaternion newRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, RotationSpeed * Time.deltaTime);
    }
}
