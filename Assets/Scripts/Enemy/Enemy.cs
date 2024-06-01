using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class Enemy : MonoBehaviour
{
    public UnityAction OnEnemyDefeated; // Event for when the enemy is defeated

    public QuestManager questManager;
    public string questID;
    public int objectiveIndex;

    private StateMachine stateMachine;
    private NavMeshAgent agent;
    private GameObject player;
    public NavMeshAgent Agent { get => agent; }

    public GameObject Player { get => player; }

    public Path path;
    [Header("Sight Values")]
    public float sightDistance = 20f;
    public float fieldOfView = 85f;
    public float eyeHeight;

    [Header("Weapon Values")]
    public Transform gunBarrel;
    [Range(0.1f, 10f)]
    public float fireRate;

    [Header("Health Values")]
    public float maxHealth = 100f;
    public float currentHealth;
    private bool isDead = false;

    [SerializeField]
    private string currentState;

    void Start()
    {
        stateMachine = GetComponent<StateMachine>();
        agent = GetComponent<NavMeshAgent>();
        stateMachine.Initialise();
        player = GameObject.FindGameObjectWithTag("Player");

        currentHealth = maxHealth;
    }

    void Update()
    {
        if (!isDead)
        {
            CanSeePlayer();
            currentState = stateMachine.activeState.ToString();
        }
    }

    public bool CanSeePlayer()
    {
        if (player != null)
        {
            if (Vector3.Distance(transform.position, player.transform.position) < sightDistance)
            {
                Vector3 targetDirection = player.transform.position - transform.position - (Vector3.up * eyeHeight);
                float angleToPlayer = Vector3.Angle(targetDirection, transform.forward);
                if (angleToPlayer >= -fieldOfView && angleToPlayer <= fieldOfView)
                {
                    Ray ray = new Ray(transform.position + (Vector3.up * eyeHeight), targetDirection);
                    RaycastHit hitInfo;
                    if (Physics.Raycast(ray, out hitInfo, sightDistance))
                    {
                        if (hitInfo.transform.gameObject == player)
                        {
                            Debug.DrawRay(ray.origin, ray.direction * sightDistance);
                            return true;
                        }
                    }
                }
            }
        }
        return false;
    }

    public void TakeDamage(float amount)
    {
        if (!isDead)
        {
            currentHealth -= amount;
            if (currentHealth <= 0)
            {
                Die();
            }
        }
    }

    private void Die()
    {
        if (!isDead)
        {
            isDead = true;
            questManager.UpdateEnemyCount(questID, objectiveIndex);
            OnEnemyDefeated?.Invoke(); // Invoke the event
            Destroy(gameObject);
        }
    }
}
