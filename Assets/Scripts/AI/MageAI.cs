using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MageAI : MonoBehaviour
{
    public float observingDistance = 100.0f;
    public float shootingDistance = 50.0f;
    public float teleportationDistanceReact = 10.0f;
    public float teleportDistance = 50.0f;

    public GameObject teleportEffect;

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

        return (target_.transform.position - transform.position).sqrMagnitude <= (teleportationDistanceReact * teleportationDistanceReact);
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
        if (IsObservingTarget(target))
        {
            nva.destination = target.transform.position;

            if ((target.transform.position - transform.position).sqrMagnitude <= (shootingDistance * shootingDistance))
                shootingComponent.ShootTo(new ShootToParams(target.transform.position, target.transform));
        }
        if (IsTargetClose(target))
        {
            var randomPos2D = Random.insideUnitCircle;
            Vector3 targetPos = new Vector3(randomPos2D.x, 0, randomPos2D.y) * teleportDistance + target.transform.position;
            if (NavMesh.SamplePosition(targetPos, out NavMeshHit hit, teleportDistance, 1))
            {
                //teleport in effect
                var teleport = GameObject.Instantiate(teleportEffect, transform.position, transform.rotation);

                if (audioSource)
                    audioSource.PlayOneShot(audioSource.clip);

                nva.Warp(hit.position);
                //teleport out effect
                teleport = GameObject.Instantiate(teleportEffect, transform.position, transform.rotation);
            }
        }
        else
        {
            target = GameObject.FindGameObjectWithTag("Player");
        }
    }
}
