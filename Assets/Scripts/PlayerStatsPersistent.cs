using UnityEngine;

public class PlayerStatsPersistent : MonoBehaviour
{
    public static PlayerStatsPersistent instance;

    [Header("Base Stats")]
    public int baseHealth = 100;
    public int baseDamage = 10;
    public float baseMoveSpeed = 7f; // valor inicial para walkSpeed

    [Header("Current (modified) Stats")]
    public int currentHealth;
    public int currentDamage;
    public float currentMoveSpeed;

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);

        // Inicializa los stats si es la primera vez
        if (currentHealth == 0 && currentDamage == 0 && currentMoveSpeed == 0f)
            ResetStats();
    }

    public void ResetStats()
    {
        currentHealth = baseHealth;
        currentDamage = baseDamage;
        currentMoveSpeed = baseMoveSpeed;
    }

    // Aplicar upgrades porcentuales
    public void UpgradeDamagePercent(float percent)
    {
        currentDamage += Mathf.RoundToInt(currentDamage * percent);
    }

    public void UpgradeHealthPercent(float percent)
    {
        currentHealth += Mathf.RoundToInt(currentHealth * percent);
    }

    public void UpgradeMoveSpeedPercent(float percent)
    {
        currentMoveSpeed *= (1f + percent);
    }
}
