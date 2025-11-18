using UnityEngine;
using UnityEngine.SceneManagement;

public class UpgradePickUp : MonoBehaviour
{
    public enum UpgradeType { MoreDamage, MoreHealth, MoreMoveSpeed }

    [Header("Tipo de mejora")]
    public UpgradeType type = UpgradeType.MoreDamage;

    [Header("Porcentaje (ej: 0.15 = 15%)")]
    public float percent = 0.15f;

    [Header("Nombre de la escena siguiente (opcional)")]
    public string nextSceneName = "LVL 2";

    private bool used = false;

    private void OnTriggerEnter(Collider other)
    {
        if (used) return;
        if (!other.CompareTag("Player")) return;

        var stats = PlayerStatsPersistent.instance;
        if (stats == null)
        {
            Debug.LogError("PlayerStatsPersistent not found!");
            return;
        }

        // Aplicar la mejora
        switch (type)
        {
            case UpgradeType.MoreDamage:
                stats.UpgradeDamagePercent(percent);
                Debug.Log("Applied damage upgrade: " + (percent * 100f) + "%");
                break;

            case UpgradeType.MoreHealth:
                stats.UpgradeHealthPercent(percent);
                Debug.Log("Applied health upgrade: " + (percent * 100f) + "%");
                break;

            case UpgradeType.MoreMoveSpeed:
                stats.UpgradeMoveSpeedPercent(percent);
                Debug.Log("Applied move speed upgrade: " + (percent * 100f) + "%");
                break;
        }

        used = true;

        // Opcional: cargar la siguiente escena
        if (!string.IsNullOrEmpty(nextSceneName))
        {
            SceneManager.LoadScene(nextSceneName);
        }
    }
}
