using UnityEngine;

public class PlayerStatsLoader : MonoBehaviour
{
    void Start()
    {
        var stats = PlayerStatsPersistent.instance;
        if (stats == null)
        {
            Debug.LogError("PlayerStatsPersistent not found in scene. Add a GameManager with PlayerStatsPersistent.");
            return;
        }

        // VIDA
        DataPlayer dataPlayer = GetComponent<DataPlayer>();
        if (dataPlayer != null)
        {
            dataPlayer.healthPlayer = stats.currentHealth;
            if (dataPlayer.visualHealth != null)
            {
                dataPlayer.visualHealth.maxValue = stats.currentHealth;
                dataPlayer.visualHealth.value = stats.currentHealth;
            }
        }

        // DAÑO (SwordDamage suele estar en un hijo; usamos GetComponentInChildren)
        SwordDamage sword = GetComponentInChildren<SwordDamage>();
        if (sword != null)
        {
            sword.damage = stats.currentDamage;
        }

        // MOVIMIENTO (PlayerMovementDashing debe estar en el mismo GameObject)
        PlayerMovementDashing movement = GetComponent<PlayerMovementDashing>();
        if (movement != null)
        {
            movement.walkSpeed = stats.currentMoveSpeed;
            movement.sprintSpeed = stats.currentMoveSpeed * 1.5f; // ratio que usás por defecto
        }
    }
}
