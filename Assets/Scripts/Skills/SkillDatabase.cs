using UnityEngine;
using System.Collections.Generic;

namespace EducationalRPG.Skills
{
    [CreateAssetMenu(fileName = "SkillDatabase", menuName = "Educational RPG/Skill Database")]
    public class SkillDatabase : ScriptableObject
    {
        public List<Skill> allSkills = new List<Skill>();

        public void InitializeSkills()
        {
            // === 전사 스킬 ===
            var slash = ScriptableObject.CreateInstance<Skill>();
            slash.skillName = "강타";
            slash.description = "적에게 강력한 일격을 가합니다";
            slash.skillType = SkillType.Attack;
            slash.requiredLevel = 1;
            slash.manaCost = 10;
            slash.cooldown = 3f;
            slash.baseDamage = 50;
            slash.damageMultiplier = 1.5f;
            slash.range = 3f;
            
            var whirlwind = ScriptableObject.CreateInstance<Skill>();
            whirlwind.skillName = "회오리 베기";
            whirlwind.description = "주변의 모든 적을 베어넘깁니다";
            whirlwind.skillType = SkillType.AOE;
            whirlwind.requiredLevel = 5;
            whirlwind.manaCost = 30;
            whirlwind.cooldown = 10f;
            whirlwind.baseDamage = 40;
            whirlwind.damageMultiplier = 1.2f;
            whirlwind.isAOE = true;
            whirlwind.aoeRadius = 5f;
            
            var rage = ScriptableObject.CreateInstance<Skill>();
            rage.skillName = "분노";
            rage.description = "20초간 공격력이 50% 증가합니다";
            rage.skillType = SkillType.Buff;
            rage.requiredLevel = 10;
            rage.manaCost = 50;
            rage.cooldown = 60f;
            rage.attackBuffMultiplier = 1.5f;
            rage.defenseBuffMultiplier = 1f;
            rage.buffDuration = 20f;
            
            var earthShatter = ScriptableObject.CreateInstance<Skill>();
            earthShatter.skillName = "대지 분쇄";
            earthShatter.description = "땅을 내리쳐 광역 피해를 입힙니다";
            earthShatter.skillType = SkillType.AOE;
            earthShatter.requiredLevel = 15;
            earthShatter.manaCost = 80;
            earthShatter.cooldown = 20f;
            earthShatter.baseDamage = 100;
            earthShatter.damageMultiplier = 2f;
            earthShatter.isAOE = true;
            earthShatter.aoeRadius = 8f;
            
            // === 마법사 스킬 ===
            var fireball = ScriptableObject.CreateInstance<Skill>();
            fireball.skillName = "화염구";
            fireball.description = "불타는 화염구를 발사합니다";
            fireball.skillType = SkillType.Attack;
            fireball.requiredLevel = 1;
            fireball.manaCost = 15;
            fireball.cooldown = 2f;
            fireball.baseDamage = 60;
            fireball.damageMultiplier = 1.8f;
            fireball.range = 10f;
            
            var iceStorm = ScriptableObject.CreateInstance<Skill>();
            iceStorm.skillName = "얼음 폭풍";
            iceStorm.description = "광범위한 얼음 폭풍을 일으킵니다";
            iceStorm.skillType = SkillType.AOE;
            iceStorm.requiredLevel = 8;
            iceStorm.manaCost = 60;
            iceStorm.cooldown = 15f;
            iceStorm.baseDamage = 50;
            iceStorm.damageMultiplier = 1.5f;
            iceStorm.isAOE = true;
            iceStorm.aoeRadius = 10f;
            
            var magicShield = ScriptableObject.CreateInstance<Skill>();
            magicShield.skillName = "마법 방패";
            magicShield.description = "30초간 방어력이 100% 증가합니다";
            magicShield.skillType = SkillType.Buff;
            magicShield.requiredLevel = 12;
            magicShield.manaCost = 70;
            magicShield.cooldown = 90f;
            magicShield.attackBuffMultiplier = 1f;
            magicShield.defenseBuffMultiplier = 2f;
            magicShield.buffDuration = 30f;
            
            var meteor = ScriptableObject.CreateInstance<Skill>();
            meteor.skillName = "메테오";
            meteor.description = "하늘에서 거대한 운석을 떨어뜨립니다";
            meteor.skillType = SkillType.AOE;
            meteor.requiredLevel = 20;
            meteor.manaCost = 150;
            meteor.cooldown = 60f;
            meteor.baseDamage = 200;
            meteor.damageMultiplier = 3f;
            meteor.isAOE = true;
            meteor.aoeRadius = 12f;
            
            // === 힐러 스킬 ===
            var heal = ScriptableObject.CreateInstance<Skill>();
            heal.skillName = "치유";
            heal.description = "HP를 200 회복합니다";
            heal.skillType = SkillType.Heal;
            heal.requiredLevel = 1;
            heal.manaCost = 20;
            heal.cooldown = 5f;
            heal.healAmount = 200;
            
            var massHeal = ScriptableObject.CreateInstance<Skill>();
            massHeal.skillName = "광역 치유";
            massHeal.description = "주변 모두의 HP를 30% 회복합니다";
            massHeal.skillType = SkillType.Heal;
            massHeal.requiredLevel = 10;
            massHeal.manaCost = 100;
            massHeal.cooldown = 30f;
            massHeal.healPercentage = 0.3f;
            
            var holyLight = ScriptableObject.CreateInstance<Skill>();
            holyLight.skillName = "성스러운 빛";
            holyLight.description = "최대 HP의 50%를 회복합니다";
            holyLight.skillType = SkillType.Heal;
            holyLight.requiredLevel = 15;
            holyLight.manaCost = 120;
            holyLight.cooldown = 45f;
            holyLight.healPercentage = 0.5f;
            
            // === 암살자 스킬 ===
            var backstab = ScriptableObject.CreateInstance<Skill>();
            backstab.skillName = "암습";
            backstab.description = "뒤에서 치명적인 일격을 가합니다";
            backstab.skillType = SkillType.Attack;
            backstab.requiredLevel = 5;
            backstab.manaCost = 25;
            backstab.cooldown = 8f;
            backstab.baseDamage = 80;
            backstab.damageMultiplier = 2.5f;
            backstab.range = 2f;
            
            var shadowStep = ScriptableObject.CreateInstance<Skill>();
            shadowStep.skillName = "그림자 이동";
            shadowStep.description = "10초간 이동속도 200% 증가";
            shadowStep.skillType = SkillType.Buff;
            shadowStep.requiredLevel = 8;
            shadowStep.manaCost = 40;
            shadowStep.cooldown = 20f;
            shadowStep.buffDuration = 10f;
            
            var poisonBlade = ScriptableObject.CreateInstance<Skill>();
            poisonBlade.skillName = "독칼";
            poisonBlade.description = "독이 묻은 칼로 적을 베어 지속 피해를 입힙니다";
            poisonBlade.skillType = SkillType.Attack;
            poisonBlade.requiredLevel = 12;
            poisonBlade.manaCost = 60;
            poisonBlade.cooldown = 12f;
            poisonBlade.baseDamage = 70;
            poisonBlade.damageMultiplier = 2f;
            poisonBlade.range = 3f;

            allSkills = new List<Skill>
            {
                slash, whirlwind, rage, earthShatter,
                fireball, iceStorm, magicShield, meteor,
                heal, massHeal, holyLight,
                backstab, shadowStep, poisonBlade
            };
        }
    }
}