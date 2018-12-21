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
    public PrefabWithProbability[] enemyPrototypes;
    public Transform spawnPointsCollection;
    public int desiredNumberOfNpcs = 30;
    public float spawnInterval = 5.0f;

    private HashSet<Enemy> knownNpc = new HashSet<Enemy>();
    private float timeSinceLastSpawn;

    private Transform SelectSpawnPoint() => spawnPointsCollection.GetChild(Random.Range(0, spawnPointsCollection.childCount));

    private GameObject SelectEnemyPrefab() => MathExtension.RandomWeightedSelect(enemyPrototypes.Select(x => (x.prefab, x.probabilityWeight)));

    public void RegisterNpc(Enemy npc) => knownNpc.Add(npc);
    public void UnregisterNpc(Enemy npc) => knownNpc.Remove(npc);

    // Start is called before the first frame update
    private void Start()
    {

    }

    private void SpawnNpc(GameObject prefab, Transform spawnPoint)
    {
        if (prefab is null)
            return;

        var instance = Instantiate(prefab, spawnPoint.position, spawnPoint.rotation, spawnPointsCollection);
        timeSinceLastSpawn = 0.0f;

        Debug.Log($"New NPC spawned! {knownNpc.Count}/{desiredNumberOfNpcs}");
    }

    // Update is called once per frame
    private void Update()
    {
        if (knownNpc.Count < desiredNumberOfNpcs && timeSinceLastSpawn >= spawnInterval)
            SpawnNpc(SelectEnemyPrefab(), SelectSpawnPoint());

        timeSinceLastSpawn += Time.deltaTime;
    }
}
