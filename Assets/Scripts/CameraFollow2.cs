using UnityEngine;

public class CameraFollow2 : MonoBehaviour
{
    public float FollowSpeed = 2f;
    public float yOffset = 1f;
    public Transform target;
    public float zoomSpeed = 2f; // Speed at which the camera zooms in and out
    public float minZoom = 2f; // Minimum orthographic size
    public float maxZoom = 10f; // Maximum orthographic size

    // Define boundary limits for the camera
    public float minX = -10f;
    public float maxX = 10f;
    public float minY = -10f;
    public float maxY = 10f;

    private Camera cam;

    void Start()
    {
        cam = GetComponent<Camera>();
        if (cam == null)
        {
            Debug.LogError("Camera component is missing from this GameObject.");
        }
    }

    void Update()
    {
        if (target != null)
        {
            Vector3 newPos = new Vector3(target.position.x, target.position.y + yOffset, -10f);
            // Clamp camera position within boundaries
            float clampedX = Mathf.Clamp(newPos.x, minX, maxX);
            float clampedY = Mathf.Clamp(newPos.y, minY, maxY);
            transform.position = Vector3.Slerp(transform.position, new Vector3(clampedX, clampedY, -10f), FollowSpeed * Time.deltaTime);
        }

        // Handle zooming with scroll wheel
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll != 0f && cam != null)
        {
            cam.orthographicSize -= scroll * zoomSpeed;
            cam.orthographicSize = Mathf.Clamp(cam.orthographicSize, minZoom, maxZoom);
        }

        // Handle camera size based on key presses
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SetCameraSize(minZoom);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SetCameraSize(minZoom + 2f); // Adjust size as needed
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SetCameraSize(minZoom + 4f); // Adjust size as needed
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            SetCameraSize(maxZoom); // Adjust size as needed
        }
    }

    public void MoveToNewRoom(Transform newRoom)
    {
        // Move the camera immediately to the new room position
        Vector3 newPos = new Vector3(newRoom.position.x, newRoom.position.y + yOffset, -10f);
        // Clamp camera position within boundaries
        float clampedX = Mathf.Clamp(newPos.x, minX, maxX);
        float clampedY = Mathf.Clamp(newPos.y, minY, maxY);
        transform.position = new Vector3(clampedX, clampedY, -10f);
    }

    private void SetCameraSize(float size)
    {
        if (cam != null)
        {
            cam.orthographicSize = Mathf.Clamp(size, minZoom, maxZoom);
        }
    }
}
