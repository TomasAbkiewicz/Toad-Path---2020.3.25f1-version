using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MeleeEnemyAI : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform player;

    public LayerMask whatIsGround, whatIsPlayer;

    // Patrullaje
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange = 5f;

    // Rango de detección
    public float sightRange = 10f;
    public float attackRange = 2f;
    public bool playerInSightRange, playerInAttackRange;

    private DataEnemyMelee dataEnemy; //  ahora apunta al script correcto

    private void Awake()
    {
        player = GameObject.Find("PlayerObj")?.transform;
        agent = GetComponent<NavMeshAgent>();
        dataEnemy = GetComponent<DataEnemyMelee>(); //  correcto

        if (agent != null)
            agent.stoppingDistance = Mathf.Max(0.5f, attackRange * 0.9f);
    }

    private void Update()
    {
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        if (!playerInSightRange && !playerInAttackRange) Patroling();
        else if (playerInSightRange && !playerInAttackRange) ChasePlayer();
        else if (playerInSightRange && playerInAttackRange) AttackPlayer();
    }

    private void Patroling()
    {
        if (!walkPointSet) SearchWalkPoint();

        if (walkPointSet)
            agent.SetDestination(walkPoint);

        if (Vector3.Distance(transform.position, walkPoint) < 1f)
            walkPointSet = false;
    }

    private void SearchWalkPoint()
    {
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
            walkPointSet = true;
    }

    private void ChasePlayer()
    {
        if (agent != null && player != null)
            agent.SetDestination(player.position);
    }

    private void AttackPlayer()
    {
        if (agent != null) agent.SetDestination(transform.position);
        if (player != null) transform.LookAt(player);

        float dist = Vector3.Distance(transform.position, player.position);
        if (dist <= attackRange + 0.2f)
        {
            dataEnemy.MeleeAttackOnPlayer(); //  ahora llama al método del DataEnemyMelee
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }
}
