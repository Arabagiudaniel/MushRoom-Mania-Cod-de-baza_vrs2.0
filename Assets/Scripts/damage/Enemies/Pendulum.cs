using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pendulum : MonoBehaviour
{
    Rigidbody2D rb2d;
    public float moveSpeed;
    public AudioSource audioSource;
    public float maxDistance = 10f; // Maximum distance at which sound is heard
    private Transform player;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        player = Camera.main.transform; // Assuming the main camera represents the player's position

        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        Rotate();
        HandleSound();
    }

    public void Rotate()
    {
        rb2d.angularVelocity = moveSpeed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Ensure the pendulum keeps rotating after a collision
        rb2d.angularVelocity = moveSpeed;
    }

    private void HandleSound()
    {
        float distance = Vector3.Distance(transform.position, player.position);

        if (distance <= maxDistance)
        {
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
            audioSource.volume = 1 - (distance / maxDistance); // Reduce volume based on distance
        }
        else
        {
            if (audioSource.isPlaying)
            {
                audioSource.Stop();
            }
        }
    }
}
