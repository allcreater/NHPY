using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Preferences
{
    public enum DynamicLightsAmount
    {
        OnlyImportant,
        Flashes,
        Everywhere
    }

    [CreateAssetMenu]
    public class GameSettings : ScriptableObject
    {
        public float difficulty = 0.5f;
        public DynamicLightsAmount dynamicLightsAmount = DynamicLightsAmount.OnlyImportant;

        /*
        public void Set()
        {
            PlayerPrefs.SetFloat(nameof(difficulty), difficulty);
            PlayerPrefs.SetString(nameof(dynamicLightsAmount), dynamicLightsAmount.ToString());

            PlayerPrefs.Save();
        }

        private void InitializeManager()
        {
            difficulty = PlayerPrefs.GetFloat(nameof(difficulty), difficulty);

            if (System.Enum.TryParse(PlayerPrefs.GetString(nameof(dynamicLightsAmount)), out DynamicLightsAmount parsedEnum))
                dynamicLightsAmount = parsedEnum;
        }
        */
    }

}