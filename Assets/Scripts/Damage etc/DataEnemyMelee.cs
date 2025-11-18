using UnityEngine;

public class DataEnemyMelee : MonoBehaviour
{
    public int healthEnemy = 50;
    public int damage = 10;

    private DataPlayer player;

    private void Start()
    {
        player = FindObjectOfType<DataPlayer>();
    }

    private void Update()
    {
        if (healthEnemy <= 0)
        {
            Die();
        }
    }

    public void TakeDamage(int dmg)
    {
        healthEnemy -= dmg;
    }

    public void MeleeAttackOnPlayer()
    {
        if (player != null)
        {
            player.TakeDamage(damage);
        }
    }

    void Die()
    {
        Collider col = GetComponent<Collider>();
        EnemyChecker.Instance.NotifyEnemyDied(transform.position, col);
        Destroy(gameObject);
    }
}
