using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class ResponseScript : MonoBehaviour
{
    protected bool playerInSightRange, playerInAttackRange;
    public LayerMask whatIsPlayer;
    public Transform player;
    protected bool alreadyAttacked;
    public float sightRange, attackRange;
    public abstract void AttackPlayer();
    public abstract void ChasePlayer();
    protected void ResetAttack()
    {
        alreadyAttacked = false;
    }

    protected void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }
}
