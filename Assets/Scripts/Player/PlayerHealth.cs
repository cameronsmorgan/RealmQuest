using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public float health;
    private float lerpTimer;
    public float maxHealth = 100f;
    public float chipSpeed = 2f;
    public Image frontHealthBar;
    public Image backHealthBar;
    private FirstPersonController fpsController; // Assuming this is the script name
    private WeaponController weaponController;
    private bool isDead = false;

    private FadeManager fadeManager; // Reference to FadeManager

    void Start()
    {
        health = maxHealth;
        fpsController = GetComponent<FirstPersonController>();
        weaponController = GetComponent<WeaponController>();

        fadeManager = FindObjectOfType<FadeManager>(); // Find the FadeManager in the scene
    }

    void Update()
    {
        if (isDead) return;

        health = Mathf.Clamp(health, 0, maxHealth);
        UpdateHealthUI();

        if (health <= 0 && !isDead)
        {
            HandleDeath();
        }
    }

    public void UpdateHealthUI()
    {
        float fillF = frontHealthBar.fillAmount;
        float fillB = backHealthBar.fillAmount;
        float hFraction = health / maxHealth;

        if (fillB > hFraction)
        {
            frontHealthBar.fillAmount = hFraction;
            backHealthBar.color = Color.red;
            lerpTimer += Time.deltaTime;
            float percentComplete = lerpTimer / chipSpeed;
            percentComplete = percentComplete * percentComplete;
            backHealthBar.fillAmount = Mathf.Lerp(fillB, hFraction, percentComplete);
        }

        if (fillF < hFraction)
        {
            backHealthBar.color = Color.green;
            backHealthBar.fillAmount = hFraction;
            lerpTimer += Time.deltaTime;
            float percentComplete = lerpTimer / chipSpeed;
            percentComplete = percentComplete * percentComplete;
            frontHealthBar.fillAmount = Mathf.Lerp(fillF, backHealthBar.fillAmount, percentComplete);
        }
    }

    public void ChangeHealth(int healAmount)
    {
        health += healAmount;
        lerpTimer = 0f;
        Debug.Log("Health changed by: " + healAmount + ". New health: " + health);
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        lerpTimer = 0f;
    }

    private void HandleDeath()
    {
        isDead = true;

        // Disable the movement and weapon scripts
        if (fpsController != null) fpsController.enabled = false;
        if (weaponController != null) weaponController.enabled = false;

        // Rotate the player and adjust position
        transform.Rotate(-23, 84, -75);

        Debug.Log("Player has died.");

        // Start the fade and scene transition
        if (fadeManager != null)
        {
            fadeManager.FadeToBlack("PlayerLoseScene"); // Trigger the fade to black and load the lose scene
        }
        else
        {
            Debug.LogError("FadeManager not found!");
        }
    }
}
