using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataEnemyMelee : MonoBehaviour
{
    public int healthEnemy = 50;  // vida del enemigo
    public int damage = 10;       // daño que inflige al jugador

    // referencia al jugador
    private DataPlayer player;

    private void Start()
    {
        // buscamos al Player en la escena
        player = FindObjectOfType<DataPlayer>();
    }

    private void Update()
    {
        // si la vida llega a 0 o menos => enemigo muere
        if (healthEnemy <= 0)
        {
            Debug.Log("Enemy muerto");
            Destroy(gameObject);
        }
    }

    // --- enemigo recibe daño ---
    public void TakeDamage(int dmg)
    {
        healthEnemy -= dmg;
        Debug.Log("Enemy recibió " + dmg + " de daño. Vida actual: " + healthEnemy);
    }

    // --- ataque melee contra el player ---
    public void MeleeAttackOnPlayer()
    {
        if (player != null)
        {
            player.TakeDamage(damage);
            Debug.Log("Enemy golpeó al jugador e hizo " + damage + " de daño.");
        }
    }
}
