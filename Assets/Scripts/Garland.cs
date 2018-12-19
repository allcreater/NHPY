using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
[SelectionBase]
[RequireComponent(typeof(ParticleSystem))]
[RequireComponent(typeof(Spline))]
public class Garland : MonoBehaviour
{
    private Spline spline;
    private ParticleSystem system;
    private ParticleSystem.Particle[] particles;

    private Color32[] colorsToSet;
    private bool needUpdate = true;

    public void SetColors(Color32[] colors)
    {
        colorsToSet = colors;
        needUpdate = true;
    }

    private void OnEnable()
    {
        needUpdate = true;
    }

    private void Awake()
    {
        spline = GetComponent<Spline>();
        system = GetComponent<ParticleSystem>();

        spline.NodeCountChanged.AddListener(() => needUpdate = true);
    }

    private void OnValidate()
    {
        needUpdate = true;
    }

    private void Update()
    {
        if (Application.isEditor && !Application.isPlaying)
            needUpdate = true;
    }

    private void LateUpdate()
    {
        if (needUpdate)
        {
            SetParticles();
            needUpdate = false;
        }
    }

    private void SetParticles()
    {
        InitializeIfNeeded();

        int numParticlesAlive = system.GetParticles(particles);
        var splineDistanceBetweenPoints = (spline.nodes.Count - 1.0f)/ numParticlesAlive;

        for (int i = 0; i < numParticlesAlive; ++i)
        {
            ref var particle = ref particles[i];
            particle.position = spline.GetLocationAlongSpline(i * splineDistanceBetweenPoints);

            if (colorsToSet != null && colorsToSet.Length >= 1)
                particle.startColor = colorsToSet[i % colorsToSet.Length];
        }

        system.SetParticles(particles, numParticlesAlive);
    }

    private void InitializeIfNeeded()
    {
        if (particles == null || particles.Length < system.main.maxParticles)
            particles = new ParticleSystem.Particle[system.main.maxParticles];
    }
}