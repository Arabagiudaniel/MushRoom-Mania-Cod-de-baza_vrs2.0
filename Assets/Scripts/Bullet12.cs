using UnityEngine;

public class Bullet12 : MonoBehaviour
{
    private Vector2 moveDirection;
    private float moveSpeed;

    void Update()
    {
        // Move the bullet in the specified direction
        transform.Translate(moveDirection * moveSpeed * Time.deltaTime, Space.World);

        // Rotate the bullet to face its movement direction
        float angle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    public void SetDirection(Vector2 direction, float speed)
    {
        moveDirection = direction.normalized;
        moveSpeed = speed;

        // Flip bullet sprite if moving left (optional)
        if (direction.x < 0)
        {
            Vector3 scale = transform.localScale;
            scale.x = -Mathf.Abs(scale.x);
            transform.localScale = scale;
        }
        else
        {
            Vector3 scale = transform.localScale;
            scale.x = Mathf.Abs(scale.x);
            transform.localScale = scale;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Check if bullet hits the player
        if (other.CompareTag("Player"))
        {
            Health1 playerHealth = other.GetComponent<Health1>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(1); // Adjust damage amount if needed
            }

            // Destroy bullet on collision with player
            Destroy(gameObject);
        }
    }

    void OnBecameInvisible()
    {
        // Destroy bullet when it goes out of the screen
        Destroy(gameObject);
    }
}
