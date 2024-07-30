using UnityEngine;

public class Bullet1 : MonoBehaviour
{
    public int damage = 1;

    void OnTriggerEnter2D(Collider2D other)
    {
        // Check if bullet collides with an object that has Health1 script (or any other script you want to damage)
        Health1 health = other.GetComponent<Health1>();
        if (health != null)
        {
            health.TakeDamage(damage); // Damage the object
        }

        // Check if the other collider is on the "Ground" layer
        if (other.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            Destroy(gameObject); // Destroy bullet on collision with ground
        }
    }
}