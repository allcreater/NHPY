using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReduceStats : MonoBehaviour
{
    public float hpDamage = 1.0f;
    public float mpDamage = 0.0f;
    public float staminaDamage = 0.0f;

    // Start is called before the first frame update

    void CollidedWith(Collider other)
    {
        var stats = other.GetComponent<PlayerStats>();
        if (stats)
        {
            stats.hitPoints -= hpDamage;
            stats.manaPoints -= mpDamage;
            stats.stamina -= staminaDamage;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
