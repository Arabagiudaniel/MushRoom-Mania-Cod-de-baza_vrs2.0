using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int damage = 1;
    public float bulletRegenTime = 1f; // Time before a new bullet can be fired

    void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the collided object has the tag "Player"
        if (other.gameObject.CompareTag("Player"))
        {
            // If it's the player, do not deal damage and return
            return;
        }

        // Deal damage to other objects with Health2 component
        Health2 targetHealth = other.gameObject.GetComponent<Health2>();
        if (targetHealth != null)
        {
            targetHealth.TakeDamage(damage);
        }

        // Disable the bullet instead of destroying it immediately
        gameObject.SetActive(false);

        // Respawn the bullet after regen time
        Invoke("RespawnBullet", bulletRegenTime);
    }

    void RespawnBullet()
    {
        gameObject.SetActive(true);
    }
}
