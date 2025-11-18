using UnityEngine;
using UnityEngine.UI;

public class DataEnemy : MonoBehaviour
{
    public int enemyHealth;
    public Slider enemyHealthBar;

    private void Update()
    {
        enemyHealthBar.value = enemyHealth;

        if (enemyHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Collider col = GetComponent<Collider>();
        EnemyChecker.Instance.NotifyEnemyDied(transform.position, col);
        Destroy(gameObject);
    }
}
