using UnityEngine;
using UnityEngine.SceneManagement;

public class UpgradePickUp : MonoBehaviour
{
    public enum UpgradeType { MoreDamage, MoreHealth, MoreMoveSpeed }

    [Header("Tipo de mejora")]
    public UpgradeType type = UpgradeType.MoreDamage;

    [Header("Porcentaje (ej: 0.15 = 15%)")]
    public float percent = 0.15f;

    [Header("Índice de la escena siguiente en Build Settings")]
    public int nextSceneIndex = -1; // -1 = no cambiar escena

    private bool used = false;

    private void OnTriggerEnter(Collider other)
    {
        if (used) return;

        if (!other.CompareTag("whatIsPlayer")) return;

        var stats = PlayerStatsPersistent.instance;
        if (stats == null)
        {
            Debug.LogError("PlayerStatsPersistent not found!");
            return;
        }

        // Aplicar mejora
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

        // Cambiar de escena SOLO si configuraste un índice válido
        if (nextSceneIndex >= 0)
        {
            SceneManager.LoadScene(nextSceneIndex);
            DynamicGI.UpdateEnvironment();
        }
    }
}
