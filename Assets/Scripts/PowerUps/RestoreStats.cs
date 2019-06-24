using System;
using UnityEngine;
using UnityEngine.Events;

namespace TouchableObject
{
    public class RestoreStats : GenericPowerUp
    {
        public float restoredHitPoints = 1.0f;
        public float restoredManaPoints = 0.0f;
        public float restoredStaminaPoints = 0.0f;


        protected override bool TryActivate(Collider other)
        {
            var playerStats = other.GetComponent<PlayerStats>();
            if (!playerStats)
                return false;

            bool restoresHP = playerStats.hitPoints < playerStats.maxHitPoints && !Mathf.Approximately(restoredHitPoints, 0);
            bool restoresMP = playerStats.manaPoints < playerStats.maxManaPoints && !Mathf.Approximately(restoredManaPoints, 0);
            bool restoresSP = playerStats.stamina < playerStats.maxStamina && !Mathf.Approximately(restoredStaminaPoints, 0);

            if (!restoresHP && !restoresMP && !restoresSP)
                return false;

            playerStats.hitPoints += restoredHitPoints;
            playerStats.manaPoints += restoredManaPoints;
            playerStats.stamina += restoredStaminaPoints;

            return true;
        }

    }

}