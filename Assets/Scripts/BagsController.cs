using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

internal static class Ext
{
    internal static Transform FindChildByRecursion(this Transform aParent, string aName)
    {
        if (aParent == null) return null;
        var result = aParent.Find(aName);
        if (result != null)
            return result;
        foreach (Transform child in aParent)
        {
            result = child.FindChildByRecursion(aName);
            if (result != null)
                return result;
        }
        return null;
    }
}

[RequireComponent(typeof(PlayerStats))]
public class BagsController : MonoBehaviour
{
    public GameObject bagPrefab;

    private List<GameObject> bags = new List<GameObject>();

    public bool AddBag()
    {
        var bagIndex = bags.Count + 1;
        var bagName = $"Bag #{bagIndex}";
        var bagSocketName = $"BagSocket #{bagIndex}";
        
        var socket = transform.FindChildByRecursion(bagSocketName);
        if (socket)
        {
            var newBag = GameObject.Instantiate(bagPrefab, null);
            newBag.name = bagName;
            newBag.GetComponent<FlyAround>().target = socket;

            bags.Add(newBag);

            return true;
        }

        return false;
    }

    void Start()
    {
        AddBag();
        AddBag();
        AddBag();
    }

    void NoMoreHP(DeathToken token)
    {
        if (bags.Count >= 1)
        {
            var index = bags.Count - 1;
            GameObject.Destroy(bags[index]);
            bags.RemoveAt(index);


            token.ReviveHitPoints = float.MaxValue;
        }
        else
        {
            SceneManager.LoadScene("GameOver"); //TODO: remove инкостыляцию
        }
    }
}
