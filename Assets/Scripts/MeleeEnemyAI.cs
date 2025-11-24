using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class MeleeEnemyAI : MonoBehaviour
{
    [Header("References")]
    public NavMeshAgent agent;
    public Animator anim;
    public Transform player;
    public Transform attackPoint;      // Empty adelante del enemigo
    public string playerTag = "whatIsPlayer";

    [Header("Patrol")]
    public float walkPointRange = 8f;
    private Vector3 walkPoint;
    private bool walkPointSet;

    [Header("Ranges")]
    public float sightRange = 10f;
    public float attackRange = 2f;

    [Header("Combat")]
    public int damage = 10;
    public float timeBetweenAttacks = 1f;
    public float damageDelay = 0.35f; // ajustar para que coincida con el impacto de la animación
    public float attackRadius = 1.5f;

    // estados internos
    private float attackCooldownTimer = 0f;
    private bool isAttacking = false;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();

        // Seguridad: si agent existe, desactivar su rotación automática para controlarla manualmente
        if (agent != null)
        {
            agent.updateRotation = false; // IMPORTANTE para evitar inclinación e interferencia
            agent.updatePosition = true;
        }

        // Buscar player por tag si no está asignado
        if (player == null)
        {
            GameObject p = GameObject.FindGameObjectWithTag(playerTag);
            if (p != null) player = p.transform;
            else Debug.LogWarning($"[MeleeEnemyAI] No se encontró Player con tag '{playerTag}'. Asignalo en el inspector.");
        }

        if (attackPoint == null)
            Debug.LogWarning("[MeleeEnemyAI] attackPoint no está asignado. Asignalo y colocalo delante del enemigo.");
    }

    private void Update()
    {
        if (player == null) return;

        float dist = Vector3.Distance(transform.position, player.position);

        // cooldown
        if (attackCooldownTimer > 0f)
            attackCooldownTimer -= Time.deltaTime;

        // si estamos atacando, no ejecutamos la lógica normal
        if (isAttacking) return;

        if (dist > sightRange)
        {
            Patrol();
        }
        else if (dist > attackRange)
        {
            Chase();
        }
        else
        {
            // dentro del rango de ataque
            if (attackCooldownTimer <= 0f)
                StartCoroutine(PerformAttackRoutine());
        }
    }

    // ---------------- PATROL ----------------
    private void Patrol()
    {
        anim.SetBool("isWalking", true);

        if (!walkPointSet) SearchWalkPoint();

        if (walkPointSet && agent != null)
            agent.SetDestination(walkPoint);

        if (Vector3.Distance(transform.position, walkPoint) < 1f)
            walkPointSet = false;
    }

    private void SearchWalkPoint()
    {
        float rx = Random.Range(-walkPointRange, walkPointRange);
        float rz = Random.Range(-walkPointRange, walkPointRange);
        walkPoint = new Vector3(transform.position.x + rx, transform.position.y, transform.position.z + rz);

        if (Physics.Raycast(walkPoint + Vector3.up, Vector3.down, 2f))
            walkPointSet = true;
    }

    // ---------------- CHASE ----------------
    private void Chase()
    {
        anim.SetBool("isWalking", true);
        if (agent != null)
        {
            agent.isStopped = false;
            agent.SetDestination(player.position);
        }

        // rotación suave hacia player (solo Y)
        RotateTowardsPlayerSmooth();
    }

    // ---------------- ATTACK ----------------
    private IEnumerator PerformAttackRoutine()
    {
        // bloquear acciones mientras se reproduce el ataque
        isAttacking = true;
        attackCooldownTimer = timeBetweenAttacks;

        // parar al agent
        if (agent != null) agent.isStopped = true;

        // rotar inmediatamente hacia el player (solo Y)
        RotateTowardsPlayerInstant();

        // disparar animación
        if (anim != null)
        {
            anim.SetBool("isWalking", false);
            anim.SetTrigger("Attack");
        }

        // esperar al momento del impacto
        yield return new WaitForSeconds(damageDelay);

        // aplicar daño robustamente (sin depender de layers)
        TryDealDamage();

        // esperar un pequeño tiempo para que termine la animación (opcional ajuste)
        yield return new WaitForSeconds(0.15f);

        // volver a permitir IA normal
        isAttacking = false;

        // reactivar agent (si está dentro de chase o patrol lo volverá a setear)
        if (agent != null) agent.isStopped = false;
    }

    private void TryDealDamage()
    {
        if (attackPoint == null)
        {
            Debug.LogWarning("[MeleeEnemyAI] attackPoint nulo: no se puede detectar colisiones de golpe.");
            return;
        }

        // Detectar todos los colliders en radio y filtrar por tag del player
        Collider[] hits = Physics.OverlapSphere(attackPoint.position, attackRadius);

        bool anyHit = false;
        foreach (var c in hits)
        {
            if (c == null) continue;
            if (c.CompareTag(playerTag))
            {
                anyHit = true;
                DataPlayer dp = c.GetComponent<DataPlayer>();
                if (dp != null)
                {
                    dp.healthPlayer -= damage;
                    Debug.Log($"[MeleeEnemyAI] Golpeó al player: -{damage} hp (quedan {dp.healthPlayer})");
                }
                else
                {
                    Debug.Log("[MeleeEnemyAI] Collider con tag player encontrado, pero no tiene DataPlayer.");
                }
            }
        }

        if (!anyHit)
            Debug.Log("[MeleeEnemyAI] Ataque no conectó: no hubo colliders con la tag del player en el rango.");
    }

    // ---------------- ROTACIÓN (solo Y) ----------------
    private void RotateTowardsPlayerInstant()
    {
        Vector3 dir = player.position - transform.position;
        dir.y = 0f;
        if (dir.sqrMagnitude > 0.0001f)
            transform.rotation = Quaternion.LookRotation(dir);
    }

    private void RotateTowardsPlayerSmooth()
    {
        Vector3 dir = player.position - transform.position;
        dir.y = 0f;
        if (dir.sqrMagnitude > 0.0001f)
        {
            Quaternion target = Quaternion.LookRotation(dir);
            transform.rotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime * 8f);
        }
    }

    // gizmos para ver el rango de golpe
    private void OnDrawGizmosSelected()
    {
        if (attackPoint != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(attackPoint.position, attackRadius);
        }
    }
}
