using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
using UnityEngine.UI;
public class Enemy : MonoBehaviour
{
    public bool isDragon; // Flag to indicate if the enemy is a dragon

    public UnityAction OnEnemyDefeated; // Event for when the enemy is defeated

    public QuestManager questManager;
    public string questID;
    public int objectiveIndex;

    private StateMachine stateMachine;
    private NavMeshAgent agent;
    private GameObject player;
    private Animator animator; // Add Animator reference

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

    public Image healthBarImage;


    void Start()
    {
        stateMachine = GetComponent<StateMachine>();
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>(); // Initialize Animator

        stateMachine.Initialise();
        player = GameObject.FindGameObjectWithTag("Player");

        currentHealth = maxHealth;

        if (isDragon && healthBarImage != null)
        {
            SetHealth(currentHealth, maxHealth);
        }
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

            if (isDragon && healthBarImage != null)
            {
                SetHealth(currentHealth, maxHealth);
            }
            if (currentHealth <= 0)
            {
                Die();
            }
        }
    }
    private IEnumerator DestroyAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);
    }
    private void Die()
    {
        if (isDead) return;

        isDead = true;
        animator.SetBool("IsDead", true); // Set animation parameter

        agent.isStopped = true; // Stop the NavMeshAgent
        agent.enabled = false; // Disable the NavMeshAgent

        // Optionally disable colliders or other components
       

        // Disable other components as needed
        stateMachine.enabled = false;

        questManager.UpdateEnemyCount(questID, objectiveIndex);
        OnEnemyDefeated?.Invoke(); // Invoke the event

        if (!isDragon)
        {
            StartCoroutine(DestroyAfterDelay(2f)); // Destroy after 2 seconds if not a dragon
        }
        else
        {
            StartCoroutine(DisableDragonComponentsAfterDelay(2f)); // Disable components after 2 seconds
        }
    }
    private IEnumerator DisableDragonComponentsAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        // Disable other components as needed
       
        // You can disable other components here

        // Optionally, you can freeze the dragon's position to ensure it stays in the last frame of the animation
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = true;
        }
    }



    public void SetAnimationBool(string parameter, bool value)
    {
        if (animator != null)
        {
            animator.SetBool(parameter, value);
        }
    }

    public void StartFireBreathCoroutine(IEnumerator coroutine)
    {
        if (isDragon == true)
        {


            StartCoroutine(coroutine);
        }
    }

    public void SetHealth(float health, float maxHealth)
    {
        healthBarImage.fillAmount = health / maxHealth;
    }
}
