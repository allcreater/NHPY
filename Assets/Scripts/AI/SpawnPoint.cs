using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    [HideInInspector]
    public bool limited;
    public int remainingSpawns = 0;
    public PrefabWithProbability[] overridePrefabs;
    public AnimationCurve spawnProbabilityByDistance;
    public float deviationDistance;
    //TODO: spawn interval here?

    private int remainingSpawnCounter;


    public bool spawnPossible => enabled && (!limited || remainingSpawns > 0);

    public float SpawnProbability(Vector3 target) => spawnProbabilityByDistance.Evaluate(Vector3.Distance(target, transform.position)) * (spawnPossible ? 1.0f : 0.0f);

    public virtual Vector3 spawnPosition => MathExtension.RandomPointAroundCircle(Vector3.up) * deviationDistance;

    private void Start()
    {
        limited = remainingSpawns > 0;
    }
} 
