using UnityEngine;

namespace EducationalRPG.Player
{
    /// <summary>
    /// 플레이어의 스탯을 관리하는 클래스
    /// </summary>
    public class PlayerStats : MonoBehaviour
    {
        [Header("Player Stats")]
        [SerializeField] private int maxHealth = 100;
        [SerializeField] private int maxMana = 50;
        [SerializeField] private int currentLevel = 1;
        [SerializeField] private int attackPower = 10;
        [SerializeField] private int defense = 5;

        [Header("Resources")]
        [SerializeField] private int gold = 0;
        [SerializeField] private int experience = 0;
        [SerializeField] private int experienceToNextLevel = 100;

        private int currentHealth;
        private int currentMana;

        // Events
        public System.Action<int, int> OnHealthChanged;
        public System.Action<int, int> OnManaChanged;
        public System.Action<int> OnGoldChanged;
        public System.Action<int> OnLevelUp;

        // Properties
        public int CurrentHealth => currentHealth;
        public int MaxHealth => maxHealth;
        public int CurrentMana => currentMana;
        public int MaxMana => maxMana;
        public int CurrentLevel => currentLevel;
        public int AttackPower => attackPower;
        public int Defense => defense;
        public int Gold => gold;
        public int Experience => experience;

        private void Awake()
        {
            currentHealth = maxHealth;
            currentMana = maxMana;
        }

        #region Health Management
        public void TakeDamage(int damage)
        {
            int actualDamage = Mathf.Max(1, damage - defense);
            currentHealth = Mathf.Max(0, currentHealth - actualDamage);
            OnHealthChanged?.Invoke(currentHealth, maxHealth);

            if (currentHealth <= 0)
            {
                Die();
            }
        }

        public void Heal(int amount)
        {
            currentHealth = Mathf.Min(maxHealth, currentHealth + amount);
            OnHealthChanged?.Invoke(currentHealth, maxHealth);
        }

        private void Die()
        {
            Debug.Log("Player died!");
            // 사망 처리 로직
        }
        #endregion

        #region Mana Management
        public bool UseMana(int amount)
        {
            if (currentMana >= amount)
            {
                currentMana -= amount;
                OnManaChanged?.Invoke(currentMana, maxMana);
                return true;
            }
            return false;
        }

        public void RestoreMana(int amount)
        {
            currentMana = Mathf.Min(maxMana, currentMana + amount);
            OnManaChanged?.Invoke(currentMana, maxMana);
        }
        #endregion

        #region Gold and Experience
        public void AddGold(int amount)
        {
            gold += amount;
            OnGoldChanged?.Invoke(gold);
            Debug.Log($"Gold earned: {amount}. Total gold: {gold}");
        }

        public bool SpendGold(int amount)
        {
            if (gold >= amount)
            {
                gold -= amount;
                OnGoldChanged?.Invoke(gold);
                return true;
            }
            return false;
        }

        public void AddExperience(int amount)
        {
            experience += amount;

            while (experience >= experienceToNextLevel)
            {
                LevelUp();
            }
        }

        private void LevelUp()
        {
            currentLevel++;
            experience -= experienceToNextLevel;
            experienceToNextLevel = Mathf.RoundToInt(experienceToNextLevel * 1.5f);

            // 스탯 증가
            maxHealth += 10;
            maxMana += 5;
            attackPower += 2;
            defense += 1;

            // 체력/마나 회복
            currentHealth = maxHealth;
            currentMana = maxMana;

            OnLevelUp?.Invoke(currentLevel);
            Debug.Log($"Level Up! Now level {currentLevel}");
        }
        #endregion
    }
}