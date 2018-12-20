using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomColor : MonoBehaviour
{
    public Material[] randomMaterial;
    public GameObject firstColor;
    public GameObject secondColor;
    public GameObject thirdColor;

    // Start is called before the first frame update
    void Start()
    {
        if (firstColor)
        {
            firstColor.GetComponent<Renderer>().materials[0].color = Random.ColorHSV();
        }
        if (secondColor)
        {
            secondColor.GetComponent<Renderer>().materials[0].color = Random.ColorHSV();
        }
        if (thirdColor)
        {
            thirdColor.GetComponent<Renderer>().materials[0].color = Random.ColorHSV();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
