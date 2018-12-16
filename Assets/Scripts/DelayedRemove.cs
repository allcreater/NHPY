using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelayedRemove : MonoBehaviour
{
    public float delay = 1;

    void Awake ()
    {
        GameObject.Destroy(gameObject, delay);
    }

}
