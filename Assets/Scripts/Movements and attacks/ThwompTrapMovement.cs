using UnityEngine;

public class ThwompTrapMovement : MonoBehaviour
{
    public float moveSpeed = 2f;  // Viteza standard
    public float increasedSpeed = 5f;  // Viteza mărită
    public float waitTime = 2f;
    public Transform[] waypoints;

    private int currentWaypointIndex = 0;
    private bool waiting = false;
    private float waitCounter;
    private bool speedIncreased = false;

    private void Start()
    {
        if (waypoints.Length > 0)
        {
            transform.position = waypoints[0].position;
        }
    }

    private void Update()
    {
        if (waiting)
        {
            waitCounter -= Time.deltaTime;
            if (waitCounter <= 0f)
            {
                waiting = false;
                currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
                speedIncreased = false; // Resetăm flag-ul de viteză mărită
            }
        }
        else
        {
            MoveToNextWaypoint();
        }
    }

    private void MoveToNextWaypoint()
    {
        if (waypoints.Length == 0)
            return;

        Vector2 targetPosition = waypoints[currentWaypointIndex].position;
        float currentSpeed = speedIncreased ? increasedSpeed : moveSpeed;
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, currentSpeed * Time.deltaTime);

        if (Vector2.Distance(transform.position, targetPosition) < 0.1f)
        {
            waiting = true;
            waitCounter = waitTime;
            speedIncreased = true; // Setăm flag-ul de viteză mărită
        }
    }
}
