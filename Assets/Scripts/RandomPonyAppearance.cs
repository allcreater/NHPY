using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomPonyAppearance : MonoBehaviour
{
    public GameObject[] manes;
    public GameObject[] tails;
    public Material[] bodyMaterials;
    public GameObject bodyObject;

    public Transform maneSocket;
    public Transform tailSocket;

    [Header("random body color")]
    public float saturationMin = 0.5f, saturationMax = 1.0f;
    public float valueMin = 0.5f, valueMax = 1.0f;

    [Header("random mane and tail color")]
    public float maneSaturationMin = 0.5f, maneSaturationMax = 1.0f;
    public float maneValueMin = 0.5f, maneValueMax = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        var maneTailColor = Random.ColorHSV(0.0f, 1.0f, maneSaturationMin, maneSaturationMax, maneValueMin, maneValueMax);

        if (manes != null && manes.Length > 0)
        {
            var maneInstance = GameObject.Instantiate(manes[Random.Range(0, manes.Length)], maneSocket ?? transform);
            maneInstance.GetComponent<Renderer>().materials[0].color = maneTailColor;
        }

        if (tails != null && tails.Length > 0)
        {
            var tailInstance = GameObject.Instantiate(tails[Random.Range(0, tails.Length)], tailSocket ?? transform);
            tailInstance.GetComponent<Renderer>().materials[0].color = maneTailColor;
        }

        if (bodyObject)
        {
            bodyObject.GetComponent<Renderer>().materials[0].color = Random.ColorHSV(0.0f, 1.0f, saturationMin, saturationMax, valueMin, valueMax);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
