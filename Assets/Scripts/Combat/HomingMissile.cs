using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingMissile : MonoBehaviour
{
    public float reactionSpeed = 2.0f;
    public Transform target;

    private void FixedUpdate()
    {
        if (!target)
            return;

        var targetPos = target.position;
        var targetDir = (target.position - transform.position).normalized;

        //transform.position = Vector3.Lerp(transform.position, targetPos, Time.fixedDeltaTime * reactionSpeed);
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(targetDir, Vector3.up), Time.fixedDeltaTime * reactionSpeed * 3);

        GetComponent<Rigidbody>().AddRelativeForce(Vector3.forward * 10.0f, ForceMode.VelocityChange);
    }
}
