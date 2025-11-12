using UnityEngine;
using EducationalRPG.Quiz;
using EducationalRPG.Player;
using EducationalRPG.Enemy;

namespace EducationalRPG.Combat
{
    public class QuizCombatIntegration : MonoBehaviour
    {
        [Header("Quiz Settings")]
        [SerializeField] private QuizDatabase quizDatabase;
        [SerializeField] private float quizChance = 1f;
        
        [Header("Rewards")]
        [SerializeField] private float bonusExpMultiplier = 1.5f;
        [SerializeField] private int bonusGold = 50;
        
        [Header("Penalties")]
        [SerializeField] private int healthPenalty = 10;
        [SerializeField] private float expPenaltyMultiplier = 0.5f;

        public static QuizCombatIntegration Instance { get; private set; }

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        public void OnMonsterDefeated(MonsterData monsterData, CombatStats playerStats)
        {
            if (Random.value <= quizChance)
            {
                ShowQuizForMonster(monsterData, playerStats);
            }
            else
            {
                GiveNormalReward(monsterData);
            }
        }

        private void ShowQuizForMonster(MonsterData monsterData, CombatStats playerStats)
        {
            if (quizDatabase == null)
            {
                Debug.LogError("Quiz database not assigned!");
                GiveNormalReward(monsterData);
                return;
            }

            GiveNormalReward(monsterData);
        }

        private void GiveNormalReward(MonsterData monsterData)
        {
            monsterData.GiveExpToPlayer();
            Debug.Log($"Normal reward: {monsterData.ExpReward} EXP");
        }

        private void GiveQuizBonusReward(MonsterData monsterData)
        {
            int bonusExp = Mathf.RoundToInt(monsterData.ExpReward * bonusExpMultiplier);
            
            if (ExperienceSystem.Instance != null)
            {
                ExperienceSystem.Instance.GainExp(bonusExp);
            }

            if (Inventory.InventoryManager.Instance != null)
            {
                Inventory.InventoryManager.Instance.AddGold(bonusGold);
            }

            Debug.Log($"Quiz bonus: {bonusExp} EXP + {bonusGold} Gold");
        }

        private void GiveQuizPenalty(MonsterData monsterData, CombatStats playerStats)
        {
            int normalExp = Mathf.RoundToInt(monsterData.ExpReward * expPenaltyMultiplier);
            
            if (ExperienceSystem.Instance != null)
            {
                ExperienceSystem.Instance.GainExp(normalExp);
            }

            if (playerStats != null)
            {
                playerStats.TakeDamage(healthPenalty);
            }

            Debug.Log($"Quiz penalty: -{healthPenalty} HP, only {normalExp} EXP");
        }
    }
}