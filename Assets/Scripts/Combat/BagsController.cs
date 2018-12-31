using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(PlayerStats))]
public class BagsController : MonoBehaviour
{
    public GameObject[] bagPrefab;

    public float bagReactionSpeedAverage = 6.0f;
    public float bagReactionSpeedDispersion = 1.0f;

    private List<GameObject> bags = new List<GameObject>();

    public GameObject AddBag(int preafabindex )
    {
        if (preafabindex < 0 || preafabindex >= bagPrefab.Length)
            throw new System.IndexOutOfRangeException("preafabindex");

        var bagIndex = bags.Count + 1;
        var bagName = $"Bag #{bagIndex}";
        var bagSocketName = $"BagSocket #{bagIndex}";
        
        var socket = transform.FindChildByRecursion(bagSocketName);
        if (socket)
        {
            var newBag = GameObject.Instantiate(bagPrefab[preafabindex], null);
            newBag.name = bagName;
            var flyAroundComponent = newBag.GetComponent<FlyAround>();
            flyAroundComponent.target = socket;
            flyAroundComponent.reactionSpeed = Random.Range(bagReactionSpeedAverage - bagReactionSpeedDispersion, bagReactionSpeedAverage + bagReactionSpeedDispersion);

            bags.Add(newBag);

            return newBag;
        }

        return null;
    }
    
    private void Start()
    {
        AddBag(0);
    }

    private void Update()
    {
        bags.RemoveAll(x => !x.activeInHierarchy);
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
