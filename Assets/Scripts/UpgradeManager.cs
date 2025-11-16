using UnityEngine;
using UnityEngine.SceneManagement;

public class UpgradeManager : MonoBehaviour
{
    [Header("Panel de mejoras")]
    public GameObject congratsPanel;

    [Header("Player Stats")]
    public DataPlayer playerHealth;
    public SwordDamage playerDamage;
    public PlayerMovementDashing movement;

    [Header("Config")]
    public float upgradePercent = 0.15f;
    public string nextSceneName = "NextLevel";


    private void Start()
    {
        LoadUpgrades();
    }


    public void ShowCongratsPanel()
    {
        congratsPanel.SetActive(true);

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        PlayerCam cam = FindObjectOfType<PlayerCam>();
        if (cam != null)
            cam.enabled = false;
        Debug.Log("DESBLOQUEANDO CURSOR");
        Debug.Log("Cursor.lockState = " + Cursor.lockState);
        Debug.Log("Cursor.visible = " + Cursor.visible);
    }


    // ====================================================
    //   OPCIONES DE MEJORA
    // ====================================================
    public void UpgradeHealth()
    {
        playerHealth.healthPlayer = Mathf.RoundToInt(playerHealth.healthPlayer * (1f + upgradePercent));
        SaveUpgrades();
        RestoreCamera();
        LoadNextScene();
    }

    public void UpgradeDamage()
    {
        playerDamage.damage = Mathf.RoundToInt(playerDamage.damage * (1f + upgradePercent));
        SaveUpgrades();
        RestoreCamera();
        LoadNextScene();
    }

    public void UpgradeSpeed()
    {
        movement.walkSpeed *= (1f + upgradePercent);
        movement.sprintSpeed *= (1f + upgradePercent);
        SaveUpgrades();
        RestoreCamera();
        LoadNextScene();
    }


    // ====================================================
    // GUARDAR Y CARGAR
    // ====================================================
    void SaveUpgrades()
    {
        PlayerPrefs.SetInt("PlayerHealth", playerHealth.healthPlayer);
        PlayerPrefs.SetInt("PlayerDamage", playerDamage.damage);
        PlayerPrefs.SetFloat("WalkSpeed", movement.walkSpeed);
        PlayerPrefs.SetFloat("SprintSpeed", movement.sprintSpeed);
        PlayerPrefs.Save();
    }

    void LoadUpgrades()
    {
        if (PlayerPrefs.HasKey("PlayerHealth"))
            playerHealth.healthPlayer = PlayerPrefs.GetInt("PlayerHealth");

        if (PlayerPrefs.HasKey("PlayerDamage"))
            playerDamage.damage = PlayerPrefs.GetInt("PlayerDamage");

        if (PlayerPrefs.HasKey("WalkSpeed"))
            movement.walkSpeed = PlayerPrefs.GetFloat("WalkSpeed");

        if (PlayerPrefs.HasKey("SprintSpeed"))
            movement.sprintSpeed = PlayerPrefs.GetFloat("SprintSpeed");
    }


    // ====================================================
    // RESTAURAR CÁMARA
    // ====================================================
    void RestoreCamera()
    {
        PlayerCam cam = FindObjectOfType<PlayerCam>();
        if (cam != null)
        {
            cam.enabled = true;
        }
    }


    // ====================================================
    // CAMBIAR DE ESCENA
    // ====================================================
    void LoadNextScene()
    {
        SceneManager.LoadScene(nextSceneName);
    }
}
