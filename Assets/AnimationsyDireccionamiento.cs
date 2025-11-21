using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class AnimationsyDireccionamiento : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform player;
    public LayerMask whatIsGround, whatIsPlayer;


    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;


    public float timeBetweenAttacks;
    bool alreadyAttacked;
    public int damage = 10;
    public float attackRadius = 2f;
    public Transform attackPoint;


    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;


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


        if (distance > sightRange)
        {
            // Muy lejos → patrulla
            Patroling();
        }
        else if (distance > attackRange)
        {
            // Lo ve pero todavía no llega a pegar → persigue
            ChasePlayer();
        }
        else
        {
            // Ya está lo suficientemente cerca → ataca
            AttackPlayer();
        }
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
        agent.ResetPath();
        transform.LookAt(player);


        anim.SetBool("isWalking", false);


        if (!alreadyAttacked)
        {
            // Solo disparo la ANIMACIÓN de ataque
            anim.SetTrigger("Attack");


            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }


    // 👊 ESTA función se llama desde la animación justo cuando el golpe conecta
    public void DealDamage()
    {
        Collider[] hitPlayers = Physics.OverlapSphere(attackPoint.position, attackRadius, whatIsPlayer);


        foreach (Collider hit in hitPlayers)
        {
            if (hit.CompareTag("Player")) // usa el tag real del jugador
            {
                DataPlayer playerData = hit.GetComponent<DataPlayer>();
                if (playerData != null)
                {
                    playerData.healthPlayer -= damage;
                    Debug.Log("Golpeaste al jugador, daño: " + damage);
                }
            }
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


        if (attackPoint != null)
        {
            Gizmos.color = Color.magenta;
            Gizmos.DrawWireSphere(attackPoint.position, attackRadius);
        }
    }
}




