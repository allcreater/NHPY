using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class VelocityEverywhereExtension
{
    public static Vector3 GetVelocity(this GameObject gameObject)
    {
        var rigidBody = gameObject.GetComponent<Rigidbody>();
        if (rigidBody)
            return rigidBody.velocity;

        var velocityComponent = gameObject.GetComponent<Velocity>();
        if (velocityComponent)
            return velocityComponent.velocity;

        //next measure will be correct
        gameObject.AddComponent<Velocity>();
        return Vector3.zero;
    }
}

public class Velocity : MonoBehaviour
{
    public Vector3 velocity { get; private set; }

    private Vector3 previousPosition;

    void Start()
    {
        previousPosition = transform.position;
    }

    void FixedUpdate()
    {
        velocity = (transform.position - previousPosition) / Time.fixedDeltaTime;
        previousPosition = transform.position;
    }
}
