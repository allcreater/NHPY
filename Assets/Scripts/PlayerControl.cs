using System.Linq;
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

    private WeaponsController weaponsController;
    private SortedSet<string> discoveredWeapons = new SortedSet<string>();

    private int selectedSecondaryGroupIndex = 0;

    private static readonly string mainGroup = "Main";
    private static readonly string alternateGroup = "Alternate";

    void Awake ()
    {
        weaponsController = playerPawn.GetComponent<WeaponsController>();
    }

    void RediscoverWeapon()
    {
        discoveredWeapons.Clear();
        foreach (var bucket in weaponsController.buckets)
            foreach (var weapon in weaponsController.GetWeapons(bucket))
            {
                var shooter = weapon.GetComponentInChildren<Shooting>();
                if (shooter)
                    discoveredWeapons.UnionWith(shooter.weaponType);
            }

        discoveredWeapons.Remove(mainGroup);
        discoveredWeapons.Remove(alternateGroup);
    }

    void Update ()
    {
        if (Mathf.Approximately(Time.timeScale, 0.0f))
            return;

        RediscoverWeapon();

        var movement = Vector3.ClampMagnitude(new Vector3(CrossPlatformInputManager.GetAxisRaw("Horizontal"), 0.0f, CrossPlatformInputManager.GetAxisRaw("Vertical")), 1.0f);
        MoveToParams moveToParams = new MoveToParams(movement, CrossPlatformInputManager.GetButton("Run"));

        Vector3? lookToDirection = null;
        ShootToParams shootToParameters = null;

        var joysticDir = new Vector3(CrossPlatformInputManager.GetAxisRaw("RightJoystickX"), 0.0f, CrossPlatformInputManager.GetAxisRaw("RightJoystickY"));
        if (joysticDir.magnitude > 0.01)
            lookToDirection = joysticDir;

        var camera = GetComponent<Camera>();
        var mouseScreenPos = CrossPlatformInputManager.mousePosition;

        //secondary weapon selection
        var direction = (int)Mathf.Clamp(CrossPlatformInputManager.GetAxisRaw("SwitchWeapon"), -1, 1);
        if (direction != 0)
            selectedSecondaryGroupIndex++;

        selectedSecondaryGroupIndex %= Mathf.Max(discoveredWeapons.Count, 1);
        var secondaryGroup = discoveredWeapons.Skip(selectedSecondaryGroupIndex).FirstOrDefault();

        if (direction != 0)
            Debug.Log("Weapon:" + secondaryGroup);

        if (Physics.Raycast(camera.ScreenPointToRay(mouseScreenPos), out var hitInfo))
        {
            lookToDirection = (hitInfo.point - playerPawn.transform.position).normalized;

            var attacks = new List<string>();
            if (CrossPlatformInputManager.GetAxisRaw("Fire1") > 0.5f)
                attacks.Add(mainGroup);
            if (CrossPlatformInputManager.GetButtonDown("Fire2"))
                attacks.Add(alternateGroup);
            if (CrossPlatformInputManager.GetAxisRaw("Fire3") > 0.5f)
                attacks.Add(secondaryGroup);

            if (attacks.Count > 0)
            {
                var target = (hitInfo.collider.tag == "Enemy") ? hitInfo.collider.transform : null;
                shootToParameters = new ShootToParams(hitInfo.point, target, attacks.ToArray());
            }
        }

        //TODO: switch to new message system
        //TODO: decrutch this weapon search
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
