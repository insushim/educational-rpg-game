using UnityEngine;
using UnityEngine.SceneManagement;

namespace EducationalRPG.Managers
{
    /// <summary>
    /// 게임 전체 관리자 (싱글톤)
    /// </summary>
    public class GameManager : MonoBehaviour
    {
        private static GameManager instance;
        public static GameManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = FindObjectOfType<GameManager>();
                    if (instance == null)
                    {
                        GameObject go = new GameObject("GameManager");
                        instance = go.AddComponent<GameManager>();
                    }
                }
                return instance;
            }
        }

        [Header("References")]
        public Player.PlayerStats playerStats;
        public InventoryManager inventoryManager;
        public RewardManager rewardManager;
        public Quiz.QuizManager quizManager;
        public DataManager dataManager;

        [Header("Game State")]
        public bool isPaused = false;
        public bool isInCombat = false;

        private void Awake()
        {
            if (instance != null && instance != this)
            {
                Destroy(gameObject);
                return;
            }

            instance = this;
            DontDestroyOnLoad(gameObject);

            InitializeManagers();
        }

        private void InitializeManagers()
        {
            if (playerStats == null)
            {
                var player = GameObject.FindGameObjectWithTag("Player");
                if (player != null)
                {
                    playerStats = player.GetComponent<Player.PlayerStats>();
                }
            }

            if (inventoryManager == null)
                inventoryManager = FindObjectOfType<InventoryManager>();

            if (rewardManager == null)
                rewardManager = FindObjectOfType<RewardManager>();

            if (quizManager == null)
                quizManager = FindObjectOfType<Quiz.QuizManager>();

            if (dataManager == null)
                dataManager = FindObjectOfType<DataManager>();
        }

        public void PauseGame()
        {
            isPaused = true;
            Time.timeScale = 0f;
        }

        public void ResumeGame()
        {
            isPaused = false;
            Time.timeScale = 1f;
        }

        public void LoadScene(string sceneName)
        {
            SceneManager.LoadScene(sceneName);
        }

        public void QuitGame()
        {
            Application.Quit();
        }
    }
}