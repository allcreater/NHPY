using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemyManager))]
public class IncreasingSpawnRate : MonoBehaviour
{
    public int maxNumberOfNpcs = 100; //Пожалейте компьютер!
    public float minSpawnInterval;
    public float speedUpPeriod = 600.0f;

    private int start_desiredNumberOfNpcs;
    private float start_spawnInterval;

    private float workTime = 0.0f;
    private EnemyManager enemyManager;

    private void Awake()
    {
        enemyManager = GetComponent<EnemyManager>();
        start_desiredNumberOfNpcs = enemyManager.desiredNumberOfNpcs;
        start_spawnInterval = enemyManager.spawnInterval;
    }

    private void Update()
    {
        var phase = workTime / speedUpPeriod;
        enemyManager.desiredNumberOfNpcs = (int)Mathf.Lerp(start_desiredNumberOfNpcs, maxNumberOfNpcs, phase);
        enemyManager.spawnInterval = Mathf.Lerp(start_spawnInterval, minSpawnInterval, phase);

        workTime += Time.deltaTime;
    }
}
