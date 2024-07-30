using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public Transform checkpoint1;
    public Transform checkpoint2;
    public float speed = 2.0f;

    private Transform targetCheckpoint;
    private bool isMoving = false;
    private GameObject player;

    void Start()
    {
        // Initialize the first target checkpoint
        targetCheckpoint = checkpoint1;
    }

    void Update()
    {
        // Move the platform if it is set to move
        if (isMoving)
        {
            float step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, targetCheckpoint.position, step);

            // Check if the platform has reached the target checkpoint
            if (Vector3.Distance(transform.position, targetCheckpoint.position) < 0.001f)
            {
                // Toggle the target checkpoint
                targetCheckpoint = targetCheckpoint == checkpoint1 ? checkpoint2 : checkpoint1;
                isMoving = false;
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        // Check if the player steps on the platform
        if (other.CompareTag("Player"))
        {
            player = other.gameObject;
            player.transform.SetParent(transform);
            isMoving = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        // Check if the player steps off the platform
        if (other.CompareTag("Player"))
        {
            player.transform.SetParent(null);
        }
    }
}
