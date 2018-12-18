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
    public GameObject[] bagPrefab;

    public float bagReactionSpeedAverage = 6.0f;
    public float bagReactionSpeedDispersion = 1.0f;

    private List<GameObject> bags = new List<GameObject>();

    public bool AddBag_1()
    {
        var bagIndex = bags.Count + 1;
        var bagName = $"Bag #{bagIndex}";
        var bagSocketName = $"BagSocket #{bagIndex}";
        
        var socket = transform.FindChildByRecursion(bagSocketName);
        if (socket)
        {
            var newBag = GameObject.Instantiate(bagPrefab[0], null);
            newBag.name = bagName;
            var flyAroundComponent = newBag.GetComponent<FlyAround>();
            flyAroundComponent.target = socket;
            flyAroundComponent.reactionSpeed = Random.Range(bagReactionSpeedAverage - bagReactionSpeedDispersion, bagReactionSpeedAverage + bagReactionSpeedDispersion);

            bags.Add(newBag);

            return true;
        }

        return false;
    }
    public bool AddBag_2()
    {
        var bagIndex = bags.Count + 1;
        var bagName = $"Bag #{bagIndex}";
        var bagSocketName = $"BagSocket #{bagIndex}";

        var socket = transform.FindChildByRecursion(bagSocketName);
        if (socket)
        {
            var newBag = GameObject.Instantiate(bagPrefab[1], null);
            newBag.name = bagName;
            var flyAroundComponent = newBag.GetComponent<FlyAround>();
            flyAroundComponent.target = socket;
            flyAroundComponent.reactionSpeed = Random.Range(bagReactionSpeedAverage - bagReactionSpeedDispersion, bagReactionSpeedAverage + bagReactionSpeedDispersion);

            bags.Add(newBag);

            return true;
        }

        return false;
    }

    void Start()
    {
        AddBag_1();
        AddBag_1();
        AddBag_2();
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
