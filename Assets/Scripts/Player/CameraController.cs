using UnityEngine;

namespace EducationalRPG.Player
{
    /// <summary>
    /// 리니지 스타일의 쿼터뷰 카메라 컨트롤러
    /// </summary>
    public class CameraController : MonoBehaviour
    {
        [Header("Target")]
        [SerializeField] private Transform target;

        [Header("Camera Settings")]
        [SerializeField] private float distance = 10f;
        [SerializeField] private float height = 8f;
        [SerializeField] private float angle = 45f;
        [SerializeField] private float smoothSpeed = 5f;

        [Header("Zoom Settings")]
        [SerializeField] private float minDistance = 5f;
        [SerializeField] private float maxDistance = 15f;
        [SerializeField] private float zoomSpeed = 2f;

        private Vector3 offset;

        private void Start()
        {
            if (target == null)
            {
                // 플레이어 자동 찾기
                var player = GameObject.FindGameObjectWithTag("Player");
                if (player != null)
                {
                    target = player.transform;
                }
            }

            CalculateOffset();
        }

        private void LateUpdate()
        {
            if (target == null) return;

            HandleZoom();
            FollowTarget();
        }

        /// <summary>
        /// 카메라 오프셋 계산
        /// </summary>
        private void CalculateOffset()
        {
            // 쿼터뷰 각도로 오프셋 계산
            float angleInRadians = angle * Mathf.Deg2Rad;
            offset = new Vector3(0, height, -distance);
        }

        /// <summary>
        /// 타겟 따라가기
        /// </summary>
        private void FollowTarget()
        {
            Vector3 desiredPosition = target.position + offset;
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
            transform.position = smoothedPosition;

            // 타겟을 바라보기
            transform.LookAt(target.position + Vector3.up * 1.5f);
        }

        /// <summary>
        /// 줌 인/아웃 처리
        /// </summary>
        private void HandleZoom()
        {
            float scroll = Input.GetAxis("Mouse ScrollWheel");
            if (scroll != 0)
            {
                distance -= scroll * zoomSpeed;
                distance = Mathf.Clamp(distance, minDistance, maxDistance);
                CalculateOffset();
            }
        }

        /// <summary>
        /// 타겟 설정
        /// </summary>
        public void SetTarget(Transform newTarget)
        {
            target = newTarget;
        }
    }
}