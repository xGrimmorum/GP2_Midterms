using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    public PlayerController player;
    public Image healthBar;
    public Text healthText;
    private int playerHealth;

    private void Start()
    {
        playerHealth = Object.FindFirstObjectByType<PlayerController>().health;
        UpdateHealthUI();
    }

    void Update()
    {
        healthBar.fillAmount = (float)player.health / 5;
    }

    public void UpdateHealthUI()
    {
        healthText.text = "HP: " + playerHealth;
    }

    public void SetHealth(int newHealth)
    {
        playerHealth = newHealth;
        UpdateHealthUI();
    }

}
