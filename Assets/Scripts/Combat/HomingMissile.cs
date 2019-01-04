using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class HomingMissile : MonoBehaviour
{
    public float reactionSpeed = 2.0f;
    public float acceleration = 10.0f;
    public float delayAfterAiming = 0.2f;
    [Range(0, -90)]
    public float ranRotXmin = 0;
    [Range(0, -90)]
    public float ranRotXmax = 90;    
    [Range(-90, 90)]
    public float ranRotYmin = -60;
    [Range(-90, 90)]
    public float ranRotYmax = -60;
    public Transform target;

    private Rigidbody rigidBody;

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        //transform.rotation = Quaternion.Euler(-90.0f, Random.Range(-45.0f, 45.0f)*0.0f, 0.0f) * transform.rotation;
        transform.Rotate(Random.Range(ranRotXmin, ranRotXmax), Random.Range(ranRotYmin, ranRotYmax), 0.0f);
    }

    private void FixedUpdate()
    {
        if (target && delayAfterAiming <= 0.0f)
        {
            var targetPos = target.position;
            var targetDir = (target.position - transform.position).normalized;

            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(targetDir, Vector3.up), Time.fixedDeltaTime * reactionSpeed);
        }

        rigidBody.AddRelativeForce(Vector3.forward * acceleration, ForceMode.VelocityChange);

        delayAfterAiming -= Time.fixedDeltaTime;
    }
}
