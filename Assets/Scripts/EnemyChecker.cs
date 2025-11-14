using UnityEngine;

public class EnemyChecker : MonoBehaviour
{
    [Header("Configuración")]
    public string enemyTag = "whatIsEnemy";   // Tag de los enemigos
    public GameObject congratsPanel;          // Panel UI a activar

    private bool panelShown = false;

    void Start()
    {
        if (congratsPanel != null)
            congratsPanel.SetActive(false);
    }

    void Update()
    {
        // Si ya se mostró el panel, no revises más
        if (panelShown) return;

        // Busca los enemigos con ese tag
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);

        // Si no hay enemigos, mostrar panel
        if (enemies.Length == 0)
        {
            panelShown = true;

            if (congratsPanel != null)
                congratsPanel.SetActive(true);
        }
    }
}
