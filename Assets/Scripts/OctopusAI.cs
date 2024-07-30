using UnityEngine;

public class OctopusAI : MonoBehaviour
{
    public Transform player; // Reference to the player's transform
    public float followSpeed = 5f; // Speed at which the octopus follows the player
    public float attackRange = 2f; // Distance at which the octopus can attack the player
    public float buoyancyForce = 5f; // Buoyancy force to keep the octopus afloat

    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        FollowPlayer();
        ApplyBuoyancy();
    }

    void FollowPlayer()
    {
        if (player != null)
        {
            // Calculate the direction to the player
            Vector3 direction = player.position - transform.position;
            direction.Normalize();

            // Move the octopus towards the player
            transform.position += direction * followSpeed * Time.deltaTime;

            // Optional: Rotate the octopus to face the player
            transform.rotation = Quaternion.LookRotation(direction);

            // Check if within attack range
            if (Vector3.Distance(transform.position, player.position) <= attackRange)
            {
                // Attack the player (implement attack logic here)
                AttackPlayer();
            }
        }
    }

    void ApplyBuoyancy()
    {
        rb.AddForce(Vector2.up * buoyancyForce);
    }

    void AttackPlayer()
    {
        // Implement your attack logic here
        Debug.Log("Octopus is attacking the player!");
    }
}
