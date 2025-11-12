using UnityEngine;
using EducationalRPG.Player;

namespace EducationalRPG.Enemy
{
    public class MonsterData : MonoBehaviour
    {
        [Header("Monster Info")] 
        [SerializeField] private string monsterName = "Slime";
        [SerializeField] private int level = 1;
        [SerializeField] private int expReward = 50;
        
        [Header("Level Scaling")] 
        [SerializeField] private int baseHealth = 50;
        [SerializeField] private int baseAttack = 10;
        [SerializeField] private float healthPerLevel = 10f;
        [SerializeField] private float attackPerLevel = 2f;

        public string MonsterName => monsterName;
        public int Level => level;
        public int ExpReward => expReward;

        private void Start()
        {
            ApplyLevelScaling();
        }

        public void SetLevel(int newLevel)
        {
            level = newLevel;
            ApplyLevelScaling();
        }

        public void SetExpReward(int exp)
        {
            expReward = exp;
        }

        private void ApplyLevelScaling()
        {
            var combatStats = GetComponent<Combat.CombatStats>();
            if (combatStats != null)
            {
                int scaledHealth = baseHealth + Mathf.RoundToInt(healthPerLevel * (level - 1));
            }
        }

        public void GiveExpToPlayer()
        {
            if (ExperienceSystem.Instance != null)
            {
                ExperienceSystem.Instance.GainExp(expReward);
            }
        }
    }
}