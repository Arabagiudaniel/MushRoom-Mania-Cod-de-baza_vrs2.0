using UnityEngine;

public class MonkeyShooting1 : MonoBehaviour
{
    [Header("Shooting Settings")]
    public GameObject bulletPrefab;
    public float fireRate = 0.5f; // Adjust as needed
    public float bulletSpeed = 20f;
    public int bulletDamage = 1;

    [Header("Spawn Settings")]
    public Transform bulletSpawnPoint; // Point where bullets should spawn

    private Transform player; // Transform to aim at
    private float nextFireTime = 0f;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform; // Find the player by tag
    }

    void Update()
    {
        if (Time.time >= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + fireRate;
        }
    }

    void Shoot()
    {
        if (player == null || bulletSpawnPoint == null)
        {
            Debug.LogWarning("Player or spawn point is not set!");
            return;
        }

        // Calculate direction from bullet spawn point to player
        Vector2 direction = (player.position - bulletSpawnPoint.position).normalized;

        // Instantiate a new bullet
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, Quaternion.identity);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.velocity = direction * bulletSpeed;

        // Set bullet damage
        Bullet bulletScript = bullet.GetComponent<Bullet>();
        if (bulletScript != null)
        {
            bulletScript.damage = bulletDamage;
        }
    }
}
