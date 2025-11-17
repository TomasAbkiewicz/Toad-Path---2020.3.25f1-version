using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UpgradeManager : MonoBehaviour
{
    [Header("Panel")]
    public GameObject upgradePanel;

    [Header("Stats")]
    public PlayerHealth playerHealth;
    public PlayerDamage playerDamage;
    public PlayerMovementDashing playerMovement;

    [Header("Config")]
    public float upgradePercent = 0.15f;
    public string nextSceneName = "LVL_2";

    bool isChoosing = false;

    void Update()
    {
        if (!isChoosing) return;

        if (Input.GetKeyDown(KeyCode.Alpha1)) ChooseHealth();
        if (Input.GetKeyDown(KeyCode.Alpha2)) ChooseDamage();
        if (Input.GetKeyDown(KeyCode.Alpha3)) ChooseSpeed();
    }

    public void ShowUpgrades()
    {
        upgradePanel.SetActive(true);
        isChoosing = true;

        // 🔓 Liberar cursor
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        // Pausar movimiento
        playerMovement.canMove = false;
    }

    void CloseUpgrades()
    {
        upgradePanel.SetActive(false);
        isChoosing = false;

        // 🔒 Volver a bloquear cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        // Restaurar movimiento
        playerMovement.canMove = true;

        // Cargar siguiente nivel
        SceneManager.LoadScene(nextSceneName);
    }

    // ------------------------------
    //   OPCIONES DE UPGRADE
    // ------------------------------

    void ChooseHealth()
    {
        playerHealth.maxHealth += playerHealth.maxHealth * upgradePercent;
        playerHealth.currentHealth = playerHealth.maxHealth;
        CloseUpgrades();
    }

    void ChooseDamage()
    {
        playerDamage.damage += playerDamage.damage * upgradePercent;
        CloseUpgrades();
    }

    void ChooseSpeed()
    {
        playerMovement.moveSpeed += playerMovement.moveSpeed * upgradePercent;
        CloseUpgrades();
    }
}
