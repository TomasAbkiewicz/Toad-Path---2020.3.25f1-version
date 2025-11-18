using UnityEngine;

public class EnemyChecker : MonoBehaviour
{
    public static EnemyChecker Instance;

    public GameObject upgradePrefab1;
    public GameObject upgradePrefab2;
    public GameObject upgradePrefab3;

    public int enemiesRemaining;
    private bool upgradesSpawned = false;

    private void Awake()
    {
        Instance = this;
    }

    public void NotifyEnemyDied(Vector3 enemyPos, Collider enemyCollider)
    {
        enemiesRemaining--;

        if (enemiesRemaining <= 0 && !upgradesSpawned)
        {
            upgradesSpawned = true;

            // Siempre va a funcionar:
            // Toma la posición real del enemigo
            Vector3 spawnPos = enemyPos;

            // Le sumamos la altura del collider para que aparezca exactamente arriba
            float height = enemyCollider.bounds.size.y;

            spawnPos.y += height * 0.8f; // Ajustable. 1f = exacto arriba

            SpawnUpgradesAtPosition(spawnPos);
        }
    }

    private void SpawnUpgradesAtPosition(Vector3 pos)
    {
        float spread = 2f;

        Instantiate(upgradePrefab1, pos + new Vector3(-spread, 0, 0), Quaternion.identity);
        Instantiate(upgradePrefab2, pos, Quaternion.identity);
        Instantiate(upgradePrefab3, pos + new Vector3(spread, 0, 0), Quaternion.identity);

        Debug.Log("Upgrades spawn en posición corregida.");
    }
}
