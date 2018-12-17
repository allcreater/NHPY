using System.Linq;
using UnityEngine;

[RequireComponent(typeof(PlayerStats))]
public class GloomAttackReceiver : MonoBehaviour
{
    public float damageThreshold;
    public float maxReceiveRadius = 20.0f;
    public string layerMask = "Default";
    public string objectsTag = "Enemy";

    private PlayerStats playerStats;
    private GloomParticleEffect particleEffect;

    private void Awake()
    {
        playerStats = GetComponent<PlayerStats>();
        particleEffect = GetComponentInChildren<GloomParticleEffect>();
    }

    float DistanceAttenuation(float distance, float innerR, float outerR)
    {
        float d = Mathf.Max(distance, innerR);
        return Mathf.Clamp01(1.0f - Mathf.Pow(d / outerR, 4.0f)) / (d * d + 1.0f);
    }

    void Update()
    {
        float influence = 0.0f;
        foreach (var colliderGroup in Physics.OverlapSphere(transform.position, maxReceiveRadius, LayerMask.GetMask(layerMask), QueryTriggerInteraction.Ignore).Where(x => x.tag == objectsTag).GroupBy(x => x.gameObject))
        {
            var gloomAttacker = colliderGroup.First().GetComponent<GloomAttack>();
            if (!gloomAttacker)
                continue;

            var dir = gloomAttacker.transform.position - transform.position;

            influence += DistanceAttenuation(dir.magnitude, 5.0f, gloomAttacker.damageRadius) * gloomAttacker.damage;

            particleEffect?.GloomAttackedBy(gloomAttacker);
        }

        if (influence < damageThreshold)
            influence = 0.0f;//return

        playerStats.hitPoints -= influence;
        Debug.Log(influence);
    }
}
