using UnityEngine;

public class BatMovement : MonoBehaviour
{
    public Transform[] checkpoints;
    public float speed = 2.0f;
    private int currentCheckpointIndex = 0;
    private Vector3 originalScale;

    void Start()
    {
        if (checkpoints.Length > 0)
        {
            originalScale = transform.localScale;
        }
    }

    void Update()
    {
        if (checkpoints.Length == 0) return;

        Transform target = checkpoints[currentCheckpointIndex];
        Vector3 direction = target.position - transform.position;

        transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);

        if (direction.x > 0)
        {
            transform.localScale = new Vector3(originalScale.x, originalScale.y, originalScale.z); // Face right
        }
        else if (direction.x < 0)
        {
            transform.localScale = new Vector3(-originalScale.x, originalScale.y, originalScale.z); // Face left
        }

        if (Vector3.Distance(transform.position, target.position) < 0.1f)
        {
            currentCheckpointIndex = (currentCheckpointIndex + 1) % checkpoints.Length;
        }
    }
}
