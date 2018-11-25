using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnitySampleAssets.CrossPlatformInput;

[RequireComponent(typeof(Camera))]
public class PlayerControl : MonoBehaviour
{
    public Vector3 actualDirection;

    // Use this for initialization
    void Start ()
    {

    }

// Update is called once per frame
    void Update ()
    {
        foreach (var controlledObject in GameObject.FindGameObjectsWithTag("Player"))
        {
            //TODO: remove костыль
            var movement = Vector3.ClampMagnitude(new Vector3(CrossPlatformInputManager.GetAxisRaw("Horizontal"), 0.0f, CrossPlatformInputManager.GetAxisRaw("Vertical")), 1.0f);
            controlledObject.BroadcastMessage("MoveTo", movement, SendMessageOptions.DontRequireReceiver);

            var mouseDir = new Vector3(CrossPlatformInputManager.GetAxisRaw("RightJoystickX"), 0.0f, CrossPlatformInputManager.GetAxisRaw("RightJoystickY"));
            if (mouseDir.magnitude > 0.01)
            {
                controlledObject.BroadcastMessage("LookTo", actualDirection, SendMessageOptions.DontRequireReceiver);
                actualDirection = mouseDir.normalized;
            }

            if (CrossPlatformInputManager.GetAxisRaw("Fire1") > 0.5f)
            {
                //var shootDirection = (camera.ScreenToWorldPoint(mousePos) - controlledObject.transform.position).normalized;
                controlledObject.BroadcastMessage("ShootTo", actualDirection, SendMessageOptions.DontRequireReceiver);

            }
        }
    }
}
