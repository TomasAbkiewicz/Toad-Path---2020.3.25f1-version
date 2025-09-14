using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileDamage : MonoBehaviour
{
    public int damage;
    public GameObject Player;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("whatIsPlayer"))
        {
            Player.GetComponent<DataPlayer>().healthPlayer -= damage;
            Debug.Log("Hit Player");
            Destroy(gameObject); // destruir proyectil al tocar al jugador
        }

        if (other.CompareTag("whatIsEnemy"))
        {
            Debug.Log("Hit Enemy"); 

        }
    }

}
