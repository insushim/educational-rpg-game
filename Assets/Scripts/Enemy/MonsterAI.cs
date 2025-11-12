using UnityEngine;
using UnityEngine.AI;

public class MonsterAI : MonoBehaviour
{
    private NavMeshAgent agent;
    private Transform player;
    private Animator animator;

    private enum State { Idle, Patrol, Chase, Attack, ReturnToSpawn, Dead }
    private State currentState;

    private Vector3 spawnPosition;
    private float chaseDistance = 10f;
    private float attackDistance = 2f;
    private float patrolSpeed = 2f;
    private float chaseSpeed = 5f;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindWithTag("Player").transform;
        animator = GetComponent<Animator>();
        spawnPosition = transform.position;
        currentState = State.Idle;
    }

    void Update()
    {
        switch (currentState)
        {
            case State.Idle:
                IdleBehavior();
                break;
            case State.Patrol:
                PatrolBehavior();
                break;
            case State.Chase:
                ChaseBehavior();
                break;
            case State.Attack:
                AttackBehavior();
                break;
            case State.ReturnToSpawn:
                ReturnToSpawnBehavior();
                break;
            case State.Dead:
                DeadBehavior();
                break;
        }
    }

    private void IdleBehavior()
    {
        // Logic for idling
        // Check for player detection
        if (Vector3.Distance(transform.position, player.position) < chaseDistance)
        {
            currentState = State.Chase;
        }
    }

    private void PatrolBehavior()
    {
        // Logic for patrolling
        // Check for player detection
        if (Vector3.Distance(transform.position, player.position) < chaseDistance)
        {
            currentState = State.Chase;
        }
    }

    private void ChaseBehavior()
    {
        // Move towards player
        agent.SetDestination(player.position);
        animator.SetTrigger("Run");

        // Check for attack distance
        if (Vector3.Distance(transform.position, player.position) < attackDistance)
        {
            currentState = State.Attack;
            agent.isStopped = true;
        }

        // Check for player out of chase range
        if (Vector3.Distance(transform.position, player.position) > chaseDistance)
        {
            currentState = State.ReturnToSpawn;
        }
    }

    private void AttackBehavior()
    {
        animator.SetTrigger("Attack");
        // Implement attack logic
    }

    private void ReturnToSpawnBehavior()
    {
        agent.SetDestination(spawnPosition);
        if (Vector3.Distance(transform.position, spawnPosition) < 1f)
        {
            currentState = State.Idle;
        }
    }

    private void DeadBehavior()
    {
        // Handle what happens when the monster is dead (e.g., play death animation)
        animator.SetTrigger("Die");
        this.enabled = false; // Disable AI script
    }
}