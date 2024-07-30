using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    PlayerControls controls;

    [Header("Movement Parameters")]
    [SerializeField] private float speed;
    [SerializeField] private float jumpPower;

    [Header("Coyote Time")]
    [SerializeField] private float coyoteTime;
    private float coyoteCounter;

    [Header("Multiple Jumps")]
    [SerializeField] private int extraJumps;
    private int jumpCounter;

    [Header("Layers")]
    [SerializeField] private LayerMask groundLayer;

    [Header("Sounds")]
    [SerializeField] private AudioClip jumpSound;
    [SerializeField] private AudioClip walkSound;
    [SerializeField] private AudioClip coinSound; // Add this line

    private Rigidbody2D body;
    private Animator anim;
    private BoxCollider2D boxCollider;
    private float horizontalInput;
    public Text WINTEXT;
    private bool isFacingRight = true;
    public CoinManager cm;

    private AudioSource audioSource;
    private bool isWalking;

    private void Awake()
    {
        controls = new PlayerControls();
        controls.Enable();

        controls.Land.Move.performed += ctx => {
            horizontalInput = ctx.ReadValue<float>();
        };

        controls.Land.Jump.performed += ctx => Jump();

        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (horizontalInput > 0.01f && !isFacingRight)
            Flip();
        else if (horizontalInput < -0.01f && isFacingRight)
            Flip();

        anim.SetBool("run", horizontalInput != 0);
        anim.SetBool("grounded", isGrounded());

        if (controls.Land.Jump.WasReleasedThisFrame() && body.velocity.y > 0)
            body.velocity = new Vector2(body.velocity.x, body.velocity.y / 2);

        body.velocity = new Vector2(horizontalInput * speed, body.velocity.y);

        if (isGrounded())
        {
            coyoteCounter = coyoteTime;
            jumpCounter = extraJumps;
        }
        else
        {
            coyoteCounter -= Time.deltaTime;
        }

        HandleWalkingSound();
    }

    private void Jump()
    {
        if (coyoteCounter <= 0 && jumpCounter <= 0) return;

        if (isGrounded() || coyoteCounter > 0)
        {
            body.velocity = new Vector2(body.velocity.x, jumpPower);
            jumpCounter = extraJumps;
        }
        else if (jumpCounter > 0)
        {
            body.velocity = new Vector2(body.velocity.x, jumpPower);
            jumpCounter--;
        }

        audioSource.PlayOneShot(jumpSound);
        coyoteCounter = 0;
    }

    private bool isGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.down, 0.1f, groundLayer);
        return raycastHit.collider != null;
    }

    public bool canAttack()
    {
        return horizontalInput == 0 && isGrounded();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "WIN")
        {
            WINTEXT.gameObject.SetActive(true);
        }
    }

    private void Flip()
    {
        isFacingRight = !isFacingRight;
        transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
    }

    private void OnEnable()
    {
        controls.Enable();
    }

    private void OnDisable()
    {
        controls.Disable();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Coin"))
        {
            Destroy(other.gameObject);
            cm.coinCount++;
            audioSource.PlayOneShot(coinSound); // Add this line to play the coin sound
        }
    }

    private void HandleWalkingSound()
    {
        if (isGrounded() && horizontalInput != 0)
        {
            if (!isWalking)
            {
                audioSource.clip = walkSound;
                audioSource.loop = true;
                audioSource.Play();
                isWalking = true;
            }
        }
        else
        {
            if (isWalking)
            {
                audioSource.Stop();
                isWalking = false;
            }
        }
    }
}