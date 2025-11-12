using UnityEngine;
using UnityEngine.Events;

namespace EducationalRPG.Player
{
    public class ExperienceSystem : MonoBehaviour
    {
        [Header("Level Settings")]
        [SerializeField] private int currentLevel = 1;
        [SerializeField] private int currentExp = 0;
        [SerializeField] private int maxLevel = 99;
        
        [Header("Experience Formula")]
        [SerializeField] private int baseExpRequired = 100;
        [SerializeField] private float expMultiplier = 1.5f;
        
        [Header("Level Up Rewards")]
        [SerializeField] private int healthPerLevel = 20;
        [SerializeField] private int attackPerLevel = 5;
        [SerializeField] private int defensePerLevel = 2;
        
        [Header("Events")]
        public UnityEvent<int> OnLevelUp;
        public UnityEvent<int, int> OnExpGained;
        
        public int CurrentLevel => currentLevel;
        public int CurrentExp => currentExp;
        public int ExpRequired => GetExpRequired(currentLevel);
        
        public static ExperienceSystem Instance { get; private set; }

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        public void GainExp(int amount)
        {
            if (currentLevel >= maxLevel) return;
            
            currentExp += amount;
            Debug.Log($"Gained {amount} EXP! Current: {currentExp}/{ExpRequired}");
            
            OnExpGained?.Invoke(currentExp, ExpRequired);
            
            while (currentExp >= ExpRequired && currentLevel < maxLevel)
            {
                LevelUp();
            }
        }

        private void LevelUp()
        {
            currentExp -= ExpRequired;
            currentLevel++;
            
            Debug.Log($"🎉 Level Up! Now Level {currentLevel}!");
            
            ApplyLevelUpBonuses();
            
            OnLevelUp?.Invoke(currentLevel);
            OnExpGained?.Invoke(currentExp, ExpRequired);
        }

        private void ApplyLevelUpBonuses()
        {
            var combatStats = GetComponent<Combat.CombatStats>();
            if (combatStats != null)
            {
                combatStats.Heal(9999);
            }
        }

        private int GetExpRequired(int level)
        {
            return Mathf.RoundToInt(baseExpRequired * Mathf.Pow(expMultiplier, level - 1));
        }

        public float GetExpPercentage()
        {
            return (float)currentExp / ExpRequired;
        }

        public void SaveProgress()
        {
            PlayerPrefs.SetInt("PlayerLevel", currentLevel);
            PlayerPrefs.SetInt("PlayerExp", currentExp);
            PlayerPrefs.Save();
        }

        public void LoadProgress()
        {
            currentLevel = PlayerPrefs.GetInt("PlayerLevel", 1);
            currentExp = PlayerPrefs.GetInt("PlayerExp", 0);
        }
    }
}