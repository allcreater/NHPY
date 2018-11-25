using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnitySampleAssets.CrossPlatformInput;

[RequireComponent(typeof(Camera))]
public class PlayerControl : MonoBehaviour
{
    public GameObject controlledObject;

    // Use this for initialization
    void Start ()
    {

    }

// Update is called once per frame
    void Update ()
    {
        var movement = Vector3.ClampMagnitude(new Vector3(CrossPlatformInputManager.GetAxisRaw("Horizontal"), 0.0f, CrossPlatformInputManager.GetAxisRaw("Vertical")), 1.0f);
        controlledObject.BroadcastMessage("MoveTo", movement, SendMessageOptions.DontRequireReceiver);

        var camera = GetComponent<Camera>();

        if (CrossPlatformInputManager.GetAxisRaw("Fire1") > 0.5f)
        {
            var mouseDir = new Vector3(CrossPlatformInputManager.GetAxisRaw("Mouse X"), 0.0f, CrossPlatformInputManager.GetAxisRaw("Mouse Y"));
            //var shootDirection = (camera.ScreenToWorldPoint(mousePos) - controlledObject.transform.position).normalized;
            Debug.Log(mouseDir.normalized);
            controlledObject.BroadcastMessage("ShootTo", mouseDir);
            
        }
    }
}
