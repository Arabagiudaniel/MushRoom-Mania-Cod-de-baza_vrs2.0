using UnityEngine;

public class BubbleManager : MonoBehaviour
{
    public GameObject bubblePrefab; // The bubble prefab
    public Transform player; // Reference to the player's transform
    public float spawnInterval = 1.0f; // Time between bubble spawns
    public float bubbleLifetime = 5.0f; // How long a bubble lasts before popping
    public float bubbleSpeed = 1.0f; // Speed at which bubbles rise

    private float timer = 0.0f;

    void Start()
    {
        // Optionally, start spawning bubbles immediately
        SpawnBubble();
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            SpawnBubble();
            timer = 0.0f;
        }
    }

    void SpawnBubble()
    {
        if (player != null)
        {
            GameObject bubble = Instantiate(bubblePrefab, GetRandomPositionNearPlayer(), Quaternion.identity);
            Rigidbody bubbleRb = bubble.GetComponent<Rigidbody>();
            if (bubbleRb != null)
            {
                bubbleRb.velocity = new Vector3(0, bubbleSpeed, 0); // Make the bubble move upwards
            }
            Destroy(bubble, bubbleLifetime); // Destroy the bubble after its lifetime
        }
    }

    Vector3 GetRandomPositionNearPlayer()
    {
        float x = Random.Range(-8.0f, 8.0f);
        float y = Random.Range(-4.5f, -3.5f);
        float z = Random.Range(-1.0f, 1.0f);

        // Return a position relative to the player's position
        return new Vector3(player.position.x + x, player.position.y + y, player.position.z + z);
    }
}
