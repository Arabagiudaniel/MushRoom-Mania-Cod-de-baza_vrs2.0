using UnityEngine;

public class HeartCollectible : MonoBehaviour
{
    [SerializeField] private int healthAmount = 1; // Amount of health to add when collected

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Health1 playerHealth = collision.GetComponent<Health1>();
            if (playerHealth != null)
            {
                playerHealth.AddHealth(healthAmount);
                Destroy(gameObject);
            }
        }
    }
}
