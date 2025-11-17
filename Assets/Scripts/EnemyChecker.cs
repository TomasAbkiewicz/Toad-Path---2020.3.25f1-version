using UnityEngine;

public class EnemyChecker : MonoBehaviour
{

    public string enemyTag = "whatIsEnemy";   
    public GameObject congratsPanel;          

    private bool panelShown = false;

    void Start()
    {
        if (congratsPanel != null)
            congratsPanel.SetActive(false);
    }

    void Update()
    {
        if (panelShown) return;


        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);

        if (enemies.Length == 0)
        {
            panelShown = true;

            if (congratsPanel != null)
                congratsPanel.SetActive(true);
        }
    }
}
