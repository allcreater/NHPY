using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;

public class Projectile : MonoBehaviour 
{
    public string[] tags;
    public GameObject hitEffect;
    public float startDelay = 0.0f;

    private Coroutine delayedEffect;

    // Use this for initialization
    void Start () 
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!tags.Contains(other.gameObject.tag) || delayedEffect != null)
            return;
        
        if (hitEffect)
            delayedEffect = StartCoroutine(ExecuteAfterTime(other, hitEffect));
    }

    IEnumerator ExecuteAfterTime( Collider other, GameObject hitEffect)
    {
        Debug.Log($"{gameObject.name} EAF {startDelay}");
        if (!Mathf.Approximately(startDelay, 0.0f))
            yield return new WaitForSeconds(startDelay);

        var effectObject = GameObject.Instantiate(hitEffect, transform.position, transform.rotation, null);
        effectObject.SendMessage("CollidedWith", other);

        GameObject.Destroy(gameObject);
    }

    // Update is called once per frame
    void Update ()
    {
        
    }
}
