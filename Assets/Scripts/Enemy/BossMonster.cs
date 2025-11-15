using UnityEngine;
using UnityEngine.Events;
using System.Collections;

namespace EducationalRPG.Enemy
{
    public class BossMonster : MonoBehaviour
    {
        [Header("Boss Info")]
        [SerializeField] private string bossName = "보스 몬스터";
        [SerializeField] private MonsterRank bossRank = MonsterRank.Boss;
        [SerializeField] private int bossLevel = 10;
        
        [Header("Boss Stats")]
        [SerializeField] private int maxHealth = 5000;
        [SerializeField] private int currentHealth;
        [SerializeField] private int attackPower = 100;
        [SerializeField] private int defense = 50;
        
        [Header("Boss Phases")]
        [SerializeField] private int currentPhase = 1;
        [SerializeField] private float phase2HealthPercent = 0.7f;
        [SerializeField] private float phase3HealthPercent = 0.3f;
        
        [Header("Boss Skills")]
        [SerializeField] private float skillCooldown = 10f;
        [SerializeField] private float ultimateSkillCooldown = 30f;
        private float lastSkillTime;
        private float lastUltimateTime;
        
        [Header("Enrage")]
        [SerializeField] private bool isEnraged = false;
        [SerializeField] private float enrageTime = 300f; // 5분
        [SerializeField] private float enrageMultiplier = 2f;
        private float combatStartTime;
        
        [Header("Rewards")] 
        [SerializeField] private int expReward = 5000;
        [SerializeField] private int goldReward = 5000;
        [SerializeField] private GameObject[] dropItems;
        
        [Header("Events")]
        public UnityEvent OnBossSpawn;
        public UnityEvent OnBossEnterPhase2;
        public UnityEvent OnBossEnterPhase3;
        public UnityEvent OnBossEnrage;
        public UnityEvent OnBossDeath;
        
        private bool isInCombat = false;
        private Combat.CombatStats combatStats;
        private MonsterAI monsterAI;

        private void Start()
        {
            currentHealth = maxHealth;
            combatStats = GetComponent<Combat.CombatStats>();
            monsterAI = GetComponent<MonsterAI>();
            
            if (combatStats != null)
            {
                combatStats.OnHealthChanged.AddListener(OnHealthChanged);
                combatStats.OnDeath.AddListener(OnDeath);
            }
            
            OnBossSpawn?.Invoke();
            ShowBossUI();
        }

        private void Update()
        {
            if (isInCombat)
            {
                CheckEnrage();
                UpdateSkills();
            }
        }

        private void OnHealthChanged(int current, int max)
        {
            currentHealth = current;
            
            float healthPercent = (float)currentHealth / maxHealth;
            
            // Phase transitions
            if (currentPhase == 1 && healthPercent <= phase2HealthPercent)
            {
                EnterPhase2();
            }
            else if (currentPhase == 2 && healthPercent <= phase3HealthPercent)
            {
                EnterPhase3();
            }
            
            UpdateBossUI();
        }

        private void EnterPhase2()
        {
            currentPhase = 2;
            attackPower = Mathf.RoundToInt(attackPower * 1.3f);
            
            OnBossEnterPhase2?.Invoke();
            Debug.Log($"{bossName} entered Phase 2! Attack increased!");
            
            // Phase 2 special effect
            UsePhase2Skill();
        }

        private void EnterPhase3()
        {
            currentPhase = 3;
            attackPower = Mathf.RoundToInt(attackPower * 1.5f);
            skillCooldown *= 0.7f; // Skills faster
            
            OnBossEnterPhase3?.Invoke();
            Debug.Log($"{bossName} entered Phase 3! DANGER!");
            
            // Phase 3 special effect
            UsePhase3Skill();
        }

        private void CheckEnrage()
        {
            if (!isEnraged && Time.time - combatStartTime >= enrageTime)
            {
                Enrage();
            }
        }

        private void Enrage()
        {
            isEnraged = true;
            attackPower = Mathf.RoundToInt(attackPower * enrageMultiplier);
            skillCooldown *= 0.5f;
            
            OnBossEnrage?.Invoke();
            Debug.Log($"{bossName} is ENRAGED! All damage doubled!");
            
            UI.GameUIManager.Instance?.ShowNotification(
                $"⚠️ {bossName}이(가) 분노했습니다!\n공격력 2배 증가!"
            );
        }

        private void UpdateSkills()
        {
            // Normal skill
            if (Time.time - lastSkillTime >= skillCooldown)
            {
                UseRandomSkill();
                lastSkillTime = Time.time;
            }
            
            // Ultimate skill
            if (Time.time - lastUltimateTime >= ultimateSkillCooldown)
            {
                UseUltimateSkill();
                lastUltimateTime = Time.time;
            }
        }

        private void UseRandomSkill()
        {
            int skillIndex = Random.Range(1, 4);
            
            switch (skillIndex)
            {
                case 1:
                    UseSkill1();
                    break;
                case 2:
                    UseSkill2();
                    break;
                case 3:
                    UseSkill3();
                    break;
            }
        }

