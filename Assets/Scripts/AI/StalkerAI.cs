using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class StalkerAI : MonoBehaviour
{
    public float observingDistance = 100.0f;

    private GameObject target;
    private NavMeshAgent nva;

    public bool IsObservingTarget (GameObject target_)
    {
        if (!target_)
            return false;

        return (target_.transform.position - transform.position).sqrMagnitude <= (observingDistance * observingDistance);
    }

    void Awake()
    {
        nva = GetComponent<NavMeshAgent>();
        //nva.Warp(transform.position);
    }

    void Update ()
    {
        if (IsObservingTarget(target))
        {
            nva.destination = target.transform.position;
        }
        else
        {
            target = GameObject.FindGameObjectWithTag("Player");
        }
    }
}
