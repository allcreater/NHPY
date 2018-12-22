using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomizePlayerStats : MonoBehaviour
{
    public float minHPFraction = 0.5f;
    public float maxHPFraction = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        var stats = GetComponent<PlayerStats>();
        stats.maxHitPoints *= Random.Range(minHPFraction, maxHPFraction);
        stats.hitPoints = stats.maxHitPoints;
    }
}
