using UnityEngine;
using UnityEngine.SceneManagement;

public class UpgradePickUp : MonoBehaviour
{
    public enum UpgradeType { MoreDamage, MoreHealth, MoreMoveSpeed }

    public UpgradeType type = UpgradeType.MoreDamage;
    public float percent = 0.15f;
    public int nextSceneIndex = -1;

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

        if (nextSceneIndex >= 0)
        {
            SceneManager.LoadScene(nextSceneIndex);
            DynamicGI.UpdateEnvironment();
        }
    }
}
