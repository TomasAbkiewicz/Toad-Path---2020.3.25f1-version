using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordDamage : MonoBehaviour
{
    public int damage;
    private bool isAttacking = false;
    public GameObject Enemy;

    void Update()
    {
        // Si el jugador hace click, activa el modo ataque por un corto tiempo
        if (Input.GetMouseButtonDown(0))
        {
            StartCoroutine(AttackWindow());
        }
    }

    private IEnumerator AttackWindow()
    {
        isAttacking = true;
        yield return new WaitForSeconds(0.3f); // ventana de tiempo donde la espada puede dañar
        isAttacking = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isAttacking && other.CompareTag("whatIsEnemy"))
        {
            DataEnemy enemy = other.GetComponent<DataEnemy>();
            if (enemy != null)
            {
                enemy.enemyHealth -= damage;
                Debug.Log("Enemy Hit");
            }
        }
    }


}
