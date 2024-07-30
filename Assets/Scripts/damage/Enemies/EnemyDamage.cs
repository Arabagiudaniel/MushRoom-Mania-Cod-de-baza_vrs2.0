using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    [SerializeField] protected float damage;

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Health1 playerHealth = collision.GetComponent<Health1>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage((int)damage); // Cast damage to int
            }
        }
    }
}
