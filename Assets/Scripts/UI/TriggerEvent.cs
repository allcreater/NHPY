using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerEvent : MonoBehaviour
{
    public UnityEvent onTrigger;
    public string activatorTag = "Player";

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag != activatorTag)
            return;

        if (onTrigger != null)
            onTrigger.Invoke();
    }
}
