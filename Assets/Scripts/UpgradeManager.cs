using UnityEngine;
using UnityEngine.SceneManagement;

public class UpgradeManager : MonoBehaviour
{
    public enum UpgradeType { MoreDamage, MoreHealth, MoreSpeed }
    public UpgradeType type;

    public void ApplyUpgrade()
    {
        PlayerStatsPersistent stats = PlayerStatsPersistent.instance;

        switch (type)
        {
            case UpgradeType.MoreDamage:
                stats.currentDamage = Mathf.RoundToInt(stats.currentDamage * 1.10f);
                break;

            case UpgradeType.MoreHealth:
                stats.currentHealth = Mathf.RoundToInt(stats.currentHealth * 1.20f);
                break;

            case UpgradeType.MoreSpeed:
                stats.currentMoveSpeed *= 1.15f;
                break;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        ApplyUpgrade();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
