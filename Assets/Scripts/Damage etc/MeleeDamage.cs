using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeDamage : MonoBehaviour
{
    [Header("Daño del enemigo melee")]
    public int meleeDamage = 15;
    public float attackCooldown = 1.2f;

    private bool alreadyAttacked = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("whatIsPlayer") && !alreadyAttacked)
        {
            DataPlayer playerData = other.GetComponent<DataPlayer>();
            if (playerData != null)
            {
                playerData.TakeDamage(meleeDamage);
                Debug.Log("Melee enemy hit player!");
            }

            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), attackCooldown);
        }
    }

    private void ResetAttack()
    {
        alreadyAttacked = false;
    }
}
