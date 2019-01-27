using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableLightByGameSettings : MonoBehaviour
{
    public Preferences.GameSettings gameSettings;

    public Preferences.DynamicLightsAmount enableIfBetter;

    // Start is called before the first frame update
    void Start()
    {
        var light = GetComponent<Light>();
        if (light)
            light.enabled = gameSettings.dynamicLightsAmount >= enableIfBetter;
    }

}
