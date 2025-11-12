// MonsterSpawner.cs
using UnityEngine;

public class MonsterSpawner : MonoBehaviour {
    public GameObject monsterPrefab;
    public float spawnInterval = 5f;
    private float timeSinceLastSpawn;

    void Update() {
        timeSinceLastSpawn += Time.deltaTime;
        if (timeSinceLastSpawn >= spawnInterval) {
            SpawnMonster();
            timeSinceLastSpawn = 0f;
        }
    }

    void SpawnMonster() {
        Instantiate(monsterPrefab, transform.position, Quaternion.identity);
    }
}