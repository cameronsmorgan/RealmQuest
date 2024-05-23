using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : BaseState
{

    private float moveTimer;
    private float losePlayerTimer;
    private float shotTimer;
    public override void Enter()
    {
    }

    public override void Exit()
    {
    }

    public override void Perform()
    {
       if( enemy.CanSeePlayer())
        {
            losePlayerTimer = 0;
            moveTimer += Time.deltaTime;
            shotTimer+= Time.deltaTime;
            enemy.transform.LookAt(enemy.Player.transform);
            if(shotTimer > enemy.fireRate)
            {
                Shoot();
            }
            if(moveTimer > Random.Range(3, 7))
            {
                enemy.Agent.SetDestination(enemy.transform.position + (Random.insideUnitSphere * 5));
                moveTimer = 0;
            }
        }
        else
        {
            losePlayerTimer += Time.deltaTime;
            if(losePlayerTimer > 8)
            {
                stateMachine.ChangeState(new PatrolState());
            }
        }
    }

    public void Shoot()
    {
        Transform gunBarrel = enemy.gunBarrel;
        if (gunBarrel == null)
        {
            Debug.LogError("Gun barrel transform is not assigned.");
            return;
        }

        GameObject bulletPrefab = Resources.Load("Prefabs/Bullet") as GameObject;
        if (bulletPrefab == null)
        {
            Debug.LogError("Bullet prefab could not be loaded. Check the path and name.");
            return;
        }

        GameObject bullet = GameObject.Instantiate(bulletPrefab, gunBarrel.position, enemy.transform.rotation);
        if (bullet == null)
        {
            Debug.LogError("Bullet instantiation failed.");
            return;
        }

        Debug.Log("Bullet instantiated at position: " + bullet.transform.position);

        Vector3 shootDirection = (enemy.Player.transform.position - gunBarrel.position).normalized;
        Rigidbody bulletRigidbody = bullet.GetComponent<Rigidbody>();
        if (bulletRigidbody == null)
        {
            Debug.LogError("Bullet prefab does not have a Rigidbody component.");
            return;
        }

        bulletRigidbody.velocity = Quaternion.AngleAxis(Random.Range(-3f, 3f), Vector3.up) * shootDirection * 40;
        Debug.Log("Bullet velocity set to: " + bulletRigidbody.velocity);

        Debug.Log("Shoot");
        shotTimer = 0;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
