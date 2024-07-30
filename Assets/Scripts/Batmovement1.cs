using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GiantOctopusBoss giantOctopusBoss; // Make this field public so it can be accessed from other classes

    private Transform player; // Transform of the player
    public float speed = 2f; // Speed at which the enemy moves towards the player
    private bool facingRight = true; // Track the direction the enemy is facing

    void Start()
    {
        // Find and set the GiantOctopusBoss reference by tag
        GameObject bossObject = GameObject.FindGameObjectWithTag("caracatita");
        if (bossObject != null)
        {
            giantOctopusBoss = bossObject.GetComponent<GiantOctopusBoss>();
        }

        if (giantOctopusBoss == null)
        {
            Debug.LogError("GiantOctopusBoss reference not found for enemy!");
        }

        // Find the player by tag
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
        {
            player = playerObject.transform;
        }

        if (player == null)
        {
            Debug.LogError("Player not found!");
        }
    }

    void Update()
    {
        if (player != null)
        {
            // Move towards the player
            Vector2 direction = (player.position - transform.position).normalized;
            transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);

            // Flip the enemy sprite based on the movement direction
            if (direction.x > 0 && !facingRight)
            {
                Flip();
            }
            else if (direction.x < 0 && facingRight)
            {
                Flip();
            }
        }
    }

    void Flip()
    {
        // Switch the way the player is facing
        facingRight = !facingRight;

        // Multiply the player's x local scale by -1
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    // This method should be called when the enemy dies
    public void OnDeath()
    {
        // Call the EnemyDestroyed method on the GiantOctopusBoss instance
        if (giantOctopusBoss != null)
        {
            giantOctopusBoss.EnemyDestroyed();
        }
        else
        {
            Debug.LogError("GiantOctopusBoss reference not set for enemy!");
        }

        // Destroy the enemy GameObject
        Destroy(gameObject);
    }
}
