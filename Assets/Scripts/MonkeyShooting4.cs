using UnityEngine;

public class MonkeyShooting4 : MonoBehaviour
{
    [Header("Shooting Settings")]
    public GameObject bulletPrefab;
    public float fireRate = 0.5f; // Adjust as needed
    public float bulletSpeed = 20f;
    public int bulletDamage = 1;

    [Header("Target Settings")]
    public Transform targetTransform; // Transform to aim at

    [Header("Spawn Settings")]
    public Transform bulletSpawnPoint; // Point where bullets should spawn

    private float nextFireTime = 0f;

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
        if (targetTransform == null || bulletSpawnPoint == null)
        {
            Debug.LogWarning("Target transform or spawn point is not set!");
            return;
        }

        // Calculate direction from bullet spawn point to target
        Vector2 direction = (targetTransform.position - bulletSpawnPoint.position).normalized;

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
