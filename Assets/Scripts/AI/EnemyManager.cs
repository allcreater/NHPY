using System.Linq;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct PrefabWithProbability
{
    public GameObject prefab;
    public float probabilityWeight;
}

public class EnemyManager : MonoBehaviour
{
    public float spawnIfDisance = 0;
    public float spawnPointDeviation = 3.0f;

    public Transform playerTransform;
    public PrefabWithProbability[] enemyPrototypes;
    public Transform spawnPointsCollection;
    public int desiredNumberOfNpcs = 30;
    public float spawnInterval = 5.0f;

    public int deadNpcsCounter = 0;

    [Header("game difficulty influence")]
    public float minHitPointsFactor = 0.5f;
    public float maxHitPointsFactor = 2.0f;
    private float hpFactor => Mathf.Lerp(minHitPointsFactor, maxHitPointsFactor, Preferences.GameSettings.instance.difficulty);

    private HashSet<Enemy> knownNpc = new HashSet<Enemy>();
    private float timeSinceLastSpawn;

    private Transform SelectSpawnPoint() => spawnPointsCollection.Find($"Spawnpoint #{Random.Range(0, spawnPointsCollection.childCount)}");

    private GameObject SelectEnemyPrefab() => MathExtension.RandomWeightedSelect(enemyPrototypes.Select(x => (x.prefab, x.probabilityWeight)));

    public void RegisterNpc(Enemy npc) => knownNpc.Add(npc);
    public void UnregisterNpc(Enemy npc)
    
   
    {
        knownNpc.Remove(npc);
        deadNpcsCounter++;

        if (npc.GetComponent<PlayerStats>().hitPoints <= 0.0f)
            ScoreManager.instance.scores++;
    }

    // Start is called before the first frame update
    private void Start()
    {

        
        Debug.Log($"Manager {gameObject.name}: HP factor is {hpFactor}");
    }

    private void PlayerDist()
    {
        
    }

    private void SpawnNpc(GameObject prefab, Transform spawnPoint)
    {
        float playerDist = Vector3.Distance(spawnPoint.position, playerTransform.position);
        if (playerDist < spawnIfDisance)
            return;

        var randomPoint = Random.insideUnitCircle * spawnPointDeviation;
        var posSpawnPoint = new Vector3(randomPoint.x, 0, randomPoint.y);
        spawnPoint.position = spawnPoint.position + posSpawnPoint;
        if (prefab is null)
            return;

        var instance = Instantiate(prefab, spawnPoint.position, spawnPoint.rotation, spawnPointsCollection);
        timeSinceLastSpawn = 0.0f;

        //TODO: remove?
        var playerStats = instance.GetComponent<PlayerStats>();
        if (playerStats)
            playerStats.hitPoints = (playerStats.maxHitPoints *= hpFactor);

        Debug.Log($"New NPC spawned! {knownNpc.Count}/{desiredNumberOfNpcs}");

        spawnPoint.position = spawnPoint.position - posSpawnPoint;
    }

    // Update is called once per frame
    private void Update()
    {
        if (knownNpc.Count < desiredNumberOfNpcs && timeSinceLastSpawn >= spawnInterval)
            SpawnNpc(SelectEnemyPrefab(), SelectSpawnPoint());

        timeSinceLastSpawn += Time.deltaTime;
    }
}
