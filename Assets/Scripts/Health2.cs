using UnityEngine;

public class Health2 : MonoBehaviour
{
    public int maxHealth = 1;
    private int currentHealth;
    private GiantOctopusBoss octopusBoss; // Reference to the GiantOctopusBoss script

    void Start()
    {
        currentHealth = maxHealth;
        octopusBoss = FindObjectOfType<GiantOctopusBoss>(); // Find the instance of the GiantOctopusBoss script
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        // Perform any death-related actions here (e.g., play animation, drop loot, etc.)
        Destroy(gameObject); // Destroy the game object
        octopusBoss.EnemyDestroyed(); // Notify GiantOctopusBoss that an enemy has been destroyed
    }
}
