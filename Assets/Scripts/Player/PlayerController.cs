using UnityEngine;
using UnityEngine.AI;

namespace EducationalRPG.Player
{
    /// <summary>
    /// 플레이어 이동을 제어하는 컨트롤러
    /// 마우스 클릭으로 이동 (리니지 스타일)
    /// </summary>
    [RequireComponent(typeof(NavMeshAgent))]
    public class PlayerController : MonoBehaviour
    {
        [Header("Movement Settings")]
        [SerializeField] private float moveSpeed = 5f;
        [SerializeField] private float rotationSpeed = 10f;

        private NavMeshAgent navAgent;
        private Camera mainCamera;
        private Animator animator;

        private void Awake()
        {
            navAgent = GetComponent<NavMeshAgent>();
            mainCamera = Camera.main;
            animator = GetComponent<Animator>();

            // NavMeshAgent 설정
            navAgent.speed = moveSpeed;
            navAgent.angularSpeed = rotationSpeed * 100f;
        }

        private void Update()
        {
            HandleMouseInput();
            UpdateAnimation();
        }

        /// <summary>
        /// 마우스 클릭 입력 처리
        /// </summary>
        private void HandleMouseInput()
        {
            // 마우스 왼쪽 버튼 클릭
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                // 지면 클릭 감지
                if (Physics.Raycast(ray, out hit))
                {
                    // 몬스터가 아닌 경우에만 이동
                    if (hit.collider.CompareTag("Ground"))
                    {
                        MoveToPosition(hit.point);
                    }
                }
            }
        }

        /// <summary>
        /// 특정 위치로 이동
        /// </summary>
        public void MoveToPosition(Vector3 targetPosition)
        {
            navAgent.SetDestination(targetPosition);
        }

        /// <summary>
        /// 이동 중지
        /// </summary>
        public void StopMovement()
        {
            navAgent.ResetPath();
        }

        /// <summary>
        /// 애니메이션 업데이트
        /// </summary>
        private void UpdateAnimation()
        {
            if (animator == null) return;

            // 이동 속도에 따라 애니메이션 설정
            float speed = navAgent.velocity.magnitude;
            animator.SetFloat("Speed", speed);
            animator.SetBool("IsMoving", speed > 0.1f);
        }

        /// <summary>
        /// 현재 이동 중인지 확인
        /// </summary>
        public bool IsMoving()
        {
            return navAgent.hasPath && navAgent.remainingDistance > navAgent.stoppingDistance;
        }

        private void OnDrawGizmos()
        {
            // 에디터에서 목적지 시각화
            if (navAgent != null && navAgent.hasPath)
            {
                Gizmos.color = Color.green;
                Gizmos.DrawWireSphere(navAgent.destination, 0.5f);
            }
        }
    }
}