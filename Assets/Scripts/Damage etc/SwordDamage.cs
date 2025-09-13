using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordDamage : MonoBehaviour
{
    public int swordDamage = 10;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("whatIsEnemy"))
        {
            DataEnemy enemy = other.GetComponent<DataEnemy>();
            if (enemy != null)
            {
                enemy.enemyHealth -= swordDamage;
            }
        }
    }

}
