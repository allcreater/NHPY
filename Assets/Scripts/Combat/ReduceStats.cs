using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReduceStats : ProjectileHit
{
    public float hpDamage = 1.0f;
    public float mpDamage = 0.0f;
    public float staminaDamage = 0.0f;

    private void ChangeStats(Collider target, float factor)
    {
        var stats = target.GetComponent<PlayerStats>();
        if (stats)
        {
            stats.hitPoints -= hpDamage * factor;
            stats.manaPoints -= mpDamage * factor;
            stats.stamina -= staminaDamage * factor;
        }
    }

    protected override void OnDirectHit(Collider other) => ChangeStats(other, 1.0f);
    protected override void OnSplashHit(Collider other, float factor) => ChangeStats(other, factor);
}
