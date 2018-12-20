using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotation : MonoBehaviour
{
    public Vector3 angularVelocity;

    // Update is called once per frame
    void Update()
    {
        var dRotation = Quaternion.Euler(angularVelocity * Time.deltaTime);
        transform.rotation = dRotation * transform.rotation;
    }
}
