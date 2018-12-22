using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;

public class Projectile : MonoBehaviour 
{
    public string[] tags;
    public GameObject hitEffect;
    public float startDelay = 0.0f;
    public float explodeDelay = 0.0f;

    // Use this for initialization
    void Start () 
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!tags.Contains(other.gameObject.tag))
            return;
        
        if (hitEffect)
        {
            StartCoroutine(ExecuteAfterTime(startDelay, explodeDelay, other, hitEffect));
        }
    }

    IEnumerator ExecuteAfterTime(float startDelay, float explodeDelay, Collider other, GameObject hitEffect)
    {
        if (!Mathf.Approximately(startDelay, 0.0f))
            yield return new WaitForSeconds(startDelay);
        GetComponent<Rigidbody>().velocity = Vector3.up * 20.0f;

        if (!Mathf.Approximately(explodeDelay, 0.0f))
            yield return new WaitForSeconds(explodeDelay);

        Debug.Log("Hello!");

        var effectObject = GameObject.Instantiate(hitEffect, transform.position, transform.rotation, null);
        effectObject.SendMessage("CollidedWith", other);
            GameObject.Destroy(gameObject);
    }

    // Update is called once per frame
    void Update ()
    {
        
    }
}
