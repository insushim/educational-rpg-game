using UnityEngine;
using UnityEngine.AI;

namespace EducationalRPG.Enemy
{
    /// <summary>
    /// 몬스터의 기본 AI
    /// </summary>
    [RequireComponent(typeof(NavMeshAgent))]
    [RequireComponent(typeof(EnemyStats))]
    public class EnemyAI : MonoBehaviour
    {
        [Header("AI Settings")]
        [SerializeField] private float detectionRange = 10f;
        [SerializeField] private float attackRange = 2f;
        [SerializeField] private float moveSpeed = 3.5f;
        [SerializeField] private float attackCooldown = 2f;

        [Header("Patrol Settings")]
        [SerializeField] private float patrolRadius = 5f;
        [SerializeField] private float idleTime = 2f;

        private NavMeshAgent navAgent;
        private EnemyStats enemyStats;
        private Transform player;
        private float lastAttackTime;
        private float idleTimer;
        private Vector3 spawnPosition;

        private enum AIState
        {
            Idle,
            Patrol,
            Chase,
            Attack,
            Dead
        }

        private AIState currentState = AIState.Idle;

        private void Awake()
        {
            navAgent = GetComponent<NavMeshAgent>();
            enemyStats = GetComponent<EnemyStats>();
            navAgent.speed = moveSpeed;
            spawnPosition = transform.position;
        }

        private void Start()
        {
            var playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj != null)
            {
                player = playerObj.transform;
            }

            enemyStats.OnDeath += HandleDeath;
        }

        private void Update()
        {
            if (currentState == AIState.Dead) return;
            UpdateAI();
        }

        private void UpdateAI()
        {
            if (player == null) return;

            float distanceToPlayer = Vector3.Distance(transform.position, player.position);

            switch (currentState)
            {
                case AIState.Idle:
                    HandleIdleState();
                    break;
                case AIState.Patrol:
                    HandlePatrolState();
                    break;
                case AIState.Chase:
                    HandleChaseState(distanceToPlayer);
                    break;
                case AIState.Attack:
                    HandleAttackState(distanceToPlayer);
                    break;
            }

            if (distanceToPlayer <= detectionRange && currentState != AIState.Attack)
            {
                currentState = AIState.Chase;
            }
        }

        private void HandleIdleState()
        {
            idleTimer += Time.deltaTime;
            if (idleTimer >= idleTime)
            {
                idleTimer = 0;
                currentState = AIState.Patrol;
            }
        }

        private void HandlePatrolState()
        {
            if (!navAgent.hasPath || navAgent.remainingDistance < 0.5f)
            {
                Vector3 randomPoint = spawnPosition + Random.insideUnitSphere * patrolRadius;
                randomPoint.y = spawnPosition.y;

                NavMeshHit hit;
                if (NavMesh.SamplePosition(randomPoint, out hit, patrolRadius, NavMesh.AllAreas))
                {
                    navAgent.SetDestination(hit.position);
                }
                else
                {
                    currentState = AIState.Idle;
                }
            }
        }

        private void HandleChaseState(float distanceToPlayer)
        {
            if (distanceToPlayer <= attackRange)
            {
                currentState = AIState.Attack;
                navAgent.ResetPath();
            }
            else if (distanceToPlayer > detectionRange * 1.5f)
            {
                currentState = AIState.Patrol;
            }
            else
            {
                navAgent.SetDestination(player.position);
            }
        }

        private void HandleAttackState(float distanceToPlayer)
        {
            if (distanceToPlayer > attackRange)
            {
                currentState = AIState.Chase;
                return;
            }

            Vector3 direction = (player.position - transform.position).normalized;
            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z)),
                Time.deltaTime * 5f
            );

            if (Time.time >= lastAttackTime + attackCooldown)
            {
                Attack();
                lastAttackTime = Time.time;
            }
        }

        private void Attack()
        {
            var playerStats = player.GetComponent<Player.PlayerStats>();
            if (playerStats != null)
            {
                playerStats.TakeDamage(enemyStats.AttackPower);
                Debug.Log($"{gameObject.name} attacks player!");
            }
        }

        private void HandleDeath()
        {
            currentState = AIState.Dead;
            navAgent.enabled = false;
            Destroy(gameObject, 3f);
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, detectionRange);

            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, attackRange);

            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(spawnPosition, patrolRadius);
        }

        private void OnDestroy()
        {
            if (enemyStats != null)
            {
                enemyStats.OnDeath -= HandleDeath;
            }
        }
    }
}