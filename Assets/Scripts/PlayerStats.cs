using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathToken
{
    public float? ReviveHitPoints { get; set; } = null;
}

public class PlayerStats : MonoBehaviour
{
    public GameObject death;

    public float maxHitPoints = 10;
    public float maxManaPoints = 0;
    public float maxStamina = 100;

    public float hitPointsRegenerationRate = 0;
    public float staminaRegenerationRate = 5;

    public UnityEngine.UI.Slider hitPointsBar;
    public UnityEngine.UI.Slider manaPointsBar;
    public UnityEngine.UI.Slider staminaPointsBar;


    private float m_hitPoints = 0;
    private float m_manaPoints = 0;
    private float m_stamina = 0;

    public float hitPoints
    {
        get { return m_hitPoints; }
        set { m_hitPoints = Mathf.Clamp(value, 0.0f, maxHitPoints); }
    }

    public float manaPoints
    {
        get { return m_manaPoints; }
        set { m_manaPoints = Mathf.Clamp(value, 0.0f, maxManaPoints); }
    }

    public float stamina
    {
        get { return m_stamina; }
        set { m_stamina = Mathf.Clamp(value, 0.0f, maxStamina); }
    }

    public float consumeStamina(float staminaConsumption)
    {
        float prevStamina = stamina;
        stamina -= staminaConsumption;

        return (prevStamina - stamina) / staminaConsumption;
    }

    void Start ()
    {
        m_hitPoints = maxHitPoints;
        m_stamina = maxStamina;
    }
    
    void Update()
    {
        if (hitPointsBar)
            hitPointsBar.value = m_hitPoints / maxHitPoints;

        if (manaPointsBar)
            manaPointsBar.value = m_manaPoints / maxManaPoints;

        if (staminaPointsBar)
            staminaPointsBar.value = m_stamina / maxStamina;

        if (m_hitPoints <= 0)
        {
            var token = new DeathToken();
            SendMessage("NoMoreHP", token, SendMessageOptions.DontRequireReceiver);
            if (token.ReviveHitPoints.HasValue)
                hitPoints = token.ReviveHitPoints.Value;
            else
                OnUnhandledDeath();
        }
    }

    //It will influence to player movement, so using FixedUpdate
    void FixedUpdate ()
    {
        hitPoints += hitPointsRegenerationRate * Time.fixedDeltaTime;
        stamina += staminaRegenerationRate * Time.fixedDeltaTime;
    }

    private void OnUnhandledDeath()
    {
        death.SetActive(true);
        death.transform.SetParent(null);

        gameObject.SetActive(false);
        GameObject.Destroy(gameObject, 1.15f);
    }
}
