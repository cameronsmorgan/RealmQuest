using UnityEngine;

public class Bullet : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        Transform hitTransform = collision.transform;
        Debug.Log("Hit: " + hitTransform.name + ", Tag: " + hitTransform.tag); // Log the name and tag of the hit object

        if (hitTransform.CompareTag("Shield"))
        {
            Debug.Log("Hit Shield");
            // Assuming the shield has a PlayerHealth script to reduce shield health
            hitTransform.GetComponentInParent<PlayerHealth>().TakeDamage(1);
            Destroy(gameObject);
            return; // Exit early if the shield is hit
        }

        if (hitTransform.CompareTag("Player"))
        {
            Debug.Log("Hit Player");
            // Check if the shield is active
            Shield shieldScript = hitTransform.GetComponentInChildren<Shield>();
            if (shieldScript != null && shieldScript.IsShieldActive())
            {
                Debug.Log("Shield is active, no damage to player");
                // Optionally you could handle shield logic here if needed
            }
            else
            {
                hitTransform.GetComponent<PlayerHealth>().TakeDamage(10);
            }
        }

        Destroy(gameObject);
    }
}
