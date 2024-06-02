using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class DragonAI : MonoBehaviour
{
    public DragonState currentState;
    public Transform[] patrolPoints;
    public float detectionRadius = 20f;
    public float attackRange = 5f;
    public float fireBreathRange = 15f;
    public float health = 500f;
    public float fireBreathCooldown = 10f;

    private int currentPatrolIndex;
    private NavMeshAgent agent;
    private Transform player;
    private float nextFireBreathTime;

    private Animator animator;


    public enum DragonState
    {
        Idle,
        Patrol,
        Chase,
        Attack,
        Die
    }
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        currentState = DragonState.Idle;
        animator = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        switch (currentState)
        {
            case DragonState.Idle:
                Idle();
                break;
            case DragonState.Patrol:
                Patrol();
                break;
            case DragonState.Chase:
                Chase();
                break;
            case DragonState.Attack:
                Attack();
                break;
            case DragonState.Die:
                Die();
                break;
        }

        if (health <= 0)
        {
            currentState = DragonState.Die;
        }
    }

    void Idle()
    {
        if (Vector3.Distance(player.position, transform.position) <= detectionRadius)
        {
            currentState = DragonState.Chase;
        }
        else
        {
            currentState = DragonState.Patrol;
        }
    }

    void Patrol()
    {
        if (patrolPoints.Length == 0) return;

        if (agent.remainingDistance < 0.5f)
        {
            currentPatrolIndex = (currentPatrolIndex + 1) % patrolPoints.Length;
            agent.SetDestination(patrolPoints[currentPatrolIndex].position);
        }

        if (Vector3.Distance(player.position, transform.position) <= detectionRadius)
        {
            currentState = DragonState.Chase;
        }
    }

    void Chase()
    {
        agent.SetDestination(player.position);

        if (Vector3.Distance(player.position, transform.position) <= attackRange)
        {
            currentState = DragonState.Attack;
        }
        else if (Vector3.Distance(player.position, transform.position) > detectionRadius)
        {
            currentState = DragonState.Idle;
        }
    }

    void Attack()
    {
        agent.isStopped = true;

        if (Time.time >= nextFireBreathTime && Vector3.Distance(player.position, transform.position) <= fireBreathRange)
        {
            FireBreath();
            nextFireBreathTime = Time.time + fireBreathCooldown;
        }
        else if (Vector3.Distance(player.position, transform.position) <= attackRange)
        {
            MeleeAttack();
        }
        else
        {
            currentState = DragonState.Chase;
            agent.isStopped = false;
        }
    }

    void Die()
    {
        // Play death animation and destroy the dragon object
        Destroy(gameObject, 5f);
    }

    void FireBreath()
    {
        // Play fire breath animation and apply damage to player
        Debug.Log("Dragon uses fire breath!");
    }

    void MeleeAttack()
    {
        // Play melee attack animation and apply damage to player
        Debug.Log("Dragon uses melee attack!");
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            currentState = DragonState.Die;
        }
    }
}
