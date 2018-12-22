using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyMovement : MonoBehaviour
{
    public float movementSpeedDeviation = 0.3f;
    public bool isFlying = false;

    private NavMeshAgent navMeshAgent;
    private Animator animator;

    private Vector3 prevPosition;

    void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();

        navMeshAgent.speed = Random.Range(Mathf.Max(0.0f, navMeshAgent.speed * (1 - movementSpeedDeviation)), navMeshAgent.speed * (1 + movementSpeedDeviation));

        prevPosition = transform.position;
    }

    void Update()
    {
        var velocity = (transform.position - prevPosition) / Time.deltaTime;
        prevPosition = transform.position;

        animator.SetFloat("Speed", velocity.magnitude);
        animator.SetBool("IsFlying", isFlying);
    }
}
