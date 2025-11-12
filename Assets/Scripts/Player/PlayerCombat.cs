using UnityEngine;
using EducationalRPG.Combat;

namespace EducationalRPG.Player
{
    public class PlayerCombat : MonoBehaviour
    {
        [Header("Attack Settings")]
        [SerializeField] private int attackDamage = 20;
        [SerializeField] private float attackRange = 2.5f;
        [SerializeField] private float attackCooldown = 1f;
        [SerializeField] private LayerMask enemyLayer;
        
        [Header("Animation")]
        [SerializeField] private Animator animator;
        
        private CombatStats combatStats;
        private float attackTimer;
        private Transform currentTarget;
        
        private static readonly int AttackHash = Animator.StringToHash("Attack");
        private static readonly int HitHash = Animator.StringToHash("Hit");
        private static readonly int DeathHash = Animator.StringToHash("Death");

        private void Awake()
        {
            combatStats = GetComponent<CombatStats>();
            if (combatStats == null)
            {
                combatStats = gameObject.AddComponent<CombatStats>();
            }
            
            if (animator == null)
            {
                animator = GetComponentInChildren<Animator>();
            }
        }

        private void Start()
        {
            combatStats.OnDeath.AddListener(OnPlayerDeath);
        }

        private void Update()
        {
            attackTimer -= Time.deltaTime;
            
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
            {
                TryAttack();
            }
        }

        private void TryAttack()
        {
            if (attackTimer > 0f) return;
            if (combatStats.IsDead) return;
            
            Collider[] enemies = Physics.OverlapSphere(
                transform.position, 
                attackRange, 
                enemyLayer
            );
            
            if (enemies.Length > 0)
            {
                Transform nearestEnemy = GetNearestEnemy(enemies);
                if (nearestEnemy != null)
                {
                    PerformAttack(nearestEnemy);
                }
            }
        }

        private Transform GetNearestEnemy(Collider[] enemies)
        {
            Transform nearest = null;
            float minDistance = float.MaxValue;
            
            foreach (var enemy in enemies)
            {
                float distance = Vector3.Distance(transform.position, enemy.transform.position);
                if (distance < minDistance)
                {
                    minDistance = distance;
                    nearest = enemy.transform;
                }
            }
            
            return nearest;
        }

        private void PerformAttack(Transform target)
        {
            attackTimer = attackCooldown;
            currentTarget = target;
            
            Vector3 direction = (target.position - transform.position).normalized;
            direction.y = 0f;
            if (direction.magnitude > 0.1f)
            {
                transform.rotation = Quaternion.LookRotation(direction);
            }
            
            if (animator != null)
            {
                animator.SetTrigger(AttackHash);
            }
            
            DealDamage();
        }

        public void DealDamage()
        {
            if (currentTarget == null) return;
            
            var enemyStats = currentTarget.GetComponent<CombatStats>();
            if (enemyStats != null)
            {
                enemyStats.TakeDamage(attackDamage);
            }
        }

        public void TakeDamage(int damage)
        {
            if (combatStats.IsDead) return;
            
            combatStats.TakeDamage(damage);
            
            if (animator != null && !combatStats.IsDead)
            {
                animator.SetTrigger(HitHash);
            }
        }

        private void OnPlayerDeath()
        {
            if (animator != null)
            {
                animator.SetTrigger(DeathHash);
            }
            
            GetComponent<PlayerController>().enabled = false;
            
            Invoke(nameof(Respawn), 3f);
        }

        private void Respawn()
        {
            combatStats.Revive();
            GetComponent<PlayerController>().enabled = true;
            
            Vector3 spawnPoint = Vector3.zero;
            transform.position = spawnPoint;
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, attackRange);
        }
    }
}