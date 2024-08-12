using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{

    public NavMeshAgent agent;
    public Transform player;
    public LayerMask whatisGround, whatIsPlayer;
    public GameObject projectile;

    //Patrolling
    public Vector3 walkPoint;
    [SerializeField] bool walkPointSet;
    public float walkPointRange;
    //Attacking
    public float timeBetweenAttacks;
    bool attackCooldown;
    //States
    public float sightRange, attackRange;
    public bool playerInSight, playerInAttack;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        if(player == null)
        {
            player = GameObject.FindGameObjectWithTag("Boat").transform;
        }
    }
    private void Update()
    {
        CheckRange();
        if (!playerInSight && !playerInAttack)
        {
            Patrol();
        }
        if (playerInSight && !playerInAttack)
        {
            Chase();
        }
        if (playerInSight && playerInAttack)
        {
            Attack();
        }
    }
    private void CheckRange()
    {
        playerInSight = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
       
        playerInAttack = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);
       
    }
    private void Patrol()
    {
        if (!walkPointSet)
        {
            SearchWalkPoint();
        }
        if (walkPointSet)
        {
            Debug.Log("Soo");
            agent.SetDestination(walkPoint);
        }
        Vector3 distanceToWalkPoint = transform.position - walkPoint;
        //Reached
        if (distanceToWalkPoint.magnitude < 1f)
        {
            walkPointSet = false;
        }
    }
    private void SearchWalkPoint()
    {
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);
        walkPoint = new Vector3(randomX + transform.position.x, transform.position.y, randomZ + transform.position.z);
        RaycastHit hit;
        if (Physics.Raycast(walkPoint, -transform.up,out hit, 10f, whatisGround))
        {
            walkPoint.y = hit.point.y;
            walkPointSet = true;
        }

    }
    private void Chase()
    {
        agent.SetDestination(player.position);
    }
    private void Attack()
    {
        //Stop movement
        agent.SetDestination(transform.position);
        transform.LookAt(player);
        if (!attackCooldown)
        {
            //Attack
            Rigidbody rigidbody = Instantiate(projectile, transform.position, Quaternion.identity).GetComponent<Rigidbody>();
            rigidbody.AddForce(transform.forward * 32f, ForceMode.Impulse);
            rigidbody.AddForce(transform.up * 8f, ForceMode.Impulse);

            attackCooldown = true;
            Invoke("ResetAttack", timeBetweenAttacks);
        }
    }
    private void ResetAttack()
    {
        attackCooldown = false;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(walkPoint, 0.1f);
    }
}
