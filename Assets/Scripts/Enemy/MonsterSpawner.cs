using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EducationalRPG.World;

namespace EducationalRPG.Enemy
{
    // Spawns monsters based on HuntingGroundData and MonsterDatabase
    public class MonsterSpawner : MonoBehaviour
    {
        [Header("Databases")]
        public MonsterDatabase monsterDatabase;
        public HuntingGroundDatabase huntingGroundDatabase;

        [Header("Spawner Settings")]
        public int minSpawn = 3;
        public int maxSpawn = 8;
        public float spawnInterval = 20f; // seconds between waves
        [Range(0,100)] public int eliteChancePercent = 12; // chance (0-100) to spawn an elite instead of normal
        [Range(0,100)] public int championChancePercent = 3; // chance to spawn champion variant
        [Range(0,100)] public int roamingBossChancePercent = 2; // chance for a roaming boss per wave

        [Header("References")]
        public int huntingGroundIndex = 0; // which hunting ground to use from DB
        public List<Transform> spawnPoints = new List<Transform>();

        private HuntingGroundData groundData;

        private void Start()
        {
            if (huntingGroundDatabase != null && huntingGroundDatabase.huntingGrounds.Count > huntingGroundIndex)
                groundData = huntingGroundDatabase.huntingGrounds[huntingGroundIndex];
            else
                Debug.LogWarning("Hunting ground database or index invalid in MonsterSpawner.");

            if (monsterDatabase == null)
                Debug.LogWarning("MonsterDatabase not assigned in MonsterSpawner.");

            StartCoroutine(SpawnLoop());
        }

        private IEnumerator SpawnLoop()
        {
            while (true)
            {
                SpawnWave();
                yield return new WaitForSeconds(spawnInterval);
            }
        }

        private void SpawnWave()
        {
            if (monsterDatabase == null || groundData == null) return;

            int toSpawn = Random.Range(minSpawn, maxSpawn + 1);
            for (int i = 0; i < toSpawn; i++)
            {
                SpawnOne();
            }

            // occasional roaming boss spawn (rare)
            if (groundData.hasBoss && Random.Range(0, 100) < roamingBossChancePercent)
            {
                SpawnBossByName(groundData.bossName);
            }
        }

        private void SpawnOne()
        {
            Transform sp = spawnPoints.Count > 0 ? spawnPoints[Random.Range(0, spawnPoints.Count)] : transform;
            if (groundData.monsterTypes == null || groundData.monsterTypes.Count == 0) return;

            string chosenName = groundData.monsterTypes[Random.Range(0, groundData.monsterTypes.Count)];
            // Find candidates by name (could be multiple variants)
            List<MonsterData> candidates = monsterDatabase.monsters.FindAll(m => m.monsterName == chosenName);

            if (candidates.Count == 0)
            {
                // fallback: choose any monster in level range
                candidates = monsterDatabase.monsters.FindAll(m => m.level >= groundData.minLevel && m.level <= groundData.maxLevel && m.rank == MonsterRank.Normal);
                if (candidates.Count == 0) return;
            }

            MonsterData baseMonster = candidates[Random.Range(0, candidates.Count)];
            MonsterData spawnData = baseMonster;

            // chance to upgrade to champion/elite
            int roll = Random.Range(0, 100);
            if (roll < championChancePercent)
            {
                MonsterData champion = monsterDatabase.monsters.Find(m => m.rank == MonsterRank.Elite && Mathf.Abs(m.level - baseMonster.level) <= 4);
                if (champion != null) spawnData = champion;
            }
            else if (roll < eliteChancePercent + championChancePercent)
            {
                MonsterData elite = monsterDatabase.monsters.Find(m => m.rank == MonsterRank.Elite && Mathf.Abs(m.level - baseMonster.level) <= 3);
                if (elite != null) spawnData = elite;
            }

            if (spawnData.prefab != null)
            {
                Vector3 pos = sp.position + Random.insideUnitSphere * 3f;
                pos.y = sp.position.y;
                GameObject go = Instantiate(spawnData.prefab, pos, Quaternion.identity);
                var monsterAI = go.GetComponent<MonsterAI>();
                if (monsterAI != null)
                {
                    monsterAI.InitializeFromData(spawnData);
                }
            }
            else
            {
                Debug.LogWarning($"SpawnData for {spawnData.monsterName} has no prefab assigned.");
            }
        }

        private void SpawnBossByName(string bossName)
        {
            MonsterData boss = monsterDatabase.monsters.Find(m => m.monsterName == bossName && (m.rank == MonsterRank.Boss || m.rank == MonsterRank.WorldBoss));
            if (boss == null)
            {
                Debug.LogWarning($"Boss {bossName} not found in MonsterDatabase.");
                return;
            }

            Transform sp = spawnPoints.Count > 0 ? spawnPoints[Random.Range(0, spawnPoints.Count)] : transform;
            Vector3 pos = sp.position;

            if (boss.prefab != null)
            {
                GameObject go = Instantiate(boss.prefab, pos, Quaternion.identity);
                var bossComp = go.GetComponent<BossMonster>();
                if (bossComp != null)
                {
                    bossComp.StartCombat();
                }
            }
            else
            {
                Debug.LogWarning($"Boss {bossName} prefab not assigned.");
            }
        }
    }
}