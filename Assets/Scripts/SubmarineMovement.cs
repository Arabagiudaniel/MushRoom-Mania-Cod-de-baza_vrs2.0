using UnityEngine;

public class SubmarineMovement : MonoBehaviour
{
    [Header("Movement Parameters")]
    [SerializeField] private float acceleration = 5f; // Acceleration force
    [SerializeField] private float maxSpeed = 2f; // Max horizontal speed
    [SerializeField] private float verticalAcceleration = 3f; // Vertical acceleration force
    [SerializeField] private float maxVerticalSpeed = 1f; // Max vertical speed

    [Header("Buoyancy")]
    [SerializeField] private float buoyancyForce = 5f; // Reduced buoyancy force

    private Rigidbody2D rb;
    private bool isFacingRight = true;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        // Horizontal movement
        float horizontalInput = Input.GetAxis("Horizontal");
        if (horizontalInput != 0)
        {
            rb.AddForce(new Vector2(horizontalInput * acceleration, 0), ForceMode2D.Force);
            if (Mathf.Abs(rb.velocity.x) > maxSpeed)
            {
                rb.velocity = new Vector2(Mathf.Sign(rb.velocity.x) * maxSpeed, rb.velocity.y);
            }

            // Flip submarine based on direction
            if (horizontalInput > 0 && !isFacingRight)
            {
                Flip();
            }
            else if (horizontalInput < 0 && isFacingRight)
            {
                Flip();
            }
        }

        // Vertical movement
        float verticalInput = Input.GetAxis("Vertical");
        if (verticalInput != 0)
        {
            rb.AddForce(new Vector2(0, verticalInput * verticalAcceleration), ForceMode2D.Force);
            if (Mathf.Abs(rb.velocity.y) > maxVerticalSpeed)
            {
                rb.velocity = new Vector2(rb.velocity.x, Mathf.Sign(rb.velocity.y) * maxVerticalSpeed);
            }
        }

        // Apply buoyancy force
        ApplyBuoyancy();
    }

    private void ApplyBuoyancy()
    {
        rb.AddForce(Vector2.up * buoyancyForce);
    }

    private void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 localScale = transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;

        // Rotate child objects to match submarine's rotation
        foreach (Transform child in transform)
        {
            Vector3 childLocalScale = child.localScale;
            childLocalScale.x *= -1;
            child.localScale = childLocalScale;
        }
    }
}