using System;
using UnityEngine;
using UnityEngine.Events;

namespace TouchableObject
{

    public class RestoreHP : MonoBehaviour
    {
        public string pickerTag;
        public float restoredHP = 1.0f;

        private void OnTriggerEnter(Collider other)
        {
            if (other.tag != pickerTag)
                return;

            var playerStats = other.GetComponent<PlayerStats>();
            if (playerStats)
                playerStats.hitPoints += restoredHP;

            GameObject.Destroy(gameObject);
        }
    }

}