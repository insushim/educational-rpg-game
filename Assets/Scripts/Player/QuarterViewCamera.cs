using UnityEngine;

namespace EducationalRPG.Player
{
    public class QuarterViewCamera : MonoBehaviour
    {
        [Header("Target Settings")]
        [SerializeField] private Transform target;
        
        [Header("Camera Settings")]
        [SerializeField] private Vector3 offset = new Vector3(0f, 10f, -8f);
        [SerializeField] private float followSpeed = 5f;
        [SerializeField] private float rotationAngle = 45f;
        [SerializeField] private float lookDownAngle = 45f;
        
        [Header("Zoom Settings")]
        [SerializeField] private float zoomSpeed = 2f;
        [SerializeField] private float minZoom = 5f;
        [SerializeField] private float maxZoom = 15f;
        
        private Vector3 currentOffset;
        private float currentZoom;

        private void Start()
        {
            if (target == null)
            {
                GameObject player = GameObject.FindGameObjectWithTag("Player");
                if (player != null)
                {
                    target = player.transform;
                }
            }
            
            currentOffset = offset;
            currentZoom = offset.magnitude;
            
            transform.rotation = Quaternion.Euler(lookDownAngle, rotationAngle, 0f);
        }

        private void LateUpdate()
        {
            if (target == null) return;
            
            HandleZoom();
            FollowTarget();
        }

        private void HandleZoom()
        {
            float scrollInput = Input.GetAxis("Mouse ScrollWheel");
            if (Mathf.Abs(scrollInput) > 0.01f)
            {
                currentZoom -= scrollInput * zoomSpeed;
                currentZoom = Mathf.Clamp(currentZoom, minZoom, maxZoom);
                
                currentOffset = offset.normalized * currentZoom;
            }
        }

        private void FollowTarget()
        {
            Quaternion rotation = Quaternion.Euler(lookDownAngle, rotationAngle, 0f);
            Vector3 desiredPosition = target.position + rotation * currentOffset;
            
            transform.position = Vector3.Lerp(
                transform.position, 
                desiredPosition, 
                followSpeed * Time.deltaTime
            );
            
            transform.LookAt(target.position + Vector3.up * 1.5f);
        }

        private void OnDrawGizmosSelected()
        {
            if (target == null) return;
            
            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(transform.position, target.position);
            Gizmos.DrawWireSphere(target.position, 0.5f);
        }
    }
}