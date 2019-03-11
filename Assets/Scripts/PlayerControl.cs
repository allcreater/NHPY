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

    private int selectedSecondaryGroupIndex = 0;

    private static readonly string mainGroupName = "Bags";
    private static readonly string alternateGroupName = "Alternate";
    private static readonly string[] primaryGroupNames = { mainGroupName, alternateGroupName };

    void Awake ()
    {
        weaponsController = playerPawn.GetComponent<WeaponsController>();
    }

    void Update ()
    {
        if (Mathf.Approximately(Time.timeScale, 0.0f))
            return;

        //Movement
        var movement = Vector3.ClampMagnitude(new Vector3(CrossPlatformInputManager.GetAxisRaw("Horizontal"), 0.0f, CrossPlatformInputManager.GetAxisRaw("Vertical")), 1.0f);
        MoveToParams moveToParams = new MoveToParams(movement, CrossPlatformInputManager.GetButton("Run"));
        playerPawn.BroadcastMessage("MoveTo", moveToParams, SendMessageOptions.DontRequireReceiver);//TODO: switch to new message system ?


        //Targeting
        Vector3? lookToDirection = null;
        ShootToParams shootToParameters = null;

        var joysticDir = new Vector3(CrossPlatformInputManager.GetAxisRaw("RightJoystickX"), 0.0f, CrossPlatformInputManager.GetAxisRaw("RightJoystickY"));
        if (joysticDir.magnitude > 0.01)
            lookToDirection = joysticDir;

        var camera = GetComponent<Camera>();
        var mouseScreenPos = CrossPlatformInputManager.mousePosition;


        var weaponGroups = weaponsController.Weapons.GroupBy(x => x.socketType).ToDictionary(x => x.Key);

        //secondary weapon selection
        var direction = (int)Mathf.Clamp(CrossPlatformInputManager.GetAxisRaw("SwitchWeapon"), -1, 1);
        if (direction != 0)
            selectedSecondaryGroupIndex++;

        var secondaryGroups = weaponGroups.Where(x => !primaryGroupNames.Contains(x.Key)).Select(x => x.Value).ToArray();
        if (secondaryGroups.Length > 0)
        {
            selectedSecondaryGroupIndex %= secondaryGroups.Length;
        }


        if (Physics.Raycast(camera.ScreenPointToRay(mouseScreenPos), out var hitInfo))
        {
            lookToDirection = (hitInfo.point - playerPawn.transform.position).normalized;

            var target = (hitInfo.collider.tag == "Enemy") ? hitInfo.collider.transform : null;
            shootToParameters = new ShootToParams(hitInfo.point, target, weaponsController); //TODO: think about sending weaponsController

            //TODO: move some grouping functionality into socket/controller?
            bool Shoot(IGrouping<string, WeaponSocket> weaponGroup) => weaponGroup.SelectMany(x => x.weapon.GetComponentsInChildren<IShootable>()).Any(x => x.ShootTo(shootToParameters));

            //Right now weapon is subdivided by groups(setted by socket types), secondary group is selectable
            if (CrossPlatformInputManager.GetAxisRaw("Fire1") > 0.5f && weaponGroups.TryGetValue(mainGroupName, out var mainGroup))
                Shoot(mainGroup);
            if (CrossPlatformInputManager.GetButtonDown("Fire2") && weaponGroups.TryGetValue(alternateGroupName, out var alternateGroup))
                Shoot(alternateGroup);
            if (CrossPlatformInputManager.GetAxisRaw("Fire3") > 0.5f && secondaryGroups.Length > 0)
                Shoot(secondaryGroups[selectedSecondaryGroupIndex]);
        }

        foreach (var weaponSocket in weaponsController.Weapons)
        {
            if (lookToDirection?.magnitude > 0.01)
                weaponSocket.weapon.BroadcastMessage("LookTo", lookToDirection.Value, SendMessageOptions.DontRequireReceiver);
        }
    }
}
