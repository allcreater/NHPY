using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class SettingsWindow : MonoBehaviour
{
    public Slider difficultySlider;
    public Dropdown lightingQualitySelector;

    public void SetSettings()
    {
        Preferences.GameSettings.instance.dynamicLightsAmount = (Preferences.DynamicLightsAmount)lightingQualitySelector.value;
        Preferences.GameSettings.instance.difficulty = difficultySlider.value;

        Preferences.GameSettings.instance.Set();
    }

    private void SetControls()
    {
        lightingQualitySelector.value = (int)Preferences.GameSettings.instance.dynamicLightsAmount;
        difficultySlider.value = Preferences.GameSettings.instance.difficulty;
    }

    private void Start()
    {
        SetControls();
    }


    private void OnEnable()
    {
        SetControls();
    }

}
