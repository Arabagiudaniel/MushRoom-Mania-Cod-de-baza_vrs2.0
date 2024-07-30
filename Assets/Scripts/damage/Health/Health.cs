using UnityEngine;
using System.Collections;

public class Health1 : MonoBehaviour
{
    [Header("Health Settings")]
    public int maxHealth = 10;
    public int health { get; private set; }

    [Header("Animation Settings")]
    private Animator animator;
    private bool isDead = false;

    [Header("iFrames Settings")]
    [SerializeField] private float iFramesDuration = 0.5f;
    [SerializeField] private int numberOfFlashes = 5;
    private bool invulnerable = false;
    private SpriteRenderer spriteRend;

    [Header("Components")]
    [SerializeField] private Behaviour[] components;
    private PlayerRespawn playerRespawn;

    [Header("Sound Settings")]
    [SerializeField] private AudioClip deathSound;
    [SerializeField] private AudioClip hurtSound; // Already present for hurt sound
    private AudioSource audioSource; // Add an AudioSource

    [Header("Audio Manager")]
    [SerializeField] private AudioManager audioManager; // Add a reference to AudioManager

    private void Start()
    {
        health = maxHealth;
        animator = GetComponent<Animator>();
        spriteRend = GetComponent<SpriteRenderer>();
        playerRespawn = GetComponent<PlayerRespawn>();
        audioSource = GetComponent<AudioSource>(); // Initialize AudioSource
    }

    public void TakeDamage(int damage)
    {
        if (isDead || invulnerable) return;

        health -= damage;
        if (health > 0)
        {
            animator.SetTrigger("hurt");
            StartCoroutine(Invulnerability());
            if (hurtSound)
                audioSource.PlayOneShot(hurtSound); // Play the hurt sound
        }
        else
        {
            StartCoroutine(Die());
        }
    }

    public void AddHealth(int healthAmount)
    {
        if (isDead) return;

        health = Mathf.Clamp(health + healthAmount, 0, maxHealth);
    }

    private IEnumerator Die()
    {
        isDead = true;
        animator.SetTrigger("die");

        if (deathSound)
            audioSource.PlayOneShot(deathSound); // Play the death sound

        if (audioManager)
            audioManager.StopMusic(); // Stop the music

        foreach (Behaviour component in components)
            component.enabled = false;

        yield return new WaitForSeconds(0.1f);

        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        yield return new WaitForSeconds(stateInfo.length);

        gameObject.SetActive(false);
        playerRespawn.RespawnCheck();
    }

    private IEnumerator Invulnerability()
    {
        invulnerable = true;
        Physics2D.IgnoreLayerCollision(10, 11, true);
        for (int i = 0; i < numberOfFlashes; i++)
        {
            spriteRend.color = new Color(1, 0, 0, 0.5f);
            yield return new WaitForSeconds(iFramesDuration / (numberOfFlashes * 2));
            spriteRend.color = Color.white;
            yield return new WaitForSeconds(iFramesDuration / (numberOfFlashes * 2));
        }
        Physics2D.IgnoreLayerCollision(10, 11, false);
        invulnerable = false;
    }

    public void Respawn()
    {
        health = maxHealth;
        isDead = false;
        animator.ResetTrigger("die");
        animator.Play("Idle");

        foreach (Behaviour component in components)
            component.enabled = true;

        StartCoroutine(Invulnerability());
        gameObject.SetActive(true);
    }
}