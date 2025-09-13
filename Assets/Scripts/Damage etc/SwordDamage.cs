using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordDamage : MonoBehaviour
{
    public int swordDamage;
    public GameObject Enemy;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "whatIsEnemy")
        {
            Enemy.GetComponent<DataEnemy>().enemyHealth -= swordDamage;
        }
    }

}
