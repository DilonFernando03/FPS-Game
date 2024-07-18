using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : ResponseScript
{
    [SerializeField]
    private GunShot turretShot;
    private Vector3 target;
    private float timeBetweenAttacks = 1f;
    public void Update()
    {
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);
        if (playerInSightRange && !playerInAttackRange) ChasePlayer();
        if (playerInSightRange && playerInAttackRange) AttackPlayer();
    }

    public override void ChasePlayer()
    {
        transform.LookAt(player);
        transform.rotation =  transform.rotation * Quaternion.AngleAxis(-90,Vector3.right);
    }

    public override void AttackPlayer()
    {   
        transform.LookAt(player);
        transform.rotation =  transform.rotation * Quaternion.AngleAxis(-90,Vector3.right);
        if(!alreadyAttacked)
        {
            turretShot.Shoot();
            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }    
}