        private void UseSkill1()
        {
            Debug.Log($"{bossName} used Skill 1: Area Attack!");
            
            // AOE damage
            Collider[] hits = Physics.OverlapSphere(transform.position, 10f);
            foreach (var hit in hits)
            {
                if (hit.CompareTag("Player"))
                {
                    var playerStats = hit.GetComponent<Combat.CombatStats>();
                    if (playerStats != null)
                    {
                        int damage = Mathf.RoundToInt(attackPower * 0.8f);
                        playerStats.TakeDamage(damage);
                    }
                }
            }
            
            UI.GameUIManager.Instance?.ShowNotification($"{bossName}: 광역 공격!");
        }

        private void UseSkill2()
        {
            Debug.Log($"{bossName} used Skill 2: Summon Minions!");
            
            // Summon minions
            for (int i = 0; i < 3; i++)
            {
                Vector3 spawnPos = transform.position + Random.insideUnitSphere * 5f;
                spawnPos.y = transform.position.y;
                
                // TODO: Spawn minion monsters
            }
            
            UI.GameUIManager.Instance?.ShowNotification($"{bossName}: 부하 소환!");
        }

        private void UseSkill3()
        {
            Debug.Log($"{bossName} used Skill 3: Powerful Strike!");
            
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                var playerStats = player.GetComponent<Combat.CombatStats>();
                if (playerStats != null)
                {
                    int damage = Mathf.RoundToInt(attackPower * 1.5f);
                    playerStats.TakeDamage(damage);
                }
            }
            
            UI.GameUIManager.Instance?.ShowNotification($"{bossName}: 강력한 일격!");
        }

        private void UsePhase2Skill()
        {
            Debug.Log($"{bossName} Phase 2 Special: Rage Mode!");
            attackPower = Mathf.RoundToInt(attackPower * 1.2f);
            
            UI.GameUIManager.Instance?.ShowNotification(
                $"⚠️ {bossName} 페이즈 2 돌입!\n공격력 증가!"
            );
        }

        private void UsePhase3Skill()
        {
            Debug.Log($"{bossName} Phase 3 Special: Desperate Attack!");
            
            // Heal 20%
            int healAmount = Mathf.RoundToInt(maxHealth * 0.2f);
            if (combatStats != null)
            {
                combatStats.Heal(healAmount);
            }
            
            UI.GameUIManager.Instance?.ShowNotification(
                $"💀 {bossName} 최종 페이즈!\n체력 20% 회복!"
            );
        }

        private void UseUltimateSkill()
        {
            Debug.Log($"{bossName} used ULTIMATE SKILL!");
            
            // Massive AOE damage
            Collider[] hits = Physics.OverlapSphere(transform.position, 15f);
            foreach (var hit in hits)
            {
                if (hit.CompareTag("Player"))
                {
                    var playerStats = hit.GetComponent<Combat.CombatStats>();
                    if (playerStats != null)
                    {
                        int damage = Mathf.RoundToInt(attackPower * 2f);
                        playerStats.TakeDamage(damage);
                    }
                }
            }
            
            UI.GameUIManager.Instance?.ShowNotification(
                $"💥 {bossName}: 궁극기 발동!\n즉시 대피하세요!"
            );
        }

        private void OnDeath()
        {
            Debug.Log($"{bossName} has been defeated!");
            
            GiveRewards();
            OnBossDeath?.Invoke();
            HideBossUI();
            
            UI.GameUIManager.Instance?.ShowNotification(
                $"🎉 {bossName} 처치 완료!\n+{expReward} EXP\n+{goldReward} Gold"
            );
            
            // Drop items
            DropLoot();
            
            Destroy(gameObject, 3f);
        }

        private void GiveRewards()
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                var expSystem = player.GetComponent<Player.ExperienceSystem>();
                if (expSystem != null)
                {
                    expSystem.GainExp(expReward);
                }
                
                if (Inventory.InventoryManager.Instance != null)
                {
                    Inventory.InventoryManager.Instance.AddGold(goldReward);
                }
            }
        }

        private void DropLoot()
        {
            if (dropItems != null && dropItems.Length > 0)
            {
                foreach (var itemPrefab in dropItems)
                {
                    if (itemPrefab != null)
                    {
                        Vector3 dropPos = transform.position + Random.insideUnitSphere * 2f;
                        dropPos.y = transform.position.y + 1f;
                        Instantiate(itemPrefab, dropPos, Quaternion.identity);
                    }
                }
            }
        }

        public void StartCombat()
        {
            if (!isInCombat)
            {
                isInCombat = true;
                combatStartTime = Time.time;
                lastSkillTime = Time.time;
                lastUltimateTime = Time.time;
                
                Debug.Log($"Boss combat started: {bossName}");
            }
        }

        private void ShowBossUI()
        {
            // TODO: Show boss health bar UI
            Debug.Log($"Boss UI shown for {bossName}");
        }

        private void UpdateBossUI()
        {
            // TODO: Update boss health bar
        }

        private void HideBossUI()
        {
            // TODO: Hide boss health bar UI
            Debug.Log($"Boss UI hidden for {bossName}");
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                StartCombat();
            }
        }
    }
}