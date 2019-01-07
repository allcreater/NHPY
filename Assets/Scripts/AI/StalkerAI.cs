using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class StalkerAI : MonoBehaviour
{
    public float observingDistance = 100.0f;
    public float teleportationDistance = 10.0f;

    private GameObject target;
    private NavMeshAgent nva;

    public bool IsObservingTarget (GameObject target_)
    {
        if (!target_)
            return false;

        return (target_.transform.position - transform.position).sqrMagnitude <= (observingDistance * observingDistance);
    }

    public bool IsTargetClose(GameObject target_)
    {
        if (!target_)
            return false;

        return (target_.transform.position - transform.position).sqrMagnitude <= (teleportationDistance * teleportationDistance);
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

        if (IsTargetClose(target))
        {
            Vector3 randomDirection = Random.insideUnitSphere * 30 + target.transform.position;
            NavMeshHit hit;
            NavMesh.SamplePosition(randomDirection, out hit, 10, 1);
            Vector3 randomTeleport = hit.position;
            nva.Warp(randomTeleport);
        }           
        else
        {
            target = GameObject.FindGameObjectWithTag("Player");
        }
    }
}
