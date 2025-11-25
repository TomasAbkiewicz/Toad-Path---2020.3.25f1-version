using UnityEngine;

public class UpgradeTriggerSpawn : MonoBehaviour
{
    public GameObject upgradePrefab1;
    public GameObject upgradePrefab2;
    public GameObject upgradePrefab3;

    public string playerTag = "whatIsPlayer";

    private bool spawned = false;

    private void OnTriggerEnter(Collider other)
    {
        if (spawned) return;
        if (!other.CompareTag(playerTag)) return;

        spawned = true;

        Vector3 pos = transform.position;

        float spread = 2f;

        Instantiate(upgradePrefab1, pos + new Vector3(-spread, 0, 0), Quaternion.identity);
        Instantiate(upgradePrefab2, pos, Quaternion.identity);
        Instantiate(upgradePrefab3, pos + new Vector3(spread, 0, 0), Quaternion.identity);

        Debug.Log("Mejoras spawn por trigger.");
    }
}
