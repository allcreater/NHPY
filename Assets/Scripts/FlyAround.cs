using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyAround : MonoBehaviour
{
    public Transform target;
    public float desiredDistance = 1.0f;
    public float reactionSpeed = 2.0f;

    private Vector3 lookTo;

    // Use this for initialization
    void Start ()
    {

    }

    void LookTo(Vector3 direction)
    {
        lookTo = direction;
    }

    // Update is called once per frame
    void Update ()
    {
        var direction = target.position - transform.position;
        var targetPos = target.position + target.transform.right * desiredDistance; //- direction.normalized * desiredDistance;

        transform.position = Vector3.Lerp(transform.position, targetPos, Time.deltaTime * reactionSpeed);
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(lookTo, Vector3.up), Time.deltaTime * reactionSpeed);
    }
}
