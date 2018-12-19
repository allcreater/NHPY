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

    // Start is called before the first frame update
    void Start()
    {
        if (manes != null && manes.Length > 0)
        {
            var maneInstance = GameObject.Instantiate(manes[Random.Range(0, manes.Length)], maneSocket ?? transform);
        }

        if (manes != null && manes.Length > 0)
        {
            var tailInstance = GameObject.Instantiate(tails[Random.Range(0, tails.Length)], tailSocket ?? transform);
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
