using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class StalkerAI : MonoBehaviour
{
    private GameObject target;

    private NavMeshAgent nva;

    void Awake()
    {
        nva = GetComponent<NavMeshAgent>();
        //nva.Warp(transform.position);
    }

    void Update ()
    {
        if (!target)
            target = GameObject.FindGameObjectWithTag("Player");

        if (target)
        {
            nva.destination = target.transform.position;
        }
    }
}
