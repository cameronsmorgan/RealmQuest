using System.Collections;
using System.Collections.Generic;
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
            hitTransform.GetComponent < PlayerHealth>().TakeDamage(1);
            return; // Exit early if the shield is hit
        }

        if (hitTransform.CompareTag("Player"))
        {
            Debug.Log("Hit Player");
            hitTransform.GetComponent<PlayerHealth>().TakeDamage(10);
        }

        Destroy(gameObject);
    }
}
