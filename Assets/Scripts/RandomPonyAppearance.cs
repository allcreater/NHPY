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

    // Start is called before the first frame update
    void Start()
    {
        var maneInstance = GameObject.Instantiate(manes[Random.Range(0, manes.Length)], maneSocket ?? transform);
        var tailInstance = GameObject.Instantiate(tails[Random.Range(0, tails.Length)], tailSocket ?? transform);

        if (bodyObject)
        {
            bodyObject.GetComponent<Renderer>().materials[0].color = Random.ColorHSV();
        }

        var a = new MeshRenderer();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
