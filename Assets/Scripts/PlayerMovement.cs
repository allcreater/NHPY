﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnitySampleAssets.CrossPlatformInput;

public class PlayerMovement : MonoBehaviour
{
    public float walkSpeed;
    public float runSpeed;
    public float staminaConsumption;
    public float dragOnGround = 25;
    public LayerMask groundLayerMask;

    private Animator animator;          // Reference to the animator component.
    private new Rigidbody rigidbody;    // Reference to the player's rigidbody.
    private PlayerStats stats;

    private Vector3 m_moveDirection;
    private float m_actualSpeed;
    private float m_speedBoost;

    private int groundContacts = 0;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        rigidbody = GetComponent<Rigidbody>();
        stats = GetComponent<PlayerStats>();

        m_speedBoost = runSpeed / walkSpeed;
        if (m_speedBoost < 1)
            throw new System.Exception("runSpeed must be bigger than walk speed!");
    }

    void MoveTo(MoveToParams moveParams)
    {
        m_moveDirection = Vector3.ClampMagnitude(moveParams.direction, 1);
        if (moveParams.isRun)
            m_moveDirection *= m_speedBoost;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (((1 << other.gameObject.layer) & groundLayerMask) > 0)
            groundContacts++;
    }

    private void OnTriggerExit(Collider other)
    {
        if (((1 << other.gameObject.layer) & groundLayerMask) > 0)
            groundContacts--;
    }

    private void FixedUpdate ()
    {
        if (groundContacts <= 0)
        {
            rigidbody.drag = 0;
            return;
        }

        rigidbody.drag = dragOnGround;

        var length = m_moveDirection.magnitude;
        var direction = m_moveDirection / (length + 0.001f);

        if (length > 1.01)
        {
            float staminaOverrun = (length - 1) / (m_speedBoost - 1) * staminaConsumption;
            float amplification = stats.consumeStamina(staminaOverrun * Time.deltaTime);

            length = Mathf.Clamp(length, 0, 1 + m_speedBoost * amplification);
        }

        animator.SetFloat("Speed", length);
        rigidbody.AddForce(direction * (length * walkSpeed), ForceMode.VelocityChange);

        if (m_moveDirection.sqrMagnitude > 0.0)
        {
            var newRotation = Quaternion.LookRotation(m_moveDirection.normalized, Vector3.up);
            rigidbody.MoveRotation(Quaternion.Lerp(rigidbody.rotation, newRotation, Time.fixedDeltaTime * 10.0f));
        }


    }
}
