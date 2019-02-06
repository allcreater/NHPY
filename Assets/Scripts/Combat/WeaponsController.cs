using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class StartWeapon
{
    public GameObject weaponPrefab;
    public string category;
}

//TODO: think about better linking with weapons
[RequireComponent(typeof(PlayerStats))]
public class WeaponsController : MonoBehaviour, IDeathHandler
{
    public StartWeapon[] startWeapons;
    public string[] categoriesThatBringsAdditionalLife;

    public float bagReactionSpeedAverage = 6.0f;
    public float bagReactionSpeedDispersion = 1.0f;

    private int socketsUpdateFrame = 0;
    private List<BagSocket> sockets = new List<BagSocket>(); //It's very unlikely that list will be bigger than hundred elements, so that it more efficient than dictionaries etc
    public IReadOnlyList<BagSocket> Sockets
    {
        get
        {
            if (socketsUpdateFrame != Time.frameCount)
            {
                GetComponentsInChildren(sockets);
                socketsUpdateFrame = Time.frameCount;
            }
            return sockets;
        }
    }

    public IEnumerable<BagSocket> Weapons => Sockets.Where(x => x.weapon);
    public IEnumerable<BagSocket> VacantPlaces => Sockets.Where(x => !x.weapon);

    public BagSocket GetFreeSocket(string socketType) => Sockets.Where(x => String.IsNullOrEmpty(x.socketType) || x.socketType == socketType).FirstOrDefault(x => !x.weapon);

    //TODO: maybe better to move it somewhere else
    public BagSocket InstantiateWeapon(string socketType, GameObject weaponPrefab)
    {
        var socket = GetFreeSocket(socketType);
        if (socket)
        {
            var weapon = Instantiate(weaponPrefab);

            var flyAroundComponent = weapon.GetComponent<FlyAround>();
            flyAroundComponent.target = socket.transform;
            flyAroundComponent.reactionSpeed = UnityEngine.Random.Range(bagReactionSpeedAverage - bagReactionSpeedDispersion, bagReactionSpeedAverage + bagReactionSpeedDispersion);

            socket.weapon = weapon;
            return socket;
        }

        return null;
    }
    
    public bool RemoveWeapon(GameObject weapon, bool destroyObject = true)
    {
        var socket = Sockets.FirstOrDefault(x => x.weapon == weapon);
        if (socket)
        {
            socket.weapon = null;
            if (destroyObject)
                GameObject.Destroy(weapon);
        }

        return socket;
    }

    private void Start()
    {
        //TODO: move into helper component
        foreach (var startWeapon in startWeapons)
            InstantiateWeapon(startWeapon.category, startWeapon.weaponPrefab);
    }

    private void Update()
    {
    }

    float? IDeathHandler.OnDeathDoor()
    {
        var socket = Sockets.Where(x => categoriesThatBringsAdditionalLife.Contains(x.socketType)).FirstOrDefault(x => x.weapon); //TODO: priority what category will be reduced first
        if (socket)
        {
            RemoveWeapon(socket.weapon);

            return float.MaxValue;
        }

        return null;
    }

    void IDeathHandler.OnDeath()
    {
        //Do nothing
    }
}
