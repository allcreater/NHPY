using System.Linq;
using UnityEngine;

public interface IDeathHandler
{
    float? OnDeathDoor();
    void OnDeath();
}

public class PlayerStats : MonoBehaviour
{
    public GameObject death;

    public float maxHitPoints = 10;
    public float maxManaPoints = 0;
    public float maxStamina = 100;

    public float hitPointsRegenerationRate = 0;
    public float staminaRegenerationRate = 5;

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
        if (hitPoints <= 0)
        {
            //send notification through children until someone will return revive hit points
            var reviveHitPoints = GetComponentsInChildren<IDeathHandler>().Select(x => x.OnDeathDoor()).FirstOrDefault(x => x.HasValue && x.Value > 0);

            if (reviveHitPoints.HasValue)
                hitPoints = reviveHitPoints.Value;
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
        foreach (var handler in GetComponentsInChildren<IDeathHandler>())
            handler.OnDeath();

        //Move to separate component
        death.SetActive(true);
        death.transform.SetParent(null);

        gameObject.SetActive(false);
        GameObject.Destroy(gameObject, 1.15f);
    }
}
