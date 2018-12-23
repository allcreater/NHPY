using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnitySampleAssets.CrossPlatformInput;

public class MoveToParams
{
    public Vector3 direction { get; }
    public bool isRun { get; }

    public MoveToParams(Vector3 dir, bool run)
    {
        direction = dir;
        isRun = run;
    }
}

[RequireComponent(typeof(Camera))]
public class PlayerControl : MonoBehaviour
{
    public GameObject playerPawn;

    // Use this for initialization
    void Awake ()
    {

    }

// Update is called once per frame
    void Update ()
    {
        var movement = Vector3.ClampMagnitude(new Vector3(CrossPlatformInputManager.GetAxisRaw("Horizontal"), 0.0f, CrossPlatformInputManager.GetAxisRaw("Vertical")), 1.0f);
        MoveToParams moveToParams = new MoveToParams(movement, CrossPlatformInputManager.GetButton("Run"));

        Vector3? lookToDirection = null;
        ShootToParams shootToParameters = null;

        var joysticDir = new Vector3(CrossPlatformInputManager.GetAxisRaw("RightJoystickX"), 0.0f, CrossPlatformInputManager.GetAxisRaw("RightJoystickY"));
        if (joysticDir.magnitude > 0.01)
            lookToDirection = joysticDir;

        var camera = GetComponent<Camera>();
        var mouseScreenPos = CrossPlatformInputManager.mousePosition;

        if (Physics.Raycast(camera.ScreenPointToRay(mouseScreenPos), out var hitInfo))
        {
            lookToDirection = (hitInfo.point - playerPawn.transform.position).normalized;

            var attacks = new List<string>();
            if (CrossPlatformInputManager.GetAxisRaw("Fire1") > 0.5f)
                attacks.Add("Main");
            if (CrossPlatformInputManager.GetButtonDown("Fire2"))
                attacks.Add("Alternate");

            if (attacks.Count > 0)
            {
                var target = (hitInfo.collider.tag == "Enemy") ? hitInfo.collider.transform : null;
                shootToParameters = new ShootToParams(hitInfo.point, target, attacks.ToArray());
            }
        }

        foreach (var controlledObject in GameObject.FindGameObjectsWithTag("Player"))
        {
            controlledObject.BroadcastMessage("MoveTo", moveToParams, SendMessageOptions.DontRequireReceiver);

            if (lookToDirection?.magnitude > 0.01)
                controlledObject.BroadcastMessage("LookTo", lookToDirection.Value, SendMessageOptions.DontRequireReceiver);

            if (shootToParameters != null)
                controlledObject.BroadcastMessage("ShootTo", shootToParameters, SendMessageOptions.DontRequireReceiver);
        }
    }
}
