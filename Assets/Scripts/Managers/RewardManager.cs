using UnityEngine;

namespace EducationalRPG.Managers
{
    /// <summary>
    /// 보상 시스템 관리 (퀴즈 정답, 몬스터 처치 등)
    /// </summary>
    public class RewardManager : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private Player.PlayerStats playerStats;
        [SerializeField] private InventoryManager inventoryManager;

        private void Start()
        {
            // 자동으로 플레이어 찾기
            if (playerStats == null)
            {
                var player = GameObject.FindGameObjectWithTag("Player");
                if (player != null)
                {
                    playerStats = player.GetComponent<Player.PlayerStats>();
                }
            }

            // 인벤토리 매니저 찾기
            if (inventoryManager == null)
            {
                inventoryManager = FindObjectOfType<InventoryManager>();
            }
        }

        /// <summary>
        /// 퀴즈 정답 보상 지급
        /// </summary>
        public void GiveQuizReward(Quiz.QuizResult result)
        {
            if (playerStats == null) return;

            // 골드 지급
            playerStats.AddGold(result.goldReward);
            Debug.Log($"Quiz reward: {result.goldReward} gold");

            // 정답일 경우 경험치도 지급
            if (result.isCorrect)
            {
                int expReward = 5; // 퀴즈 정답 시 5 경험치
                playerStats.AddExperience(expReward);
                Debug.Log($"Quiz correct! +{expReward} EXP");
            }

            // 아이템 보상
            if (result.hasItemReward && inventoryManager != null)
            {
                // TODO: 실제 아이템 데이터로 교체
                Debug.Log($"Item reward: {result.itemName}");
                // inventoryManager.AddItem(itemData);
            }
        }

        /// <summary>
        /// 몬스터 처치 보상 지급
        /// </summary>
        public void GiveEnemyReward(Enemy.EnemyStats enemyStats)
        {
            if (playerStats == null || enemyStats == null) return;

            // 골드 드롭
            int goldDrop = enemyStats.GetGoldDrop();
            playerStats.AddGold(goldDrop);

            // 경험치
            playerStats.AddExperience(enemyStats.ExperienceReward);

            Debug.Log($"Enemy defeated! +{goldDrop} gold, +{enemyStats.ExperienceReward} EXP");

            // 아이템 드롭 (확률)
            if (enemyStats.ShouldDropItem() && inventoryManager != null)
            {
                // TODO: 실제 아이템 데이터로 교체
                Debug.Log($"Item dropped from {enemyStats.EnemyName}!");
                // inventoryManager.AddItem(itemData);
            }
        }
    }
}