using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPotion : MonoBehaviour
{
    public float healAmount = 20f; // Amount to heal the player

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.ChangeHealth((int)healAmount);
                Debug.Log("Player healed by " + healAmount);

                // Optionally, you can add a visual effect, sound, or animation here

                Destroy(gameObject); // Destroy the health potion after it's used
            }
        }
    }
}
