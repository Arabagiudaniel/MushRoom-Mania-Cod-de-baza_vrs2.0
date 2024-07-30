using UnityEngine;

public class Minigun : MonoBehaviour
{
    [Header("Bullet Settings")]
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float bulletSpeed = 10f; // Speed of the bullet

    [Header("Shooting Settings")]
    public float fireRate = 0.2f; // Time between each shot in seconds
    private float nextFireTime = 0f;

    [Header("Target Settings")]
    public Transform target; // Player's transform

    void Update()
    {
        // Rotate towards the player
        if (target != null)
        {
            Vector2 fireDirection = (target.position - firePoint.position).normalized;
            firePoint.right = fireDirection; // Adjust firePoint's rotation to face the target direction
        }

        // Fire continuously based on fireRate
        if (Time.time >= nextFireTime)
        {
            FireBullet();
            nextFireTime = Time.time + 1f / fireRate;
        }
    }

    void FireBullet()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Bullet12 bulletScript = bullet.GetComponent<Bullet12>();

        // Set bullet speed and direction towards the target
        Vector2 fireDirection = (target.position - firePoint.position).normalized;
        bulletScript.SetDirection(fireDirection, bulletSpeed);
    }
}
