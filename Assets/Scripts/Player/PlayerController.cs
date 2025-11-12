using UnityEngine;

namespace EducationalRPG.Player
{
    [RequireComponent(typeof(CharacterController))]
    public class PlayerController : MonoBehaviour
    {
        [Header("Movement Settings")]
        [SerializeField] private float moveSpeed = 5f;
        [SerializeField] private float runSpeed = 8f;
        [SerializeField] private float rotationSpeed = 10f;
        
        [Header("Animation")]
        [SerializeField] private Animator animator;
        
        private CharacterController characterController;
        private Vector3 moveDirection;
        private bool isRunning;
        
        private static readonly int SpeedHash = Animator.StringToHash("Speed");
        private static readonly int IsRunningHash = Animator.StringToHash("IsRunning");

        private void Awake()
        {
            characterController = GetComponent<CharacterController>();
            
            if (animator == null)
            {
                animator = GetComponentInChildren<Animator>();
            }
        }

        private void Update()
        {
            HandleInput();
            Move();
            Rotate();
            UpdateAnimation();
        }

        private void HandleInput()
        {
            float horizontal = Input.GetAxisRaw("Horizontal");
            float vertical = Input.GetAxisRaw("Vertical");
            
            isRunning = Input.GetKey(KeyCode.LeftShift);
            
            Camera mainCamera = Camera.main;
            if (mainCamera != null)
            {
                Vector3 forward = mainCamera.transform.forward;
                Vector3 right = mainCamera.transform.right;
                
                forward.y = 0f;
                right.y = 0f;
                forward.Normalize();
                right.Normalize();
                
                moveDirection = (forward * vertical + right * horizontal).normalized;
            }
            else
            {
                moveDirection = new Vector3(horizontal, 0f, vertical).normalized;
            }
        }

        private void Move()
        {
            if (moveDirection.magnitude < 0.1f) return;
            
            float currentSpeed = isRunning ? runSpeed : moveSpeed;
            Vector3 motion = moveDirection * currentSpeed * Time.deltaTime;
            
            if (!characterController.isGrounded)
            {
                motion.y = -9.81f * Time.deltaTime;
            }
            
            characterController.Move(motion);
        }

        private void Rotate()
        {
            if (moveDirection.magnitude < 0.1f) return;
            
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            transform.rotation = Quaternion.Slerp(
                transform.rotation, 
                targetRotation, 
                rotationSpeed * Time.deltaTime
            );
        }

        private void UpdateAnimation()
        {
            if (animator == null) return;
            
            float speed = moveDirection.magnitude;
            animator.SetFloat(SpeedHash, speed);
            animator.SetBool(IsRunningHash, isRunning && speed > 0.1f);
        }

        public void MoveToPosition(Vector3 targetPosition)
        {
            Vector3 direction = (targetPosition - transform.position).normalized;
            direction.y = 0f;
            moveDirection = direction;
        }
    }
}