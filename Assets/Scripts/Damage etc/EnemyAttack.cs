using UnityEngine;
using UnityEngine.AI;

public class EnemyAttack : MonoBehaviour
{
    private NavMeshAgent agent;

    [Header("Ataque")]
    public GameObject projectile;
    public Transform shootPoint; // NUEVO: lugar desde donde dispara
    public float timeBetweenAttacks = 1f;
    private bool alreadyAttacked = false;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    public void AttackPlayer(Transform player)
    {
        // Detener movimiento
        agent.SetDestination(transform.position);

        // Mirar al jugador
        transform.LookAt(player);

        // Disparar si no atacó recientemente
        if (!alreadyAttacked)
        {
            ShootProjectile();
            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }

    private void ShootProjectile()
    {
        if (projectile == null || shootPoint == null)
        {
            Debug.LogWarning(" Falta asignar el projectile o el shootPoint en el inspector");
            return;
        }

        Rigidbody rb = Instantiate(projectile, shootPoint.position, shootPoint.rotation).GetComponent<Rigidbody>();

        rb.AddForce(transform.forward * 32f, ForceMode.Impulse);
        rb.AddForce(transform.up * 8f, ForceMode.Impulse);
    }

    private void ResetAttack()
    {
        alreadyAttacked = false;
    }
}
