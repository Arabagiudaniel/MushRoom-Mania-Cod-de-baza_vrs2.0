using UnityEngine;

public class MinigunRotation : MonoBehaviour
{
    [Header("Rotation Settings")]
    public Transform firePoint; // The point from which bullets are fired
    public Transform target; // Player's transform

    void Update()
    {
        RotateTowardsTarget();
    }

    void RotateTowardsTarget()
    {
        if (target != null)
        {
            // Calculate the direction from the fire point to the target
            Vector2 direction = (target.position - firePoint.position).normalized;

            // Calculate the angle to rotate the minigun
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            // Rotate the minigun to face the target
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        }
    }
}
