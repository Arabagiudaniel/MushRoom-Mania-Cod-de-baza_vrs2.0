using UnityEngine;

public class Firetrap : MonoBehaviour
{
    [SerializeField] private float damage;

    private Animator anim;
    private SpriteRenderer spriteRend;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        spriteRend = GetComponent<SpriteRenderer>();
        ActivateFiretrap();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<Health1>().TakeDamage((int)damage); // Ensure damage is cast to int
        }
    }

    private void ActivateFiretrap()
    {
        // Turn on animation and set the sprite color to its initial color
        spriteRend.color = Color.white;
        anim.SetBool("activated", true);
    }
}
