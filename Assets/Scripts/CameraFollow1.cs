using UnityEngine;

public class CameraFollow1 : MonoBehaviour
{
    public Transform player; // Reference to the player's transform
    private float fixedY; // Fixed Y position of the camera
    private float fixedZ; // Fixed Z position of the camera

    void Start()
    {
        // Store the initial Y and Z positions of the camera
        fixedY = transform.position.y;
        fixedZ = transform.position.z;
    }

    void LateUpdate()
    {
        if (player != null)
        {
            // Create a position the camera is aiming for based on the player's X position and fixed Y and Z positions
            Vector3 targetPosition = new Vector3(player.position.x, fixedY, fixedZ);

            // Set the camera's position to the target position
            transform.position = targetPosition;
        }
    }
}
