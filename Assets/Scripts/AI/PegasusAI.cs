using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PegasusAI : MonoBehaviour
{
    public GameObject death;
    public float observingDistance = 100.0f;
    public float whirlingDistance = 10.0f;

    private GameObject target;
    private NavMeshAgent nva;


    public bool IsObservingTarget(GameObject target_)
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
        if (death.activeInHierarchy == true)
            return;

        if (IsObservingTarget(target))
        {
            var dir = target.transform.position - transform.position;
            float distance = dir.magnitude;
            dir /= distance;

            if (distance < whirlingDistance)
            {
                var direction = Vector3.Cross(Vector3.up, dir);
                nva.destination = transform.position + direction * whirlingDistance;
            }
            else
            {
                nva.destination = target.transform.position;
            }
        }
        else
        {
            target = GameObject.FindGameObjectWithTag("Player");
        }
    }
}
