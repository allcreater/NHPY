using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class GloomParticleEffect : MonoBehaviour
{
    private ParticleSystem system;
    private ParticleSystem.Particle[] particles;

    private float homingSpeed = 0.1f;

    private void Awake()
    {
        if (system == null)
            system = GetComponent<ParticleSystem>();
    }

    public void GloomAttackedBy(GloomAttack attacker)
    {
        system.Emit(new ParticleSystem.EmitParams() { position = attacker.transform.position, velocity = Vector3.zero}, 1);
    }

    private void LateUpdate()
    {
        InitializeIfNeeded();

        // GetParticles is allocation free because we reuse the m_Particles buffer between updates
        int numParticlesAlive = system.GetParticles(particles);

        // Change only the particles that are alive
        var targetPos = transform.position;

        for (int i = 0; i < numParticlesAlive; ++i)
        {
            var dirToTarget = targetPos - particles[i].position;
            //particles[i].velocity += (dirToTarget) * homingSpeed;

            if (dirToTarget.magnitude < 0.7f)
                particles[i].remainingLifetime -= 0.1f;

            var newPos = Vector3.Lerp(particles[i].position, targetPos, Time.deltaTime * 4);
            particles[i].velocity = (newPos - particles[i].position) / Time.deltaTime;
            particles[i].color = Color32.Lerp(Color.black, Color.white, (particles[i].velocity - gameObject.GetVelocity()).magnitude / 20.0f);
        }

        // Apply the particle changes to the particle system
        system.SetParticles(particles, numParticlesAlive);
    }

    private void InitializeIfNeeded()
    {
        if (particles == null || particles.Length < system.main.maxParticles)
            particles = new ParticleSystem.Particle[system.main.maxParticles];
    }

}
