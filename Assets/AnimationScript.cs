using UnityEngine;
using UnityEngine.AI;

public class AnimationScript : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform player;
    public Animator anim;

    public float sightRange = 20f;
    public float attackRange = 2f;

    public float timeBetweenAttacks = 1f;
    bool alreadyAttacked;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("WhatisPlayer").transform;
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        float distance = Vector3.Distance(transform.position, player.position);

        if (distance > sightRange)
        {
            // IDLE
            anim.SetBool("isWalking", false);
            return;
        }

        if (distance > attackRange)
        {
            // WALK
            anim.SetBool("isWalking", true);
            agent.SetDestination(player.position);
        }
        else
        {
            // ATTACK
            anim.SetBool("isWalking", false);
            agent.SetDestination(transform.position);
            transform.LookAt(player);

            if (!alreadyAttacked)
            {
                anim.SetTrigger("Attack");
                alreadyAttacked = true;
                Invoke(nameof(ResetAttack), timeBetweenAttacks);
            }
        }
    }

    void ResetAttack()
    {
        alreadyAttacked = false;
    }
}
