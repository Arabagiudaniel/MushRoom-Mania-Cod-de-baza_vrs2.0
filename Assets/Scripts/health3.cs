using UnityEngine;
using UnityEngine.UI; // Add this for UI handling
using System.Collections;

public class Health3 : MonoBehaviour
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
    [SerializeField] private AudioClip hurtSound;
    private AudioSource audioSource;

    [Header("Audio Manager")]
    [SerializeField] private AudioManager audioManager;

    [Header("Game Over Settings")]
    [SerializeField] private GameObject gameOverCanvas; // Reference to the Game Over UI
    public int lives = 3; // Number of lives

    private void Start()
    {
        health = maxHealth;
        animator = GetComponent<Animator>();
        spriteRend = GetComponent<SpriteRenderer>();
        playerRespawn = GetComponent<PlayerRespawn>();
        audioSource = GetComponent<AudioSource>();
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
                audioSource.PlayOneShot(hurtSound);
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
            audioSource.PlayOneShot(deathSound);

        if (audioManager)
            audioManager.StopMusic();

        foreach (Behaviour component in components)
            component.enabled = false;

        yield return new WaitForSeconds(0.1f);

        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        yield return new WaitForSeconds(stateInfo.length);

        gameObject.SetActive(false);
        playerRespawn.RespawnCheck();

        // Check if the player has more lives
        lives--;
        if (lives <= 0)
        {
            ShowGameOver();
        }
        else
        {
            Respawn();
        }
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

    private void ShowGameOver()
    {
        // Show the Game Over canvas
        if (gameOverCanvas)
        {
            gameOverCanvas.SetActive(true);
        }
        // Optionally, you can also stop the game or trigger other actions here
        Time.timeScale = 0; // Pause the game (optional)
    }
}
