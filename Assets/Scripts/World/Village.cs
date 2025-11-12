using UnityEngine;
using UnityEngine.Events;

namespace EducationalRPG.World
{
    public class Village : MonoBehaviour
    {
        [Header("Village Info")]
        [SerializeField] private string villageName = "시작 마을";
        [SerializeField] private Vector3 respawnPoint;
        [SerializeField] private bool useTransformAsRespawn = true;
        
        [Header("Safe Zone")]
        [SerializeField] private float safeZoneRadius = 20f;
        [SerializeField] private bool healInSafeZone = true;
        [SerializeField] private float healRate = 5f;
        
        [Header("Events")]
        public UnityEvent OnPlayerEnter;
        public UnityEvent OnPlayerExit;
        
        private bool playerInVillage = false;
        private Combat.CombatStats playerStats;

        private void Start()
        {
            if (useTransformAsRespawn)
            {
                respawnPoint = transform.position;
            }
        }

        private void Update()
        {
            if (playerInVillage && healInSafeZone && playerStats != null)
            {
                playerStats.Heal(Mathf.RoundToInt(healRate * Time.deltaTime));
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                playerInVillage = true;
                playerStats = other.GetComponent<Combat.CombatStats>();
                
                Debug.Log($"마을 '{villageName}'에 입장했습니다!");
                OnPlayerEnter?.Invoke();
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                playerInVillage = false;
                playerStats = null;
                
                Debug.Log($"마을 '{villageName}'에서 나갔습니다!");
                OnPlayerExit?.Invoke();
            }
        }

        public Vector3 GetRespawnPoint()
        {
            return respawnPoint;
        }

        private void OnDrawGizmosSelected()
        {
            Vector3 center = useTransformAsRespawn ? transform.position : respawnPoint;
            
            Gizmos.color = new Color(0f, 1f, 0f, 0.3f);
            Gizmos.DrawSphere(center, safeZoneRadius);
            
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(center, safeZoneRadius);
        }
    }
}