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
    public float spawnPointDeviation = 3.0f;

    public Transform playerTransform;
    public PrefabWithProbability[] enemyPrototypes;
    public int desiredNumberOfNpcs = 30;
    public float spawnInterval = 5.0f;

    public int deadNpcsCounter = 0;

    [Header("game difficulty influence")]
    public float minHitPointsFactor = 0.5f;
    public float maxHitPointsFactor = 2.0f;
    private float hpFactor => Mathf.Lerp(minHitPointsFactor, maxHitPointsFactor, Preferences.GameSettings.instance.difficulty);

    private HashSet<Enemy> knownNpc = new HashSet<Enemy>();
    private float timeSinceLastSpawn;

    private List<SpawnPoint> knownSpawnPoints = new List<SpawnPoint>();

    private void UpdateSpawnPointList()
    {
        GetComponentsInChildren(knownSpawnPoints);
    }

    //TODO: optimization ?
    private SpawnPoint SelectSpawnPoint(Vector3 relativePosition) => MathExtension.RandomWeightedSelect(knownSpawnPoints.Select(x => (x, x.SpawnProbability(relativePosition))));
    private static GameObject SelectPrefab(PrefabWithProbability[] prototypes) => MathExtension.RandomWeightedSelect(prototypes.Select(x => (x.prefab, x.probabilityWeight))); //TODO: implicit cast PrefabWithProbability -> (a, b)

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
        UpdateSpawnPointList();

        Debug.Log($"Manager {gameObject.name}: HP factor is {hpFactor}");
    }

    public void SpawnNpc(GameObject prefab, Vector3 position, Quaternion rotation)
    {
        if (prefab is null)
            throw new System.ArgumentNullException(nameof(prefab));

        var instance = Instantiate(prefab, position, rotation, transform); //TODO: probably no need to set self as parent
        timeSinceLastSpawn = 0.0f;

        //TODO: seems it should be responsibility of another object?
        var playerStats = instance.GetComponent<PlayerStats>();
        if (playerStats)
            playerStats.hitPoints = (playerStats.maxHitPoints *= hpFactor);

        Debug.Log($"New NPC spawned! {knownNpc.Count+1}/{desiredNumberOfNpcs}");
    }

    public void AutoSpawn()
    {
        var spawnPoint = SelectSpawnPoint(playerTransform.position);
        var prefabs = spawnPoint.overridePrefabs != null && spawnPoint.overridePrefabs.Length > 0 ? spawnPoint.overridePrefabs : enemyPrototypes;
        if (prefabs is null || prefabs.Length < 0)
            return;

        var prefab = SelectPrefab(prefabs);
        if (!prefab)
            return;

        spawnPoint.remainingSpawns--;
        SpawnNpc(prefab, spawnPoint.spawnPosition, spawnPoint.transform.rotation);
    }

    private void Update()
    {
        if (knownNpc.Count < desiredNumberOfNpcs && timeSinceLastSpawn >= spawnInterval)
            AutoSpawn();

        timeSinceLastSpawn += Time.deltaTime;
    }
}
