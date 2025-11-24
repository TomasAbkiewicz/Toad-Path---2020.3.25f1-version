using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RobotArana : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform player;
    public LayerMask whatIsGround, whatIsPlayer;

    public Transform shootingPoint1;   // 🔥 PRIMER PUNTO DE DISPARO
    public Transform shootingPoint2;   // 🔥 SEGUNDO PUNTO DE DISPARO
    int nextShotPoint = 1;             // Para alternar entre 1 y 2

    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    public float timeBetweenAttacks;
    bool alreadyAttacked;
    public GameObject projectile;

    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;

    private void Awake()
    {
        player = GameObject.Find("PlayerObj").transform;
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        if (!playerInSightRange && !playerInAttackRange) Patroling();
        if (playerInSightRange && !playerInAttackRange) ChasePlayer();
        if (playerInAttackRange && playerInSightRange) AttackPlayer();
    }

    private void Patroling()
    {
        if (!walkPointSet) SearchWalkPoint();

        if (walkPointSet)
            agent.SetDestination(walkPoint);

        if ((transform.position - walkPoint).magnitude < 1f)
            walkPointSet = false;
    }

    private void SearchWalkPoint()
    {
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y,
                                transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
            walkPointSet = true;
    }

    private void ChasePlayer()
    {
        agent.SetDestination(player.position);
    }

    private void AttackPlayer()
    {
        agent.SetDestination(transform.position);
        transform.LookAt(player);

        if (!alreadyAttacked)
        {
            // --------------------------------------------------------------------------------------------------------
            // 🔥 DESDE ACÁ DISPARA INTERCALANDO ENTRE shootingPoint1 Y shootingPoint2
            // --------------------------------------------------------------------------------------------------------

            Transform sp = nextShotPoint == 1 ? shootingPoint1 : shootingPoint2;

            if (sp != null)
            {
                Rigidbody rb = Instantiate(projectile, sp.position, sp.rotation).GetComponent<Rigidbody>();
                rb.AddForce(sp.forward * 32f, ForceMode.Impulse);
                rb.AddForce(sp.up * 8f, ForceMode.Impulse);
            }

            // Cambiamos el próximo punto
            nextShotPoint = (nextShotPoint == 1) ? 2 : 1;

            // --------------------------------------------------------------------------------------------------------

            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }

    private void ResetAttack()
    {
        alreadyAttacked = false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);

        if (shootingPoint1 != null)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(shootingPoint1.position, 0.1f);
        }

        if (shootingPoint2 != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(shootingPoint2.position, 0.1f);
        }
    }
}