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

        // ⭐⭐ Cambio importante acá ⭐⭐
        if (!other.CompareTag("whatIsPlayer")) return;

        var stats = PlayerStatsPersistent.instance;
        if (stats == null)
        {
            Debug.LogError("PlayerStatsPersistent not found!");
            return;
        }

        switch (type)
        {
            case UpgradeType.MoreDamage:
                stats.UpgradeDamagePercent(percent);
                break;

            case UpgradeType.MoreHealth:
                stats.UpgradeHealthPercent(percent);
                break;

            case UpgradeType.MoreMoveSpeed:
                stats.UpgradeMoveSpeedPercent(percent);
                break;
        }

        used = true;

        if (!string.IsNullOrEmpty(nextSceneName))
        {
            SceneManager.LoadScene(nextSceneName);
        }
    }
}
