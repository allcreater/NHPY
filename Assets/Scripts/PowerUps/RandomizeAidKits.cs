using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TouchableObject
{
    [RequireComponent(typeof(RestoreStats))]
    public class RandomizeAidKits : MonoBehaviour
    {
        public float restoredHitPointsFactorFrom = 1.0f, restoreHitPointsFactorTo = 1.0f;
        public float restoredManaPointsFactorFrom = 1.0f, restoreManaPointsFactorTo = 1.0f;
        public float restoredStaminaPointsFactorFrom = 1.0f, restoreStaminaPointsFactorTo = 1.0f;

        public float scalingFactor = 1.0f;

        // Start is called before the first frame update
        void Awake()
        {
            var factorHP = Random.Range(restoredHitPointsFactorFrom, restoreHitPointsFactorTo);
            var factorMP = Random.Range(restoredManaPointsFactorFrom, restoreManaPointsFactorTo);
            var factorSP = Random.Range(restoredStaminaPointsFactorFrom, restoreStaminaPointsFactorTo);

            var kit = GetComponent<RestoreStats>();
            kit.restoredHitPoints *= factorHP;
            kit.restoredManaPoints *= factorMP;
            kit.restoredStaminaPoints *= factorSP;

            transform.localScale *= (1.0f + Mathf.Sqrt(factorHP * factorHP + factorMP * factorMP + factorSP * factorSP) * scalingFactor);
        }
    }

}