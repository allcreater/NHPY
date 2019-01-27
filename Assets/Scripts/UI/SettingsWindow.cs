using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class SettingsWindow : MonoBehaviour
{
    public Slider difficultySlider;
    public Dropdown lightingQualitySelector;

    public Preferences.GameSettings gameSettings;

    public void SetSettings()
    {
        gameSettings.dynamicLightsAmount = (Preferences.DynamicLightsAmount)lightingQualitySelector.value;
        gameSettings.difficulty = difficultySlider.value;
        
        //gameSettings.Set();
    }

    private void SetControls()
    {
        lightingQualitySelector.value = (int)gameSettings.dynamicLightsAmount;
        difficultySlider.value = gameSettings.difficulty;
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
