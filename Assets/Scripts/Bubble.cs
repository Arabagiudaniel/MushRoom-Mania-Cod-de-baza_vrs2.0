using UnityEngine;

public class Bubble : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        // Check if the bubble collided with the player
        if (other.CompareTag("Player"))
        {
            // Destroy the bubble
            Destroy(gameObject);
        }
    }
}
