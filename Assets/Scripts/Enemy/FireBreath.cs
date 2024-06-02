using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBreath : MonoBehaviour
{
    private ParticleSystem fireBreathEffect;
    private bool isDamaging = false;
    private float damage = 10f;

    void Start()
    {
        fireBreathEffect = GetComponent<ParticleSystem>();
        if (fireBreathEffect == null)
        {
            Debug.LogError("No ParticleSystem found on FireBreath object.");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Transform hitTransform = other.transform;
        if (hitTransform.CompareTag("Player"))
        {
            Debug.Log("Hit Player");
            hitTransform.GetComponent<PlayerHealth>().TakeDamage(10f);
        }
    }

    public void StartDamaging()
    {
        isDamaging = true;
        Debug.Log("FireBreath started damaging.");
    }

    public void StopDamaging()
    {
        isDamaging = false;
        Debug.Log("FireBreath stopped damaging.");
    }
}
