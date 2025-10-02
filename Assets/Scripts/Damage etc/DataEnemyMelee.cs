using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DataEnemyMelee : MonoBehaviour
{
    [Header("Stats del Enemigo")]
    public int enemyHealth = 100;
    public int meleeDamage = 10;
    public float attackCooldown = 1.5f;
    private bool alreadyAttacked = false;

    [Header("UI")]
    public Slider enemyHealthBar;

    private Transform player;
    private DataPlayer playerData;

    private void Start()
    {
        player = GameObject.Find("PlayerObj").transform;
        playerData = player.GetComponent<DataPlayer>();
    }

    private void Update()
    {
        if (enemyHealthBar != null)
            enemyHealthBar.value = enemyHealth;

        if (enemyHealth <= 0)
        {
            Debug.Log("ENEMY DEFEATED");
            Destroy(gameObject);
        }
    }

    public void MeleeAttack()
    {
        if (!alreadyAttacked && playerData != null)
        {
            Debug.Log("Enemy hits player with melee!");
            playerData.TakeDamage(meleeDamage);

            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), attackCooldown);
        }
    }

    private void ResetAttack()
    {
        alreadyAttacked = false;
    }

    public void TakeDamage(int damage)
    {
        enemyHealth -= damage;
    }
}
