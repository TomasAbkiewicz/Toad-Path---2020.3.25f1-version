using UnityEngine;
using System.Collections;
using System.Collections.Generic;   // ← IMPORTANTE para HashSet

public class SwordDamage : MonoBehaviour
{
    public int damage;
    private bool isAttacking = false;
    private HashSet<EnemyHealth> enemiesHit = new HashSet<EnemyHealth>();

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            StartCoroutine(AttackWindow());
        }
    }

    private IEnumerator AttackWindow()
    {
        isAttacking = true;
        enemiesHit.Clear(); // Evita golpear a un mismo enemigo varias veces por ataque
        yield return new WaitForSeconds(0.3f);
        isAttacking = false;
    }

    void OnTriggerEnter(Collider other)
    {
        if (!isAttacking) return;

        EnemyHealth health = other.GetComponent<EnemyHealth>();
        if (health != null && !enemiesHit.Contains(health))
        {
            health.TakeDamage(damage);
            enemiesHit.Add(health);
        }
    }
}
