using UnityEngine;

public class EnemyChecker : MonoBehaviour
{
    public string enemyTag = "whatIsEnemy";
    [Header("Prefabs de upgrades (3)")]
    public GameObject[] upgradePrefabs; // asigná exactamente 3
    [Header("Spawn points (3 Transforms)")]
    public Transform[] spawnPoints;

    private bool spawned = false;

    void Update()
    {
        if (spawned) return;

        if (GameObject.FindGameObjectsWithTag(enemyTag).Length == 0)
        {
            spawned = true;
            SpawnUpgrades();
        }
    }

    void SpawnUpgrades()
    {
        if (upgradePrefabs.Length < 3 || spawnPoints.Length < 3)
        {
            Debug.LogWarning("Asigná 3 prefabs y 3 spawnPoints en EnemyChecker.");
            return;
        }

        for (int i = 0; i < 3; i++)
        {
            Instantiate(upgradePrefabs[i], spawnPoints[i].position, Quaternion.identity);
        }
    }
}
