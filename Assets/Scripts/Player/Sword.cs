using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    public float swordDamage = 25f;

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Sword collision detected with: " + collision.gameObject.name);

        MeleeEnemy meleeEnemy = collision.gameObject.GetComponent<MeleeEnemy>();
        if (meleeEnemy != null)
        {
            Debug.Log("Dealing damage to MeleeEnemy");
            meleeEnemy.TakeDamage(swordDamage);
        }
        else
        {
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            if (enemy != null)
            {
                Debug.Log("Dealing damage to Enemy");
                enemy.TakeDamage(swordDamage);
            }
        }
    }
}
