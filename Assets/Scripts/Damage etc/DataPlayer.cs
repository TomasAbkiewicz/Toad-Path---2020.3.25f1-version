using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DataPlayer : MonoBehaviour
{
    [Header("Player Stats")]
    public int healthPlayer = 100;

    [Header("UI")]
    public Slider visualHealth;
    public GameObject deathPanel; // ← asignalo en el inspector

    [Header("Death Settings")]
    public float timeBeforeRestart = 5f; // tiempo antes de volver al menú
    public int mainMenuSceneIndex = 0;   // índice de la escena a cargar

    private bool isDead = false;

    private void Update()
    {
        // Actualizar slider
        if (visualHealth != null)
            visualHealth.value = healthPlayer;

        // Detectar muerte
        if (!isDead && healthPlayer <= 0)
        {
            isDead = true;
            StartCoroutine(HandleDeath());
        }
    }

    public void TakeDamage(int damage)
    {
        healthPlayer -= damage;
        Debug.Log("Player recibió " + damage + " de daño. Vida actual: " + healthPlayer);
    }

    private IEnumerator HandleDeath()
    {
        Debug.Log("GAME OVER");

        // Mostrar panel
        if (deathPanel != null)
            deathPanel.SetActive(true);

        // Esperar X segundos
        yield return new WaitForSeconds(timeBeforeRestart);

        // Volver al menú
        SceneManager.LoadScene(mainMenuSceneIndex);
    }
}
