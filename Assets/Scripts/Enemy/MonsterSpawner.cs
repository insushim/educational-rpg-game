using UnityEngine;
using System.Collections.Generic;

namespace EducationalRPG.Enemy
{
    public class MonsterSpawner : MonoBehaviour
    {
        [Header("Spawn Settings")]
        [SerializeField] private GameObject monsterPrefab;
        [SerializeField] private int maxMonsters = 5;
        [SerializeField] private float spawnRadius = 10f;
        [SerializeField] private float respawnTime = 30f;
        
        [Header("Monster Settings")]
        [SerializeField] private int monsterLevel = 1;
        [SerializeField] private int expReward = 50;
        
        [Header("Spawn Area")]
        [SerializeField] private Vector3 spawnCenter;
        [SerializeField] private bool useTransformAsCenter = true;
        
        private List<GameObject> activeMonsters = new List<GameObject>();
        private List<float> respawnTimers = new List<float>();

        private void Start()
        {
            if (useTransformAsCenter)
            {
                spawnCenter = transform.position;
            }
            
            for (int i = 0; i < maxMonsters; i++)
            {
                SpawnMonster();
            }
        }

        private void Update()
        {
            for (int i = respawnTimers.Count - 1; i >= 0; i--)
            {
                respawnTimers[i] -= Time.deltaTime;
                
                if (respawnTimers[i] <= 0f)
                {
                    SpawnMonster();
                    respawnTimers.RemoveAt(i);
                }
            }
        }

        private void SpawnMonster()
        {
            if (activeMonsters.Count >= maxMonsters) return;
            
            Vector3 spawnPosition = GetRandomSpawnPosition();
            GameObject monster = Instantiate(monsterPrefab, spawnPosition, Quaternion.identity);
            
            var combatStats = monster.GetComponent<Combat.CombatStats>();
            if (combatStats != null)
            {
                combatStats.OnDeath.AddListener(() => OnMonsterDeath(monster));
            }
            
            var monsterData = monster.GetComponent<MonsterData>();
            if (monsterData != null)
            {
                monsterData.SetLevel(monsterLevel);
                monsterData.SetExpReward(expReward);
            }
            
            activeMonsters.Add(monster);
            Debug.Log($"Monster spawned at {spawnPosition}. Active: {activeMonsters.Count}/{maxMonsters}");
        }

        private void OnMonsterDeath(GameObject monster)
        {
            if (activeMonsters.Contains(monster))
            {
                activeMonsters.Remove(monster);
                respawnTimers.Add(respawnTime);
                Debug.Log($"Monster died. Respawn in {respawnTime}s. Active: {activeMonsters.Count}/{maxMonsters}");
            }
        }

        private Vector3 GetRandomSpawnPosition()
        {
            Vector2 randomCircle = Random.insideUnitCircle * spawnRadius;
            Vector3 randomPosition = spawnCenter + new Vector3(randomCircle.x, 0f, randomCircle.y);
            
            UnityEngine.AI.NavMeshHit hit;
            if (UnityEngine.AI.NavMesh.SamplePosition(randomPosition, out hit, spawnRadius, UnityEngine.AI.NavMesh.AllAreas))
            {
                return hit.position;
            }
            
            return randomPosition;
        }

        private void OnDrawGizmosSelected()
        {
            Vector3 center = useTransformAsCenter ? transform.position : spawnCenter;
            
            Gizmos.color = Color.cyan;
            Gizmos.DrawWireSphere(center, spawnRadius);
            
            Gizmos.color = Color.green;
            for (int i = 0; i < maxMonsters; i++)
            {
                float angle = (360f / maxMonsters) * i;
                Vector3 pos = center + Quaternion.Euler(0f, angle, 0f) * Vector3.forward * (spawnRadius * 0.5f);
                Gizmos.DrawWireSphere(pos, 0.5f);
            }
        }
    }
}