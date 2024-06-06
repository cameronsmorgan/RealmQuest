using UnityEngine;

public class Shield : MonoBehaviour
{
    public GameObject shield; // Assign the shield GameObject in the Inspector
    private Animator shieldAnimator;
    public KeyCode shieldKey = KeyCode.Mouse1; // Right mouse button by default

    void Start()
    {
        if (shield != null)
        {
            shieldAnimator = shield.GetComponent<Animator>();
            Debug.Log("Shield Animator assigned.");
        }
        else
        {
            Debug.LogError("Shield not assigned in the Inspector.");
        }
    }

    void Update()
    {
        HandleShieldInput();
    }

    private void HandleShieldInput()
    {
        if (Input.GetKeyDown(shieldKey))
        {
            ActivateShield();
        }
        else if (Input.GetKeyUp(shieldKey))
        {
            DeactivateShield();
        }
    }

    private void ActivateShield()
    {
        if (shieldAnimator != null)
        {
            shieldAnimator.SetBool("IsActive", true);
            Debug.Log("Shield activated.");
        }
    }

    private void DeactivateShield()
    {
        if (shieldAnimator != null)
        {
            shieldAnimator.SetBool("IsActive", false);
            Debug.Log("Shield deactivated.");
        }
    }
}