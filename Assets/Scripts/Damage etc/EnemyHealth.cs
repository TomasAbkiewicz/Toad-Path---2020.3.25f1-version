using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    [Header("Enemy Stats")]
    public int maxHealth = 50;
    public int currentHealth;

    [Header("Optional Health Bar")]
    public Slider healthSlider;   // Asignar SÓLO si el enemigo tiene barra

    void Start()
    {
        currentHealth = maxHealth;

        // Si tiene slider, inicializarlo
        if (healthSlider != null)
        {
            healthSlider.maxValue = maxHealth;
            healthSlider.value = maxHealth;
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        // Actualizar slider
        if (healthSlider != null)
        {
            healthSlider.value = currentHealth;
        }

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        // Obtener el collider del enemigo (puede ser null, chequeamos)
        Collider col = GetComponent<Collider>();

        if (EnemyChecker.Instance != null)
        {
            // Llamamos a la versión nueva que recibe la posición y el collider
            EnemyChecker.Instance.NotifyEnemyDied(transform.position, col);
        }
        else
        {
            Debug.LogWarning("EnemyChecker.Instance es null al morir un enemigo.");
        }

        Destroy(gameObject);
    }
}
