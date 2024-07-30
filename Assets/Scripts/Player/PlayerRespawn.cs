using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    [SerializeField] private AudioClip checkpoint;
    [SerializeField] private Transform defaultCheckpoint; // Optionally assign a default checkpoint in the inspector
    private Transform currentCheckpoint;
    private Health1 playerHealth;
    private UIManager uiManager;
    private CameraFollow cameraFollow;

    private void Awake()
    {
        playerHealth = GetComponent<Health1>();
        uiManager = FindObjectOfType<UIManager>();
        cameraFollow = Camera.main.GetComponent<CameraFollow>(); // Get the CameraFollow component from the main camera
    }

    private void Start()
    {
        if (defaultCheckpoint != null)
        {
            SetCheckpoint(defaultCheckpoint);
        }
    }

    public void RespawnCheck()
    {
        if (currentCheckpoint == null)
        {
            uiManager.GameOver();
            return;
        }

        playerHealth.Respawn(); // Restore player health and reset animation
        transform.position = currentCheckpoint.position; // Move player to checkpoint location

        // Move the camera to the checkpoint's room
        cameraFollow.MoveToNewRoom(currentCheckpoint.parent);
    }

    public void SetCheckpoint(Transform newCheckpoint)
    {
        currentCheckpoint = newCheckpoint;
        SoundManager.instance.PlaySound(checkpoint);
        Debug.Log("Checkpoint set to: " + newCheckpoint.position);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Checkpoint")
        {
            SetCheckpoint(collision.transform);
            collision.GetComponent<Collider2D>().enabled = false;
            collision.GetComponent<Animator>().SetTrigger("activate");
        }
    }
}
