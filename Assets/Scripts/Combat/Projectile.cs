using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;

public class Projectile : MonoBehaviour 
{
    public string[] tags;
    public GameObject hitEffect;
    public float startDelay = 0.0f;
    public float explodeAfter = 0.0f;

    private Coroutine delayedEffect;

    // Use this for initialization
    void Start () 
    {
        if (explodeAfter > 0.0f)
        {
            Debug.Log(gameObject.name);
            StartCoroutine(ExecuteAfterTime(explodeAfter, null, hitEffect));
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!tags.Contains(other.gameObject.tag) || delayedEffect != null)
            return;
        
        if (hitEffect)
            delayedEffect = StartCoroutine(ExecuteAfterTime(startDelay, other, hitEffect));
    }

    IEnumerator ExecuteAfterTime(float delay, Collider other, GameObject hitEffect)
    {
        if (!Mathf.Approximately(delay, 0.0f))
            yield return new WaitForSeconds(delay);

        var effectObject = GameObject.Instantiate(hitEffect, transform.position, transform.rotation, null);
        effectObject.SendMessage("CollidedWith", other, SendMessageOptions.DontRequireReceiver);

        GameObject.Destroy(gameObject);
    }


    // Update is called once per frame
    void Update ()
    {
        
    }
}
