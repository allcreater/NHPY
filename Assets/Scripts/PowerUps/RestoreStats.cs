using System;
using UnityEngine;
using UnityEngine.Events;

namespace TouchableObject
{
    public class RestoreStats : MonoBehaviour
    {
        public string pickerTag;
        public float restoredHitPoints = 1.0f;
        public float restoredManaPoints = 0.0f;
        public float restoredStaminaPoints = 0.0f;

        private void OnTriggerStay(Collider other)
        {
            if (other.tag != pickerTag)
                return;

            var playerStats = other.GetComponent<PlayerStats>();
            if (playerStats)
            {
                bool restoresHP = playerStats.hitPoints < playerStats.maxHitPoints && !Mathf.Approximately(restoredHitPoints, 0);
                bool restoresMP = playerStats.manaPoints < playerStats.maxManaPoints && !Mathf.Approximately(restoredManaPoints, 0);
                bool restoresSP = playerStats.stamina < playerStats.maxStamina && !Mathf.Approximately(restoredStaminaPoints, 0);

                if (!restoresHP && !restoresMP && !restoresSP)
                    return;

                playerStats.hitPoints += restoredHitPoints;
                playerStats.manaPoints += restoredManaPoints;
                playerStats.stamina += restoredStaminaPoints;
                GameObject.Destroy(gameObject);
            }
        }

    }

}