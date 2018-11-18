using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnitySampleAssets.CrossPlatformInput;

public class PlayerMovement : MonoBehaviour
{
    public float movementSpeed;

    private Animator animator;          // Reference to the animator component.
    private new Rigidbody rigidbody;    // Reference to the player's rigidbody.

    private Vector3 m_moveDirection;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        m_moveDirection = Vector3.ClampMagnitude(new Vector3(CrossPlatformInputManager.GetAxisRaw("Horizontal"), 0.0f, CrossPlatformInputManager.GetAxisRaw("Vertical")), 1.0f);
    }

    private void FixedUpdate ()
    {
        rigidbody.MovePosition(rigidbody.position + m_moveDirection * movementSpeed * Time.fixedDeltaTime);

        if (m_moveDirection.sqrMagnitude > 0.0)
        {
            var newRotation = Quaternion.LookRotation(m_moveDirection.normalized, Vector3.up);
            Debug.Log(m_moveDirection.normalized);//newRotation.eulerAngles);
            rigidbody.MoveRotation(Quaternion.Lerp(rigidbody.rotation, newRotation, Time.fixedDeltaTime * 10.0f));
        }
    }
}
