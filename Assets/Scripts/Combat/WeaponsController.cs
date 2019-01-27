﻿using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    private Dictionary<string, List<GameObject>> weapons = new Dictionary<string, List<GameObject>>();

    //TODO: is there better way?
    public IReadOnlyCollection<string> buckets => weapons.Keys;
    public IReadOnlyList<GameObject> GetWeapons(string bucket) => weapons.TryGetValue(bucket, out var result) ? result : null;


    public GameObject AddWeapon(string socketType, GameObject weapon)
    {
        var list = weapons.GetOrCreate(socketType);

        var socket = GetComponentsInChildren<BagSocket>().Where(x => x.socketType == socketType).Skip(list.Count).FirstOrDefault();
        if (socket)
        {
            int index = list.Count + 1;

            var newWeapon = GameObject.Instantiate(weapon, null);
            newWeapon.name = $"Weapon [{socketType}] {index}]";

            var flyAroundComponent = newWeapon.GetComponent<FlyAround>();
            flyAroundComponent.target = socket.transform;
            flyAroundComponent.reactionSpeed = UnityEngine.Random.Range(bagReactionSpeedAverage - bagReactionSpeedDispersion, bagReactionSpeedAverage + bagReactionSpeedDispersion);

            list.Add(newWeapon);

            return newWeapon;
        }

        return null;
    }
    
    private void Start()
    {
        foreach (var startWeapon in startWeapons)
            AddWeapon(startWeapon.category, startWeapon.weaponPrefab);
    }

    private void Update()
    {
        foreach (var bag in weapons)
            bag.Value.RemoveAll(x => !x.activeInHierarchy); //TODO: probably weapon should be removed another way
    }

    float? IDeathHandler.OnDeathDoor()
    {
        var selectedList = weapons.FirstOrDefault(x => categoriesThatBringsAdditionalLife.Contains(x.Key) && x.Value.Count > 0).Value; //TODO: priority what category will be reduced first
        if (selectedList != null)
        {
            var index = selectedList.Count - 1;
            GameObject.Destroy(selectedList[index]);
            selectedList.RemoveAt(index);

            return float.MaxValue;
        }

        return null;
    }

    void IDeathHandler.OnDeath()
    {
        //Do nothing
    }
}
