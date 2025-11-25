using UnityEngine;

public class EnemyChecker : MonoBehaviour
{
    public static EnemyChecker Instance;

    public GameObject upgradePrefab1;
    public GameObject upgradePrefab2;
    public GameObject upgradePrefab3;

    public int enemiesRemaining;
    private bool upgradesSpawned = false;

    void Awake()
    {
        Instance = this;
    }

    public void NotifyEnemyDied(Vector3 enemyPos, Collider enemyCollider)
    {
        enemiesRemaining--;

        if (enemiesRemaining <= 0 && !upgradesSpawned)
        {
            upgradesSpawned = true;

            Vector3 spawnPos = enemyPos;
            spawnPos.y += 1f; // un pequeño lift

            SpawnUpgradesAtPosition(spawnPos);
        }
    }

    private void SpawnUpgradesAtPosition(Vector3 pos)
    {
        float spread = 2f;

        Instantiate(upgradePrefab1, pos + new Vector3(-spread, 0, 0), Quaternion.identity);
        Instantiate(upgradePrefab2, pos, Quaternion.identity);
        Instantiate(upgradePrefab3, pos + new Vector3(spread, 0, 0), Quaternion.identity);
    }
}
