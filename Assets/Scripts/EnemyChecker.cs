using UnityEngine;

public class EnemyChecker : MonoBehaviour
{
    public GameObject panel;  // Poné acá tu panel desde el Inspector

    void Update()
    {
        // Busca TODOS los objetos con la tag Enemy
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        // Si no hay enemigos, mostrar el panel
        if (enemies.Length == 0)
        {
            panel.SetActive(true);
        }
    }
}
