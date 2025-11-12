using UnityEngine;
using UnityEngine.UI;
using TMPro;
using EducationalRPG.Player;
using EducationalRPG.Combat;

namespace EducationalRPG.UI
{
    public class PlayerHUD : MonoBehaviour
    {
        [Header("Health UI")]
        [SerializeField] private Slider healthBar;
        [SerializeField] private TextMeshProUGUI healthText;

        [Header("Experience UI")]
        [SerializeField] private Slider expBar;
        [SerializeField] private TextMeshProUGUI expText;
        [SerializeField] private TextMeshProUGUI levelText;

        [Header("Stats UI")]
        [SerializeField] private TextMeshProUGUI goldText;
        [SerializeField] private GameObject levelUpEffect;

        private ExperienceSystem expSystem;
        private CombatStats combatStats;

        private void Start()
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                expSystem = player.GetComponent<ExperienceSystem>();
                combatStats = player.GetComponent<CombatStats>();

                if (expSystem != null)
                {
                    expSystem.OnExpGained.AddListener(UpdateExpUI);
                    expSystem.OnLevelUp.AddListener(OnLevelUp);
                    UpdateExpUI(expSystem.CurrentExp, expSystem.ExpRequired);
                }

                if (combatStats != null)
                {
                    combatStats.OnHealthChanged.AddListener(UpdateHealthUI);
                    UpdateHealthUI(combatStats.CurrentHealth, combatStats.MaxHealth);
                }
            }

            if (levelUpEffect != null)
            {
                levelUpEffect.SetActive(false);
            }
        }

        private void UpdateHealthUI(int current, int max)
        {
            if (healthBar != null)
            {
                healthBar.maxValue = max;
                healthBar.value = current;
            }

            if (healthText != null)
            {
                healthText.text = $"HP: {current}/{max}";
            }
        }

        private void UpdateExpUI(int current, int required)
        {
            if (expBar != null)
            {
                expBar.maxValue = required;
                expBar.value = current;
            }

            if (expText != null)
            {
                expText.text = $"EXP: {current}/{required}";
            }

            if (levelText != null && expSystem != null)
            {
                levelText.text = $"Lv. {expSystem.CurrentLevel}";
            }
        }

        private void OnLevelUp(int newLevel)
        {
            if (levelText != null)
            {
                levelText.text = $"Lv. {newLevel}";
            }

            if (levelUpEffect != null)
            {
                levelUpEffect.SetActive(true);
                Invoke(nameof(HideLevelUpEffect), 2f);
            }
        }

        private void HideLevelUpEffect()
        {
            if (levelUpEffect != null)
            {
                levelUpEffect.SetActive(false);
            }
        }
    }
}