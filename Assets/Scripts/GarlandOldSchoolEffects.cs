using System.Linq;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Garland))]
public class GarlandOldSchoolEffects : MonoBehaviour
{
    public enum Effect
    {
        Static,
        SmoothTransition,
        RandomFlashes
    }
    public Effect effect;
    public Color[] lightColors;
    public float speed = 1.0f;
    public float updatePeriod = 0.05f;//20 times per second by default

    private float skippedTime = 0.0f;
    private Garland garland;
    private Color32[] currentColors;

    void Awake()
    {
        garland = GetComponent<Garland>();

        currentColors = lightColors.Select(x => (Color32)x).ToArray();
        garland.SetColors(currentColors);
    }

    void Update()
    {
        skippedTime += Time.deltaTime;
        if (skippedTime > updatePeriod)
            skippedTime -= updatePeriod;
        else
            return;

        var phase = Time.time * speed;

        switch (effect)
        {
            case Effect.SmoothTransition:
            {
                var w =  Mathf.PI * 2.0f / lightColors.Length;
                for (int i = 0; i < lightColors.Length; ++i)
                {
                    currentColors[i] = (Mathf.Sin(w*(i+1) + phase) + 1)* 0.5f * lightColors[i];
                }
            } break;

            case Effect.RandomFlashes:
                {
                    for (int i = 0; i < lightColors.Length; ++i)
                    {
                        currentColors[i] = Random.Range(0, 2) * lightColors[i];
                    }
                }
                break;

            default:
                return;
        }

        garland.SetColors(currentColors);
    }
}
