using UnityEngine;
using System.Collections.Generic;

public class MonkeyShooting2 : MonoBehaviour
{
    [Header("Shooting Settings")]
    public GameObject bulletPrefab;
    public int poolSize = 10; // Number of bullets to instantiate initially
    public float fireRate = 0.5f; // Adjust as needed
    public float bulletSpeed = 20f;
    public int bulletDamage = 1;

    [Header("Target Settings")]
    public Transform targetTransform; // Transform to aim at

    [Header("Spawn Settings")]
    public Transform bulletSpawnPoint; // Point where bullets should spawn

    [Header("Submarine Settings")]
    public Transform submarineTransform; // Transform of the submarine

    [Header("Minigun Settings")]
    public Vector3 minigunScale = new Vector3(3f, 3f, 3f); // Scale of the minigun

    private List<GameObject> bulletPool;
    private float nextFireTime = 0f;

    private void Start()
    {
        // Initialize bullet pool
        InitializeBulletPool();
    }

    private void InitializeBulletPool()
    {
        bulletPool = new List<GameObject>();

        for (int i = 0; i < poolSize; i++)
        {
            GameObject bullet = Instantiate(bulletPrefab, Vector3.zero, Quaternion.identity);
            bullet.SetActive(false);
            bulletPool.Add(bullet);
        }
    }

    void Update()
    {
        // Check if the left mouse button is held down and if the cooldown has passed
        if (Input.GetMouseButton(0) && Time.time >= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + fireRate;
        }

        // Check submarine facing direction
        if (submarineTransform != null)
        {
            // If submarine is facing left (considering local scale)
            if (submarineTransform.localScale.x < 0)
            {
                // Flip gun to face left
                transform.localScale = new Vector3(-minigunScale.x, minigunScale.y, minigunScale.z);
            }
            else
            {
                // Reset gun scale to face right
                transform.localScale = minigunScale;
            }
        }
    }

    void Shoot()
    {
        // Get an inactive bullet from the pool
        GameObject bullet = GetInactiveBullet();
        if (bullet == null)
        {
            Debug.LogWarning("Bullet pool exhausted. Consider increasing pool size.");
            return;
        }

        // Activate the bullet at spawn point
        bullet.transform.position = bulletSpawnPoint.position;
        bullet.SetActive(true);

        // Calculate direction from bullet spawn point to target
        Vector2 direction = (targetTransform.position - bulletSpawnPoint.position).normalized;
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.velocity = direction * bulletSpeed;

        // Calculate angle to rotate bullet towards direction
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        bullet.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        // Set bullet damage
        Bullet bulletScript = bullet.GetComponent<Bullet>();
        if (bulletScript != null)
        {
            bulletScript.damage = bulletDamage;
        }
    }

    GameObject GetInactiveBullet()
    {
        foreach (GameObject bullet in bulletPool)
        {
            if (!bullet.activeSelf)
            {
                return bullet;
            }
        }
        return null;
    }
}
