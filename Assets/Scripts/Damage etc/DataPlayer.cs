using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DataPlayer : MonoBehaviour
{
    public int healthPlayer = 100;
    public Slider visualHealth;

    private void Update()
    {
        if (visualHealth != null)
            visualHealth.value = healthPlayer;

        if (healthPlayer <= 0)
        {
            Debug.Log("GAME OVER");
            Destroy(gameObject);
        }
    }

    public void TakeDamage(int damage)
    {
        healthPlayer -= damage;
        Debug.Log("Player recibió " + damage + " de daño. Vida actual: " + healthPlayer);
    }
}
