using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MageAI : MonoBehaviour
{
    public float observingDistance = 100.0f;
    public float shootingDistance = 50.0f;

    public GameObject death;
    private GameObject target;
    private NavMeshAgent nva;
    private Shooting shootingComponent;

    public bool IsObservingTarget (GameObject target_)
    {
        if (!target_)
            return false;

        return (target_.transform.position - transform.position).sqrMagnitude <= (observingDistance * observingDistance);
    }

    void Awake()
    {
        nva = GetComponent<NavMeshAgent>();
        shootingComponent = GetComponentInChildren<Shooting>();

        //nva.Warp(transform.position);
    }

    void Update ()
    {
        if (death.activeInHierarchy==true)
            return;
            

        if (IsObservingTarget(target))
        {
            nva.destination = target.transform.position;

            if ((target.transform.position - transform.position).sqrMagnitude <= (shootingDistance * shootingDistance))
                shootingComponent.ShootTo(new ShootToParams(target.transform.position, target.transform, "Main"));
        }
        else
        {
            target = GameObject.FindGameObjectWithTag("Player");
        }
    }
}
