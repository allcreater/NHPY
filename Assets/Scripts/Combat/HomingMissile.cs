using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class HomingMissile : MonoBehaviour
{
    public float reactionSpeed = 2.0f;
    public float acceleration = 10.0f;
    public Transform target;

    private Rigidbody rigidBody;

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if (target)
        {
            var targetPos = target.position;
            var targetDir = (target.position - transform.position).normalized;

            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(targetDir, Vector3.up), Time.fixedDeltaTime * reactionSpeed);
        }

        rigidBody.AddRelativeForce(Vector3.forward * acceleration, ForceMode.VelocityChange);
    }
}
