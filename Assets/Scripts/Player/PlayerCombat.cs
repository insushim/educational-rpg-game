using UnityEngine;

namespace EducationalRPG.Player
{
    /// <summary>
    /// 플레이어의 전투를 처리하는 클래스
    /// </summary>
    [RequireComponent(typeof(PlayerStats))]
    public class PlayerCombat : MonoBehaviour
    {
        [Header("Combat Settings")]
        [SerializeField] private float attackRange = 2f;
        [SerializeField] private float attackCooldown = 1f;
        [SerializeField] private LayerMask enemyLayer;

        private PlayerStats playerStats;
        private PlayerController playerController;
        private Transform currentTarget;
        private float lastAttackTime;

        // Events
        public System.Action<Transform> OnEnemyKilled;

        private void Awake()
        {
            playerStats = GetComponent<PlayerStats>();
            playerController = GetComponent<PlayerController>();
        }

        private void Update()
        {
            HandleCombatInput();
            UpdateCombat();
        }

        /// <summary>
        /// 전투 입력 처리
        /// </summary>
        private void HandleCombatInput()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit, 100f, enemyLayer))
                {
                    SetTarget(hit.transform);
                }
            }
        }

        /// <summary>
        /// 타겟 설정
        /// </summary>
        public void SetTarget(Transform target)
        {
            currentTarget = target;
        }

        /// <summary>
        /// 전투 업데이트
        /// </summary>
        private void UpdateCombat()
        {
            if (currentTarget == null) return;

            float distanceToTarget = Vector3.Distance(transform.position, currentTarget.position);

            // 사거리 내에 있으면 공격
            if (distanceToTarget <= attackRange)
            {
                playerController.StopMovement();
                TryAttack();
            }
            else
            {
                // 타겟으로 이동
                playerController.MoveToPosition(currentTarget.position);
            }
        }

        /// <summary>
        /// 공격 시도
        /// </summary>
        private void TryAttack()
        {
            if (Time.time >= lastAttackTime + attackCooldown)
            {
                Attack();
                lastAttackTime = Time.time;
            }
        }

        /// <summary>
        /// 공격 실행
        /// </summary>
        private void Attack()
        {
            if (currentTarget == null) return;

            // 타겟을 바라보기
            Vector3 direction = (currentTarget.position - transform.position).normalized;
            transform.rotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));

            // 데미지 적용
            var enemyStats = currentTarget.GetComponent<Enemy.EnemyStats>();
            if (enemyStats != null)
            {
                enemyStats.TakeDamage(playerStats.AttackPower);

                // 적이 죽었는지 확인
                if (enemyStats.CurrentHealth <= 0)
                {
                    OnEnemyKilled?.Invoke(currentTarget);
                    currentTarget = null;
                }
            }

            Debug.Log("Player attacks!");
        }

        /// <summary>
        /// 현재 타겟 해제
        /// </summary>
        public void ClearTarget()
        {
            currentTarget = null;
        }

        private void OnDrawGizmosSelected()
        {
            // 공격 범위 시각화
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, attackRange);
        }
    }
}