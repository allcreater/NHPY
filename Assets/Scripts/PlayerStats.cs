using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public float maxHitPoints = 10;
    public float maxStamina = 100;

    public float hitPointsRegenerationRate = 0;
    public float staminaRegenerationRate = 5;

    private float m_hitPoints = 0;
    private float m_stamina = 0;

    public float hitPoints
    {
        get { return m_hitPoints; }
        set { m_hitPoints = Mathf.Clamp(value, 0.0f, maxHitPoints); }
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

        Debug.Log($"stamina: {stamina}");
        return (prevStamina - stamina) / staminaConsumption;
    }

    void Start ()
    {
        m_hitPoints = maxHitPoints;
        m_stamina = maxStamina;
    }
    
    void Awake ()
    {
        
    }

    //It will influence to player movement, so using FixedUpdate
    void FixedUpdate ()
    {
        hitPoints += hitPointsRegenerationRate * Time.fixedDeltaTime;
        stamina += staminaRegenerationRate * Time.fixedDeltaTime;
        Debug.Log($"now stamina is {stamina}");
    }
}
