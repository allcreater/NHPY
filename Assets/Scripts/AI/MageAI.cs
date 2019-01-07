﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MageAI : MonoBehaviour
{
    public float observingDistance = 100.0f;
    public float shootingDistance = 50.0f;
    public float teleportationDistansReact = 10.0f;
    public float teleportDistans = 50.0f;

    public GameObject teleportEffect;
    public GameObject death;
    private GameObject target;
    private NavMeshAgent nva;
    private Shooting shootingComponent;

    private AudioSource audioSource;
    public AudioClip[] clips;

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

        return (target_.transform.position - transform.position).sqrMagnitude <= (teleportationDistansReact * teleportationDistansReact);
    }

    void Awake()
    {
        var clip = clips[Random.Range(0, clips.Length)];
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = clip;
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
        if (IsTargetClose(target))
        {
            Vector3 randomDirection = Random.insideUnitSphere * teleportDistans + target.transform.position;
            NavMeshHit hit;
            NavMesh.SamplePosition(randomDirection, out hit, teleportDistans, 1);
            Vector3 randomTeleport = hit.position;
            var teleport = GameObject.Instantiate(teleportEffect,transform.position, transform.rotation);

            if (audioSource)
                audioSource.PlayOneShot(audioSource.clip);

            nva.Warp(randomTeleport);
            teleport = GameObject.Instantiate(teleportEffect,transform.position, transform.rotation);
        }
        else
        {
            target = GameObject.FindGameObjectWithTag("Player");
        }
    }
}
