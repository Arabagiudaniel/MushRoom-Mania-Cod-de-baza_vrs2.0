using UnityEngine;

public class Bustean : MonoBehaviour
{
    [SerializeField] private float movementDistance;
    [SerializeField] private float speed;
    private bool movingLeft;
    private float leftEdge;
    private float rightEdge;
    private Transform playerTransform;
    private bool playerOnPlatform;

    private void Awake()
    {
        leftEdge = transform.position.x - movementDistance;
        rightEdge = transform.position.x + movementDistance;
    }

    private void Update()
    {
        Vector3 movement = Vector3.zero;

        if (movingLeft)
        {
            if (transform.position.x > leftEdge)
            {
                movement = new Vector3(-speed * Time.deltaTime, 0, 0);
                transform.position += movement;
            }
            else
            {
                movingLeft = false;
            }
        }
        else
        {
            if (transform.position.x < rightEdge)
            {
                movement = new Vector3(speed * Time.deltaTime, 0, 0);
                transform.position += movement;
            }
            else
            {
                movingLeft = true;
            }
        }

        // Transport the player if they are on the platform
        if (playerOnPlatform && playerTransform != null)
        {
            playerTransform.position += movement;
            Debug.Log("Player is being transported.");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Check if the player is landing on top of the platform
            if (collision.transform.position.y > transform.position.y + 0.5f)
            {
                playerTransform = collision.transform;
                playerOnPlatform = true;
                Debug.Log("Player entered the platform.");
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // When the player exits, stop transporting the player
            playerOnPlatform = false;
            playerTransform = null;
            Debug.Log("Player exited the platform.");
        }
    }
}
