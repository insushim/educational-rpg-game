using UnityEngine;
using System.Collections.Generic;

namespace EducationalRPG.World
{
    public class HuntingGround : MonoBehaviour
    {
        [Header("Hunting Ground Info")]
        [SerializeField] private string groundName = "초보자 사냥터";
        [SerializeField] private int recommendedLevel = 1;
        [SerializeField] private int maxLevel = 10;
        
        [Header("Monster Spawners")]
        [SerializeField] private List<Enemy.MonsterSpawner> spawners = new List<Enemy.MonsterSpawner>();
        [SerializeField] private bool autoFindSpawners = true;
        
        [Header("Danger Zone")]
        [SerializeField] private float dangerLevel = 1f;
        [SerializeField] private Color groundColor = Color.red;
        
        private bool playerInGround = false;

        private void Start()
        {
            if (autoFindSpawners)
            {
                spawners.AddRange(GetComponentsInChildren<Enemy.MonsterSpawner>());
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                playerInGround = true;
                
                var expSystem = other.GetComponent<Player.ExperienceSystem>();
                int playerLevel = expSystem != null ? expSystem.CurrentLevel : 1;
                
                string message = $"{groundName} (추천 레벨: {recommendedLevel}~{maxLevel})";
                
                if (playerLevel < recommendedLevel)
                {
                    message += "\n⚠️ 레벨이 낮습니다! 주의하세요!";
                }
                else if (playerLevel > maxLevel)
                {
                    message += "\n경험치 획득량이 감소합니다.";
                }
                
                Debug.Log(message);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                playerInGround = false;
            }
        }

        public bool IsAppropriateLevel(int playerLevel)
        {
            return playerLevel >= recommendedLevel && playerLevel <= maxLevel;
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = new Color(groundColor.r, groundColor.g, groundColor.b, 0.2f);
            
            BoxCollider boxCollider = GetComponent<BoxCollider>();
            if (boxCollider != null)
            {
                Gizmos.matrix = transform.localToWorldMatrix;
                Gizmos.DrawCube(boxCollider.center, boxCollider.size);
                
                Gizmos.color = groundColor;
                Gizmos.DrawWireCube(boxCollider.center, boxCollider.size);
            }
        }
    }
}