using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomColor : MonoBehaviour
{
    public Material[] randomMaterial;
    public float hueMin = 0;
    public float hueMax = 0;
    public float saturationMin = 0;
    public float saturationMax = 0;
    public float valueMin = 0;
    public float valueMax = 0;
    public GameObject firstColor;
    public GameObject secondColor;
    public GameObject thirdColor;

    // Start is called before the first frame update
    void Start()
    {
        if (firstColor)
        {
            firstColor.GetComponent<Renderer>().materials[0].color = Random.ColorHSV(hueMin,hueMax,saturationMin, saturationMax,valueMin,valueMax);
        }
        if (secondColor)
        {
            secondColor.GetComponent<Renderer>().materials[0].color = Random.ColorHSV(hueMin, hueMax, saturationMin, saturationMax, valueMin, valueMax);
        }
        if (thirdColor)
        {
            thirdColor.GetComponent<Renderer>().materials[0].color = Random.ColorHSV(hueMin, hueMax, saturationMin, saturationMax, valueMin, valueMax);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
