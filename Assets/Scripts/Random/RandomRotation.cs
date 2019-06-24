using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class RandomRotation : MonoBehaviour
{
    public Vector3 minAngularVelocity, maxAngularVelocity;

    void Awake () 
    {
        var rigidBody = GetComponent<Rigidbody>();

        rigidBody.angularVelocity = new Vector3(
                Random.Range(minAngularVelocity.x, maxAngularVelocity.x),
                Random.Range(minAngularVelocity.y, maxAngularVelocity.y),
                Random.Range(minAngularVelocity.z, maxAngularVelocity.z)
            );
    }
    
    // Update is called once per frame
    void Update ()
    {
        
    }
}
