using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(PlayerStats))]
public class PlayerView : MonoBehaviour, IDeathHandler
{
    public UnityEngine.UI.Slider hitPointsBar;
    public UnityEngine.UI.Slider manaPointsBar;
    public UnityEngine.UI.Slider staminaPointsBar;

    private PlayerStats stats;

    private void Awake()
    {
        stats = GetComponent<PlayerStats>();
    }

    private void Update()
    {
        UpdateStats();
    }

    private void UpdateStats()
    {
        if (hitPointsBar)
            hitPointsBar.value = stats.hitPoints / stats.maxHitPoints;

        if (manaPointsBar)
            manaPointsBar.value = stats.manaPoints / stats.maxManaPoints;

        if (staminaPointsBar)
            staminaPointsBar.value = stats.stamina / stats.maxStamina;
    }

    float? IDeathHandler.OnDeathDoor() => null;
    void IDeathHandler.OnDeath()
    {
        SceneManager.LoadScene("GameOver"); //Not gracefully, but at least at personal player's view script
    }
}
