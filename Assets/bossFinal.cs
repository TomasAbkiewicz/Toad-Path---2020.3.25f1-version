using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class bossFinal : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform player;
    public LayerMask whatIsGround, whatIsPlayer;

    public Transform shootingPoint;

    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    public float timeBetweenAttacks;
    bool alreadyAttacked;
    public GameObject projectile;

    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;

    // 🔥 Animations
    public Animator anim;

    private void Awake()
    {
        player = GameObject.Find("PlayerObj").transform;
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
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
        anim.SetBool("isWalking", true);

        if (!walkPointSet) SearchWalkPoint();

        if (walkPointSet)
            agent.SetDestination(walkPoint);

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        if (distanceToWalkPoint.magnitude < 1f)
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
        anim.SetBool("isWalking", true);
        agent.SetDestination(player.position);
    }

    private void AttackPlayer()
    {
        // 🔥 Stop walking animation
        anim.SetBool("isWalking", false);

        // 🔥 Trigger shooting animation
        anim.SetTrigger("Shot");

        agent.SetDestination(transform.position);
        transform.LookAt(player);

        if (!alreadyAttacked)
        {
            Vector3 spawnPos = shootingPoint != null ? shootingPoint.position : transform.position;
            Quaternion spawnRot = shootingPoint != null ? shootingPoint.rotation : transform.rotation;
            Vector3 forward = shootingPoint != null ? shootingPoint.forward : transform.forward;
            Vector3 up = shootingPoint != null ? shootingPoint.up : transform.up;

            Rigidbody rb = Instantiate(projectile, spawnPos, spawnRot).GetComponent<Rigidbody>();
            rb.AddForce(forward * 32f, ForceMode.Impulse);
            rb.AddForce(up * 8f, ForceMode.Impulse);

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
    }
}