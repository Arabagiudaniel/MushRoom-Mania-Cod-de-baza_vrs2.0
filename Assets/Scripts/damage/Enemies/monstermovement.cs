using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement1 : MonoBehaviour
{
    public float moveSpeed = 2f; // Speed of the enemy movement
    public Vector2 moveDirection = Vector2.right; // Initial movement direction

    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        // Get the Rigidbody2D component
        rb = GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            Debug.LogError("No Rigidbody2D component found on this GameObject.");
        }

        // Get the SpriteRenderer component
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer == null)
        {
            Debug.LogError("No SpriteRenderer component found on this GameObject.");
        }
    }

    void Update()
    {
        // Move the enemy in the current direction
        rb.velocity = moveDirection * moveSpeed;

        // Flip the sprite based on the direction of movement
        if (moveDirection.x > 0)
        {
            spriteRenderer.flipX = false; // Face right
        }
        else if (moveDirection.x < 0)
        {
            spriteRenderer.flipX = true; // Face left
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Determine the direction of the collision
        Vector2 contactNormal = collision.contacts[0].normal;

        if (contactNormal.x > 0)
        {
            // Hit from the left, so move right
            moveDirection = Vector2.right;
        }
        else if (contactNormal.x < 0)
        {
            // Hit from the right, so move left
            moveDirection = Vector2.left;
        }

        // Ensure the enemy doesn't get stuck by setting a small positional offset
        transform.position += (Vector3)moveDirection * 0.1f;
    }
}
