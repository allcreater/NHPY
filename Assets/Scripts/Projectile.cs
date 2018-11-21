using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;

public class Projectile : MonoBehaviour 
{
    public string[] tags;

    // Use this for initialization
    void Start () 
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!tags.Contains(gameObject.tag))
            return;

        GameObject.Destroy(gameObject);

        //GetComponent<Rigidbody>().velocity += Vector3.up * 10;
    }

    // Update is called once per frame
    void Update ()
    {
        
    }
}
