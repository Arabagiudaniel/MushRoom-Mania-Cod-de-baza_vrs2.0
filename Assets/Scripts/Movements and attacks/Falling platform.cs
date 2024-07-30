using UnityEngine;

public class FallingBlock : MonoBehaviour
{
    private Rigidbody2D rb;
    private Vector3 initialPosition;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        initialPosition = transform.position;
        rb.isKinematic = true; // Make sure the block is initially kinematic
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            rb.isKinematic = false; // Make the block fall
            Invoke("RespawnBlock", 1.5f); // Schedule the block to respawn after 2 seconds
        }
    }

    void RespawnBlock()
    {
        rb.isKinematic = true; // Make the block kinematic again
        rb.velocity = Vector2.zero; // Reset the block's velocity
        transform.position = initialPosition; // Move the block back to its initial position
    }
}
