using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetection : MonoBehaviour
{

    public WeaponController wc;
    public GameObject HitParticle;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy" && wc.isAttacking)
        {
            Debug.Log(other.name);
            GameObject hitParticleInstance = Instantiate(HitParticle, new Vector3(other.transform.position.x, transform.position.y, other.transform.position.z), other.transform.rotation);
            Destroy(hitParticleInstance, 0.5f); // Destroy the particle after 0.5 seconds
        }
    }
}
