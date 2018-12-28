using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomActive : MonoBehaviour
{
    public GameObject[] objectsList;
    // Start is called before the first frame update
    void Awake()
    {
        objectsList[Random.Range(0, objectsList.Length)].SetActive(true);
    }

}
