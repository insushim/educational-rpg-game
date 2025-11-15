using UnityEngine;
using System.Collections.Generic;

namespace EducationalRPG.Skills
{
    public enum SkillType
    {
        Attack,
        Buff,
        Heal,
        AOE
    }

    [CreateAssetMenu(fileName = "New Skill", menuName = "Educational RPG/Skill")]
    public class Skill : ScriptableObject
    {
        [Header("Basic Info")]
        public string skillName;
        [TextArea(2, 4)]
        public string description;
        public Sprite icon;
        public SkillType skillType;
        
        [Header("Requirements")]
        public int requiredLevel = 1;
        public int manaCost = 10;
        public float cooldown = 5f;
        
        [Header("Damage")]
        public int baseDamage = 50;
        public float damageMultiplier = 1.5f;
        public float range = 5f;
        
        [Header("AOE")]
        public bool isAOE = false;
        public float aoeRadius = 3f;
        
        [Header("Effects")]
        public GameObject skillEffect;
        public AudioClip skillSound;
        public float effectDuration = 1f;
        
        [Header("Buffs")]
        public float attackBuffMultiplier = 1f;
        public float defenseBuffMultiplier = 1f;
        public float buffDuration = 0f;
        
        [Header("Healing")]
        public int healAmount = 0;
        public float healPercentage = 0f;

        public virtual bool CanUse(GameObject caster)
        {
            var playerCombat = caster.GetComponent<Player.PlayerCombat>();
            if (playerCombat != null)
            {
                var expSystem = caster.GetComponent<Player.ExperienceSystem>();
                if (expSystem != null && expSystem.CurrentLevel < requiredLevel)
                {
                    Debug.Log($"Level {requiredLevel} required for {skillName}");
                    return false;
                }
                
                return true;
            }
            return false;
        }

        public virtual void Use(GameObject caster, Vector3 targetPosition)
        {
            if (!CanUse(caster)) return;

            switch (skillType)
            {
                case SkillType.Attack:
                    UseAttackSkill(caster, targetPosition);
                    break;
                case SkillType.Buff:
                    UseBuffSkill(caster);
                    break;
                case SkillType.Heal:
                    UseHealSkill(caster);
                    break;
                case SkillType.AOE:
                    UseAOESkill(caster, targetPosition);
                    break;
            }

            PlayEffects(targetPosition);
        }

        protected virtual void UseAttackSkill(GameObject caster, Vector3 targetPosition)
        {
            Collider[] hits = Physics.OverlapSphere(targetPosition, range);
            
            foreach (var hit in hits)
            {
                if (hit.CompareTag("Enemy"))
                {
                    var enemyStats = hit.GetComponent<Combat.CombatStats>();
                    if (enemyStats != null)
                    {
                        int damage = Mathf.RoundToInt(baseDamage * damageMultiplier);
                        enemyStats.TakeDamage(damage);
                        Debug.Log($"{skillName} dealt {damage} damage!");
                    }
                    break;
                }
            }
        }

        protected virtual void UseAOESkill(GameObject caster, Vector3 targetPosition)
        {
            Collider[] hits = Physics.OverlapSphere(targetPosition, aoeRadius);
            int enemiesHit = 0;
            
            foreach (var hit in hits)
            {
                if (hit.CompareTag("Enemy"))
                {
                    var enemyStats = hit.GetComponent<Combat.CombatStats>();
                    if (enemyStats != null)
                    {
                        int damage = Mathf.RoundToInt(baseDamage * damageMultiplier);
                        enemyStats.TakeDamage(damage);
                        enemiesHit++;
                    }
                }
            }
            
            Debug.Log($"{skillName} hit {enemiesHit} enemies!");
        }

        protected virtual void UseBuffSkill(GameObject caster)
        {
            var buffSystem = caster.GetComponent<Combat.BuffSystem>();
            if (buffSystem != null && buffDuration > 0f)
            {
                var tempItem = ScriptableObject.CreateInstance<Inventory.Item>();
                tempItem.buffDuration = buffDuration;
                tempItem.attackBuffMultiplier = attackBuffMultiplier;
                tempItem.defenseBuffMultiplier = defenseBuffMultiplier;
                tempItem.itemName = skillName;
                
                buffSystem.ApplyBuff(tempItem);
                Debug.Log($"Applied {skillName} buff for {buffDuration}s");
            }
        }

        protected virtual void UseHealSkill(GameObject caster)
        {
            var combatStats = caster.GetComponent<Combat.CombatStats>();
            if (combatStats != null)
            {
                int totalHeal = healAmount;
                
                if (healPercentage > 0f)
                {
                    totalHeal += Mathf.RoundToInt(combatStats.MaxHealth * healPercentage);
                }
                
                combatStats.Heal(totalHeal);
                Debug.Log($"{skillName} healed {totalHeal} HP");
            }
        }

        protected virtual void PlayEffects(Vector3 position)
        {
            if (skillEffect != null)
            {
                GameObject effect = Instantiate(skillEffect, position, Quaternion.identity);
                Destroy(effect, effectDuration);
            }

            if (skillSound != null)
            {
                AudioSource.PlayClipAtPoint(skillSound, position);
            }
        }
    }
}