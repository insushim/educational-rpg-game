using UnityEngine;
using System.Collections.Generic;

namespace EducationalRPG.Enemy
{
    public enum MonsterRank
    {
        Normal,      // 일반
        Elite,       // 정예
        Boss,        // 보스
        WorldBoss    // 월드 보스
    }

    public enum MonsterElement
    {
        None,
        Fire,
        Ice,
        Lightning,
        Dark,
        Holy
    }

    [System.Serializable]
    public class MonsterData
    {
        public string monsterName;
        public MonsterRank rank;
        public MonsterElement element;
        
        [Header("Base Stats")]
        public int level;
        public int baseHealth;
        public int baseAttack;
        public int baseDefense;
        public float moveSpeed;
        public float attackSpeed;
        
        [Header("Scaling")]
        public float healthPerLevel;
        public float attackPerLevel;
        public float defensePerLevel;
        
        [Header("Rewards")]
        public int baseExp;
        public int baseGold;
        public List<string> dropItems;
        public float dropChance;
        
        [Header("Special Abilities")]
        public List<string> skills;
        public bool canFly;
        public bool canTeleport;
        
        [Header("Visual")]
        public GameObject prefab;
        public float size;
        public Color glowColor;
    }

    [CreateAssetMenu(fileName = "MonsterDatabase", menuName = "Educational RPG/Monster Database")]
    public class MonsterDatabase : ScriptableObject
    {
        public List<MonsterData> monsters = new List<MonsterData>()
        {
            // === 레벨 1-5: 초보자 몬스터 ===
            new MonsterData
            {
                monsterName = "슬라임",
                rank = MonsterRank.Normal,
                element = MonsterElement.None,
                level = 1,
                baseHealth = 50,
                baseAttack = 5,
                baseDefense = 2,
                moveSpeed = 2f,
                attackSpeed = 1.5f,
                healthPerLevel = 10f,
                attackPerLevel = 2f,
                defensePerLevel = 1f,
                baseExp = 20,
                baseGold = 10,
                dropItems = new List<string> { "슬라임 젤리", "작은 회복 포션" },
                dropChance = 0.3f,
                skills = new List<string>(),
                size = 1f,
                glowColor = Color.green
            },
            new MonsterData
            {
                monsterName = "토끼",
                rank = MonsterRank.Normal,
                element = MonsterElement.None,
                level = 2,
                baseHealth = 40,
                baseAttack = 8,
                baseDefense = 1,
                moveSpeed = 4f,
                attackSpeed = 2f,
                healthPerLevel = 8f,
                attackPerLevel = 3f,
                defensePerLevel = 0.5f,
                baseExp = 25,
                baseGold = 15,
                dropItems = new List<string> { "토끼 가죽", "토끼 발" },
                dropChance = 0.25f,
                skills = new List<string> { "빠른 회피" },
                size = 0.8f,
                glowColor = Color.white
            },
            new MonsterData
            {
                monsterName = "늑대",
                rank = MonsterRank.Normal,
                element = MonsterElement.None,
                level = 4,
                baseHealth = 80,
                baseAttack = 15,
                baseDefense = 5,
                moveSpeed = 5f,
                attackSpeed = 1.8f,
                healthPerLevel = 15f,
                attackPerLevel = 5f,
                defensePerLevel = 2f,
                baseExp = 40,
                baseGold = 25,
                dropItems = new List<string> { "늑대 가죽", "날카로운 이빨" },
                dropChance = 0.4f,
                skills = new List<string> { "물어뜯기", "무리 소환" },
                size = 1.2f,
                glowColor = new Color(0.7f, 0.7f, 0.7f)
            },
            
            // === 레벨 5-10: 중급 몬스터 ===
            new MonsterData
            {
                monsterName = "박쥐",
                rank = MonsterRank.Normal,
                element = MonsterElement.Dark,
                level = 6,
                baseHealth = 60,
                baseAttack = 20,
                baseDefense = 3,
                moveSpeed = 6f,
                attackSpeed = 2.5f,
                healthPerLevel = 12f,
                attackPerLevel = 6f,
                defensePerLevel = 1f,
                baseExp = 50,
                baseGold = 30,
                dropItems = new List<string> { "박쥐 날개", "어둠의 정수" },
                dropChance = 0.35f,
                skills = new List<string> { "흡혈", "초음파" },
                canFly = true,
                size = 0.9f,
                glowColor = new Color(0.3f, 0f, 0.3f)
            },
            new MonsterData
            {
                monsterName = "거미",
                rank = MonsterRank.Normal,
                element = MonsterElement.None,
                level = 7,
                baseHealth = 90,
                baseAttack = 18,
                baseDefense = 8,
                moveSpeed = 3.5f,
                attackSpeed = 2f,
                healthPerLevel = 18f,
                attackPerLevel = 5f,
                defensePerLevel = 3f,
                baseExp = 60,
                baseGold = 35,
                dropItems = new List<string> { "거미줄", "독 주머니" },
                dropChance = 0.4f,
                skills = new List<string> { "거미줄 덫", "독 공격" },
                size = 1.3f,
                glowColor = new Color(0.5f, 0f, 0f)
            },
            new MonsterData
            {
                monsterName = "고블린",
                rank = MonsterRank.Normal,
                element = MonsterElement.None,
                level = 8,
                baseHealth = 120,
                baseAttack = 25,
                baseDefense = 10,
                moveSpeed = 4f,
                attackSpeed = 1.5f,
                healthPerLevel = 20f,
                attackPerLevel = 7f,
                defensePerLevel = 3f,
                baseExp = 70,
                baseGold = 50,
                dropItems = new List<string> { "고블린 칼", "동전 주머니" },
                dropChance = 0.45f,
                skills = new List<string> { "도적질", "비겁한 공격" },
                size = 1.1f,
                glowColor = new Color(0f, 0.6f, 0f)
            },
            
            // === 레벨 8-13: 정예 몬스터 ===
            new MonsterData
            {
                monsterName = "독버섯",
                rank = MonsterRank.Elite,
                element = MonsterElement.None,
                level = 10,
                baseHealth = 200,
                baseAttack = 30,
                baseDefense = 15,
                moveSpeed = 2f,
                attackSpeed = 1.2f,
                healthPerLevel = 30f,
                attackPerLevel = 8f,
                defensePerLevel = 5f,
                baseExp = 100,
                baseGold = 80,
                dropItems = new List<string> { "독버섯 포자", "마법 버섯", "해독제" },
                dropChance = 0.6f,
                skills = new List<string> { "독 포자 분사", "치명독" },
                size = 1.5f,
                glowColor = new Color(0.8f, 0f, 0.8f)
            },
            new MonsterData
            {
                monsterName = "맹독 슬라임",
                rank = MonsterRank.Elite,
                element = MonsterElement.None,
                level = 11,
                baseHealth = 180,
                baseAttack = 35,
                baseDefense = 12,
                moveSpeed = 3f,
                attackSpeed = 1.8f,
                healthPerLevel = 28f,
                attackPerLevel = 9f,
                defensePerLevel = 4f,
                baseExp = 110,
                baseGold = 90,
                dropItems = new List<string> { "맹독 젤리", "독성 핵" },
                dropChance = 0.55f,
                skills = new List<string> { "독액 분사", "분열" },
                size = 1.4f,
                glowColor = new Color(0.5f, 0f, 0.5f)
            },
            
            // === 레벨 12-18: 고급 몬스터 ===
            new MonsterData
            {
                monsterName = "파이어 엘리멘탈",
                rank = MonsterRank.Elite,
                element = MonsterElement.Fire,
                level = 15,
                baseHealth = 300,
                baseAttack = 50,
                baseDefense = 20,
                moveSpeed = 4f,
                attackSpeed = 2f,
                healthPerLevel = 40f,
                attackPerLevel = 12f,
                defensePerLevel = 6f,
                baseExp = 150,
                baseGold = 120,
                dropItems = new List<string> { "화염 정수", "불꽃 결정" },
                dropChance = 0.7f,
                skills = new List<string> { "화염구", "불기둥" },
                size = 1.8f,
                glowColor = new Color(1f, 0.3f, 0f)
            },
            new MonsterData
            {
                monsterName = "용암 골렘",
                rank = MonsterRank.Elite,
                element = MonsterElement.Fire,
                level = 16,
                baseHealth = 400,
                baseAttack = 45,
                baseDefense = 30,
                moveSpeed = 2.5f,
                attackSpeed = 1.2f,
                healthPerLevel = 50f,
                attackPerLevel = 10f,
                defensePerLevel = 8f,
                baseExp = 170,
                baseGold = 140,
                dropItems = new List<string> { "용암석", "골렘 핵" },
                dropChance = 0.65f,
                skills = new List<string> { "지진", "용암 분출" },
                size = 2.5f,
                glowColor = new Color(1f, 0.5f, 0f)
            },
            
            // === 보스 몬스터 ===
            new MonsterData
            {
                monsterName = "동굴 지배자",
                rank = MonsterRank.Boss,
                element = MonsterElement.Dark,
                level = 10,
                baseHealth = 800,
                baseAttack = 50,
                baseDefense = 30,
                moveSpeed = 3f,
                attackSpeed = 1.5f,
                healthPerLevel = 100f,
                attackPerLevel = 15f,
                defensePerLevel = 10f,
                baseExp = 500,
                baseGold = 500,
                dropItems = new List<string> { "지배자의 왕관", "전설 무기", "보스 상자" },
                dropChance = 1f,
                skills = new List<string> { "어둠의 포효", "그림자 소환", "광란" },
                size = 3f,
                glowColor = new Color(0.5f, 0f, 0.5f)
            },
            new MonsterData
            {
                monsterName = "버섯 왕",
                rank = MonsterRank.Boss,
                element = MonsterElement.None,
                level = 13,
                baseHealth = 1200,
                baseAttack = 60,
                baseDefense = 40,
                moveSpeed = 2f,
                attackSpeed = 1.3f,
                healthPerLevel = 120f,
                attackPerLevel = 18f,
                defensePerLevel = 12f,
                baseExp = 700,
                baseGold = 700,
                dropItems = new List<string> { "왕의 버섯", "전설 갑옷", "보스 상자" },
                dropChance = 1f,
                skills = new List<string> { "맹독 폭발", "포자 구름", "재생" },
                size = 4f,
                glowColor = new Color(0.8f, 0.2f, 0.8f)
            },
            new MonsterData
            {
                monsterName = "화염 드래곤",
                rank = MonsterRank.Boss,
                element = MonsterElement.Fire,
                level = 18,
                baseHealth = 2000,
                baseAttack = 80,
                baseDefense = 50,
                moveSpeed = 5f,
                attackSpeed = 1.8f,
                healthPerLevel = 150f,
                attackPerLevel = 20f,
                defensePerLevel = 15f,
                baseExp = 1000,
                baseGold = 1000,
                dropItems = new List<string> { "드래곤 비늘", "드래곤 심장", "전설 무기", "보스 상자" },
                dropChance = 1f,
                skills = new List<string> { "화염 브레스", "비행 돌격", "용의 분노" },
                canFly = true,
                size = 5f,
                glowColor = new Color(1f, 0f, 0f)
            },
            new MonsterData
            {
                monsterName = "마왕",
                rank = MonsterRank.WorldBoss,
                element = MonsterElement.Dark,
                level = 30,
                baseHealth = 5000,
                baseAttack = 150,
                baseDefense = 100,
                moveSpeed = 4f,
                attackSpeed = 2f,
                healthPerLevel = 200f,
                attackPerLevel = 30f,
                defensePerLevel = 20f,
                baseExp = 5000,
                baseGold = 5000,
                dropItems = new List<string> { "마왕의 심장", "파멸의 검", "신화 갑옷", "전설 상자" },
                dropChance = 1f,
                skills = new List<string> { "어둠의 심판", "차원 이동", "지옥의 불꽃", "시간 정지" },
                canTeleport = true,
                size = 6f,
                glowColor = new Color(0.5f, 0f, 0f)
            }
        };
    }
}