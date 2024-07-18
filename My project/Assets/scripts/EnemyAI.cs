using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : ResponseScript
{
    private int health;
    private int maxHealth = 100;
    public float walkPointRange;
    private float delay = 0.3f;
    private Vector3 walkPoint;
    [SerializeField]
    private NavMeshAgent agent;
    public LayerMask whatIsGround;
    private float timeBetweenAttacks = 0.5f;
    private bool walkPointSet;
    public GunShot gunShot;
    public HealthBar EnemyhealthBar;
    [SerializeField]
    private DomeHealScript[] domeHealScripts;
    [SerializeField]
    private AudioSource diedSoundEffect;

    void Start()
    {
        //setting the enemy AI starting health
        health = maxHealth;
        EnemyhealthBar.SetMaxHealth(maxHealth);
    }
    private void Awake()
    {
        //to start the AI cycle
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        //Check for sight and attack range
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);
        
        //if player is far away, then just patrol the area
        if (!playerInSightRange && !playerInAttackRange) Patroling();

        //checks if the player is in one of the healing stations
        foreach (DomeHealScript domeHealScript in domeHealScripts)
        {
            //one of the modes will be activated depending on player position
            if (GetClosestDome().playerInDome && !playerInSightRange && !playerInAttackRange) Patroling();
            if (playerInSightRange && !playerInAttackRange && !GetClosestDome().playerInDome) ChasePlayer();
            if (playerInSightRange && playerInAttackRange && !GetClosestDome().playerInDome) AttackPlayer();
        }
        
    }

    void OnCollisionEnter(Collision collision)
    {
        //does player damage to the enemy AI
        if (collision.transform.tag == "PlayerBullet" && health >= 0) 
        {
            TakeDamage();
            ChasePlayer();
        }
    }

    private void Patroling()
    {
        //creates a random path within the area alloted to the AI
        if (!walkPointSet) SearchWalkPoint();
        if (walkPointSet)agent.SetDestination(walkPoint);
        Vector3 distanceToWalkPoint = transform.position - walkPoint;
        
        //Walkpoint reached
        if (distanceToWalkPoint.magnitude < 1f)
        {
            walkPointSet = false;
        }
    }

    private void SearchWalkPoint()
    {
        //Calculate random point in range
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange); 
        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        //checks if the AI is looking at the player path
        if(Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
            walkPointSet = true;
    }

    public override void ChasePlayer()
    {
        //simply set the destination of the agent to the live position of player
        agent.SetDestination(player.position);
    }

    public override void AttackPlayer()
    {
        //Ensure enemy doesn't move
        agent.SetDestination(gameObject.transform.position);
        transform.LookAt(player);
        if(!alreadyAttacked)
        {
            //start shooting the player
            gunShot.Shoot();
            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }

    //if AI gets hit, then this method will be called
    private void TakeDamage()
    {
        health -= 10;
        EnemyhealthBar.SetHealth(health);
        if (health <= 0) StartCoroutine(DestroyEnemy());
    }

    //If AI health is 0, this method will be called to "kill" the AI
    IEnumerator DestroyEnemy()
    {
        diedSoundEffect.Play();
        yield return new WaitForSeconds(delay);
        EnemyhealthBar.DestroyHealthBar();
        Destroy(gameObject);
    }

    //Returns the closest dome to the AI
    private DomeHealScript GetClosestDome()
    {
        DomeHealScript closestDome = null;
        float minDist = 300f;
        Vector3 currentPos = transform.position;
        foreach (DomeHealScript d in domeHealScripts)
        {
            float dist = Vector3.Distance(d.transform.position, currentPos);
            if (dist < minDist)
            {
                closestDome = d;
                minDist = dist;
            }
        }
        return closestDome;
    }
}
