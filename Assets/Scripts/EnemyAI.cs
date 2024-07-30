using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public float lookRadius = 10f; // Detection range for player
    public float attackRadius = 2f; // Attack range for player
    public float attackRate = 1f; // Time between attacks
    public int attackDamage = 10; // Damage dealt to the player

    private Transform target; // Reference to the player
    private NavMeshAgent agent; // Reference to the NavMeshAgent
    private float attackCooldown = 0f; // Cooldown timer for attacks

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        // Calculate the distance to the player
        float distance = Vector3.Distance(target.position, transform.position);

        // If within lookRadius, move towards the player
        if (distance <= lookRadius)
        {
            Debug.Log("Player in look radius");
            agent.SetDestination(target.position);

            // If within attackRadius, attack the player
            if (distance <= attackRadius)
            {
                Debug.Log("Player in attack radius");
                if (attackCooldown <= 0f)
                {
                    Attack();
                    attackCooldown = 1f / attackRate; // Reset attack cooldown
                }
            }
        }

        // Decrease attack cooldown over time
        if (attackCooldown > 0f)
        {
            attackCooldown -= Time.deltaTime;
        }
    }

    void Attack()
    {
        // Perform attack on the player
        Debug.Log("Attacking player");
        Health1 playerHealth = target.GetComponent<Health1>();
        if (playerHealth != null)
        {
            playerHealth.TakeDamage(attackDamage);
            Debug.Log("Damage dealt to player");
        }
    }

    void OnDrawGizmosSelected()
    {
        // Draw the look radius and attack radius in the editor
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, lookRadius);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRadius);
    }
}
