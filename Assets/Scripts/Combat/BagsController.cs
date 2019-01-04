using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(PlayerStats))]
public class BagsController : MonoBehaviour
{
    public GameObject[] bagPrefab;

    [Space]
    public bool addNeededBags;
    public int bagIndexIfTrue = 3;
    public int amountBags = 2;

    public float bagReactionSpeedAverage = 6.0f;
    public float bagReactionSpeedDispersion = 1.0f;

    private List<GameObject> bags = new List<GameObject>();
    private List<GameObject> rockets = new List<GameObject>();

    public GameObject AddBag(int preafabindex )
    {
        if (preafabindex < 0 || preafabindex >= bagPrefab.Length)
            throw new System.IndexOutOfRangeException("preafabindex");

        var bagIndex = 0;
        var bagName = "";
        var bagSocketName = "";

        //Говнокод от Андрея
        if (preafabindex == 3)
        {
            bagIndex = rockets.Count + 1;
            bagName = $"Bag #{bagIndex}";
            bagSocketName = $"RocketSocket #{bagIndex}";
            var newRocket = AddSocket(bagSocketName, bagName, preafabindex);
            if (newRocket != null)
            {
                rockets.Add(newRocket);
                return newRocket;
            }
        }
        else
        {
            bagIndex = bags.Count + 1;
            bagName = $"Bag #{bagIndex}";
            bagSocketName = $"BagSocket #{bagIndex}";
            var newBag = AddSocket(bagSocketName, bagName, preafabindex);
            if(newBag != null)
            {
                bags.Add(newBag);
                return newBag;
            }                
        }           

        return null;
    }

    private GameObject AddSocket(string bagSocketName, string bagName, int preafabindex)
    {
        var socket = transform.FindChildByRecursion(bagSocketName);
        if (socket)
        {
            var newWeapon = GameObject.Instantiate(bagPrefab[preafabindex], null);
            newWeapon.name = bagName;
            var flyAroundComponent = newWeapon.GetComponent<FlyAround>();
            flyAroundComponent.target = socket;
            flyAroundComponent.reactionSpeed = Random.Range(bagReactionSpeedAverage - bagReactionSpeedDispersion, bagReactionSpeedAverage + bagReactionSpeedDispersion);           

            return newWeapon;
        }
        return null;
    }
    
    private void Start()
    {
        AddBag(0);
        if (addNeededBags == true)
            while (amountBags > 0)
            {
                AddBag(bagIndexIfTrue);
                amountBags--;
            }            
    }

    private void Update()
    {
        bags.RemoveAll(x => !x.activeInHierarchy);
        rockets.RemoveAll(x => !x.activeInHierarchy);
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
