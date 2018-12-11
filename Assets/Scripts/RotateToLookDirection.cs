using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateToLookDirection : MonoBehaviour
{
    public float reactionSpeed = 2.0f;

    private Vector3 lookDirection, lookTrackingPoint;
    private Animator animator;
    
    void Awake ()
    {
        animator = GetComponent<Animator>();
        lookTrackingPoint = gameObject.transform.position + Vector3.forward * 10.0f;
    }

    void LookTo(Vector3 direction)
    {
        lookDirection = direction;
    }

    private void FixedUpdate()
    {
        var newTargetPos = gameObject.transform.position + lookDirection * 20.0f + Vector3.up * 3.0f;
        lookTrackingPoint = Vector3.Lerp(lookTrackingPoint, newTargetPos, Time.fixedDeltaTime * reactionSpeed);
    }

    void OnAnimatorIK(int layerIndex)
    {
        if (lookDirection.magnitude > 0.01)
        {
            animator.SetLookAtWeight(1.0f);
            animator.SetLookAtPosition(lookTrackingPoint);
        }
    }

}
