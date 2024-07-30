using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
    [SerializeField] private Health1 playerHealth;  // Reference to Health1
    [SerializeField] private Image totalHealthBar;
    [SerializeField] private Image currentHealthBar;

    private void Start()
    {
        if (playerHealth != null)
        {
            totalHealthBar.fillAmount = (float)playerHealth.maxHealth / playerHealth.maxHealth;
        }
    }

    private void Update()
    {
        if (playerHealth != null)
        {
            currentHealthBar.fillAmount = (float)playerHealth.health / playerHealth.maxHealth;
        }
    }
}