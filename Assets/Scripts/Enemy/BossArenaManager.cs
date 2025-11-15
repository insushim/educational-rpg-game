using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace EducationalRPG.Enemy
{
    // Manages a boss arena: lock doors, start cinematic, spawn adds, handle victory/defeat
    public class BossArenaManager : MonoBehaviour
    {
        [Header("Arena Settings")]
        public Transform arenaCenter;
        public float arenaRadius = 25f;
        public Door[] doorsToLock; // assign door objects that implement open/close
        public AudioClip arenaMusic;
        public AudioClip bossMusic;
        public float cinematicDuration = 3f;
        public Transform[] addSpawnPoints;
        public GameObject addPrefab;
        public int addsPerWave = 3;
        public float addSpawnInterval = 15f;

        [Header("References")]
        public BossMonster boss;
        public UnityEvent OnArenaStart;
        public UnityEvent OnArenaEnd;

        private bool arenaActive = false;
        private AudioSource audioSource;

        private void Awake()
        {
            audioSource = GetComponent<AudioSource>();
            if (audioSource == null) audioSource = gameObject.AddComponent<AudioSource>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (arenaActive) return;
            if (!other.CompareTag("Player")) return;

            // Player entered arena trigger -> start sequence
            StartCoroutine(StartArenaSequence());
        }

        private IEnumerator StartArenaSequence()
        {
            arenaActive = true;
            LockDoors();
            UI.GameUIManager.Instance?.ShowNotification("경고: 보스 전투 개시!");
            OnArenaStart?.Invoke();

            // Play arena music briefly then boss theme
            if (arenaMusic != null)
            {
                audioSource.clip = arenaMusic;
                audioSource.loop = true;
                audioSource.Play();
            }

            // cinematic pause / camera work placeholder
            yield return new WaitForSeconds(cinematicDuration);

            if (bossMusic != null)
            {
                audioSource.clip = bossMusic;
                audioSource.loop = true;
                audioSource.Play();
            }

            // Start boss combat
            if (boss != null)
            {
                boss.StartCombat();
                boss.OnBossDeath.AddListener(OnBossDefeated); // subscribe
            }

            // start spawning adds if configured
            if (addPrefab != null && addSpawnPoints != null && addSpawnPoints.Length > 0)
            {
                StartCoroutine(SpawnAddsLoop());
            }
        }

        private IEnumerator SpawnAddsLoop()
        {
            while (arenaActive && boss != null)
            {
                for (int i = 0; i < addsPerWave; i++)
                {
                    Transform sp = addSpawnPoints[Random.Range(0, addSpawnPoints.Length)];
                    Vector3 pos = sp.position + Random.insideUnitSphere * 2f;
                    pos.y = sp.position.y;
                    Instantiate(addPrefab, pos, Quaternion.identity);
                }
                yield return new WaitForSeconds(addSpawnInterval);
            }
        }

        private void OnBossDefeated()
        {
            // Stop add spawning
            arenaActive = false;
            UnlockDoors();
            UI.GameUIManager.Instance?.ShowNotification("보스 처치! 보상을 획득하세요.");
            OnArenaEnd?.Invoke();

            // Fade out music
            StartCoroutine(FadeOutAudio(2f));
        }

        private void LockDoors()
        {
            if (doorsToLock == null) return;
            foreach (var d in doorsToLock)
            {
                if (d != null) d.CloseDoor();
            }
        }

        private void UnlockDoors()
        {
            if (doorsToLock == null) return;
            foreach (var d in doorsToLock)
            {
                if (d != null) d.OpenDoor();
            }
        }

        private IEnumerator FadeOutAudio(float duration)
        {
            if (audioSource == null || !audioSource.isPlaying) yield break;
            float startVol = audioSource.volume;
            float t = 0f;
            while (t < duration)
            {
                t += Time.deltaTime;
                audioSource.volume = Mathf.Lerp(startVol, 0f, t / duration);
                yield return null;
            }
            audioSource.Stop();
            audioSource.volume = startVol;
        }

        // Optional helper: check if player inside arena bounds
        public bool IsInsideArena(Vector3 position)
        {
            if (arenaCenter == null) return false;
            return Vector3.Distance(arenaCenter.position, position) <= arenaRadius;
        }
    }

    // Simple door interface for arena locking
    public abstract class Door : MonoBehaviour
    {
        public abstract void OpenDoor();
        public abstract void CloseDoor();
    }
}