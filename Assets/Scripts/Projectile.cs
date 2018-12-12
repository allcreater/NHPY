using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;

public class Projectile : MonoBehaviour 
{
    public string[] tags;
    public GameObject hitEffect;

    // Use this for initialization
    void Start () 
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!tags.Contains(other.gameObject.tag))
            return;

        if (hitEffect)
            GameObject.Instantiate(hitEffect, transform.position, transform.rotation, null);

        //Debug.Log(other.gameObject.name);
        GameObject.Destroy(this);
    }

    // Update is called once per frame
    void Update ()
    {
        
    }
}
