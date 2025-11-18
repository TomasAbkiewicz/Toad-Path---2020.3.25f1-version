using UnityEngine;

public class UpgradeSpawnEffect : MonoBehaviour
{
    public float floatHeight = 0.5f;
    public float floatSpeed = 2f;
    public float rotateSpeed = 50f;

    void Update()
    {
        // Rotar lentamente
        transform.Rotate(Vector3.up * rotateSpeed * Time.deltaTime);

        // Flotar arriba y abajo
        float y = Mathf.Sin(Time.time * floatSpeed) * floatHeight;
        transform.localPosition = new Vector3(transform.localPosition.x, y, transform.localPosition.z);
    }
}
