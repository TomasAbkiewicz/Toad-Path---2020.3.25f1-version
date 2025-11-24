using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileDamage : MonoBehaviour
{
    public int damage = 10;
    private DataPlayer playerData; // referencia al script de vida

    private void Start()
    {
        // Buscar automáticamente al jugador
        GameObject playerObj = GameObject.Find("Player");

        if (playerObj != null)
            playerData = playerObj.GetComponent<DataPlayer>();
        else
            Debug.LogError("error");
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("whatIsPlayer"))
        {
            if (playerData != null)
            {
                playerData.healthPlayer -= damage;
                Debug.Log(" Player recibió daño: -" + damage);
            }

            Destroy(gameObject); 
        }


        if (other.CompareTag("whatIsEnemy"))
        {
            Debug.Log("Proyectil golpeó enemigo (pero no hace daño)");
        }
    }
}
