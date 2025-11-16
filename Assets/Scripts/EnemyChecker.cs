using UnityEngine;

public class EnemyChecker : MonoBehaviour
{
<<<<<<< HEAD
    public GameObject panel;  // Poné acá tu panel desde el Inspector

    void Update()
    {
        // Busca TODOS los objetos con la tag Enemy
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        // Si no hay enemigos, mostrar el panel
        if (enemies.Length == 0)
        {
            panel.SetActive(true);
=======
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
>>>>>>> parent of 5404bc3 (remove changes)
        }
    }
}
