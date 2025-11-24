using UnityEngine;
using UnityEngine.AI;

public class RobotVerdeScript : MonoBehaviour
{
    [Header("References")]
    public NavMeshAgent agent;
    public Transform player;
    public Animator anim;
    public Transform shootingPoint;

    [Header("Ranges")]
    public float sightRange = 15f;
    public float attackRange = 10f;
    public LayerMask whatIsGround, whatIsPlayer;

    [Header("Patrol")]
    private Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange = 10f;

    [Header("Attack")]
    public GameObject projectilePrefab;
    public float timeBetweenAttacks = 2f;
    bool alreadyAttacked;

    [Header("Projectile Settings")]
    public float projectileSpeed = 15f;  

    bool playerInSightRange;
    bool playerInAttackRange;

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

        if (!playerInSightRange && !playerInAttackRange) Patrol();
        if (playerInSightRange && !playerInAttackRange) Chase();
        if (playerInSightRange && playerInAttackRange) Attack();
    }

    private void Patrol()
    {
        anim.SetBool("isWalking", true);

        if (!walkPointSet) SearchWalkPoint();
        agent.SetDestination(walkPoint);

        if (Vector3.Distance(transform.position, walkPoint) < 1f)
            walkPointSet = false;
    }

    private void SearchWalkPoint()
    {
        float rx = Random.Range(-walkPointRange, walkPointRange);
        float rz = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + rx, transform.position.y, transform.position.z + rz);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
            walkPointSet = true;
    }

    private void Chase()
    {
        anim.SetBool("isWalking", true);
        agent.SetDestination(player.position);
    }

    private void Attack()
    {
        anim.SetBool("isWalking", false);
        agent.SetDestination(transform.position);

        transform.LookAt(player.position);

        if (!alreadyAttacked)
        {
            anim.SetTrigger("Shot");

            Invoke(nameof(ShootProjectile), 0.15f);

            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }

    private void ShootProjectile()
    {
        GameObject proj = Instantiate(projectilePrefab, shootingPoint.position, Quaternion.identity);
        Rigidbody rb = proj.GetComponent<Rigidbody>();

        Vector3 target = player.position;

        Vector3 dir = target - shootingPoint.position;
        Vector3 dirXZ = new Vector3(dir.x, 0f, dir.z);
        Vector3 forward = dirXZ.normalized;

    
        float distance = dirXZ.magnitude;

        //  altura automática para la parábola
        float autoUpward = Mathf.Clamp(distance / 6f, 1.2f, 6f);

        //  velocidad configurable (dificultad)
        Vector3 force =
            forward * projectileSpeed +
            Vector3.up * autoUpward;

        rb.AddForce(force, ForceMode.Impulse);
    }

    private void ResetAttack()
    {
        alreadyAttacked = false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
