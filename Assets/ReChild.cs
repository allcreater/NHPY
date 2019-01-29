using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReChild : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        gameObject.transform.SetParent(null);
    }
}
