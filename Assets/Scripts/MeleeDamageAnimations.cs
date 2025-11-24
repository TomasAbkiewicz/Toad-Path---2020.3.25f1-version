using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MeleeDamageAnimations : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform player;
    public LayerMask whatIsGround, whatIsPlayer;

    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange = 10f;

    public float timeBetweenAttacks = 1f;
    float attackCooldown = 0f;

    public int damage = 10;
    public float attackRadius = 2f;
    public Transform attackPoint;

    public float sightRange = 10f;
    public float attackRange = 2f;

    public Animator anim;

    private void Awake()
    {
        player = GameObject.Find("PlayerObj").transform;
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        float distance = Vector3.Distance(transform.position, player.position);

        // Reducir cooldown del ataque
        if (attackCooldown > 0)
            attackCooldown -= Time.deltaTime;

        if (distance > sightRange)
        {
            Patroling();
        }
        else if (distance > attackRange)
        {
            ChasePlayer();
        }
        else
        {
            AttackPlayer();
        }
    }

    private void Patroling()
    {
        anim.SetBool("isWalking", true);

        if (!walkPointSet) SearchWalkPoint();

        if (walkPointSet)
            agent.SetDestination(walkPoint);

        // Llegó al punto
        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        if (distanceToWalkPoint.magnitude < 1f)
            walkPointSet = false;
    }

    private void SearchWalkPoint()
    {
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX,
                                transform.position.y,
                                transform.position.z + randomZ);

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
        anim.SetBool("isWalking", false);

        agent.ResetPath();
        transform.LookAt(player);

        // Solo atacar si cooldown terminó
        if (attackCooldown <= 0f)
        {
            anim.SetTrigger("Attack");
            attackCooldown = timeBetweenAttacks;
        }
    }

    // LLAMADA DESDE LA ANIMACIÓN EXACTAMENTE EN EL FRAME DEL GOLPE
    public void DealDamage()
    {
        Collider[] hitPlayers = Physics.OverlapSphere(attackPoint.position, attackRadius, whatIsPlayer);

        foreach (Collider hit in hitPlayers)
        {
            if (hit.CompareTag("Player"))
            {
                DataPlayer playerData = hit.GetComponent<DataPlayer>();
                if (playerData != null)
                {
                    playerData.healthPlayer -= damage;
                    Debug.Log("Golpeaste al jugador! daño: " + damage);
                }
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);

        if (attackPoint != null)
        {
            Gizmos.color = Color.magenta;
            Gizmos.DrawWireSphere(attackPoint.position, attackRadius);
        }
    }
}