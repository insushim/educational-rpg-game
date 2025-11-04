using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace EducationalRPG.UI
{
    /// <summary>
    /// 퀴즈 UI 관리
    /// </summary>
    public class QuizUI : MonoBehaviour
    {
        [Header("UI References")]  
        [SerializeField] private GameObject quizPanel;
        [SerializeField] private TextMeshProUGUI questionText;
        [SerializeField] private TMP_InputField answerInput;
        [SerializeField] private Button submitButton;
        [SerializeField] private TextMeshProUGUI resultText;
        [SerializeField] private TextMeshProUGUI rewardText;

        [Header("Managers")]  
        [SerializeField] private Quiz.QuizManager quizManager;

        private Quiz.VocabularyData currentQuestion;
        private Quiz.QuizType currentQuizType;

        private void Start()
        {
            if (quizPanel != null)
                quizPanel.SetActive(false);

            if (submitButton != null)
                submitButton.onClick.AddListener(OnSubmitAnswer);

            if (quizManager == null)
                quizManager = FindObjectOfType<Quiz.QuizManager>();
        }

        /// <summary>
        /// 퀴즈 시작
        /// </summary>
        public void StartQuiz(string difficulty = "easy")
        {
            if (quizManager == null) return;

            currentQuestion = quizManager.GetRandomQuestion(difficulty);
            if (currentQuestion == null)
            {
                Debug.LogError("No quiz question available!");
                return;
            }

            currentQuizType = Random.value > 0.5f ? Quiz.QuizType.EnglishToKorean : Quiz.QuizType.KoreanToEnglish;

            if (quizPanel != null)
                quizPanel.SetActive(true);

            if (questionText != null)
            {
                if (currentQuizType == Quiz.QuizType.EnglishToKorean)
                {
                    questionText.text = $"영어를 한글로: {currentQuestion.english}";
                }
                else
                {
                    questionText.text = $"한글을 영어로: {currentQuestion.korean}";
                }
            }

            if (answerInput != null)
            {
                answerInput.text = "";
                answerInput.ActivateInputField();
            }

            if (resultText != null)
                resultText.text = "";

            if (rewardText != null)
                rewardText.text = "";
        }

        /// <summary>
        /// 정답 제출
        /// </summary>
        private void OnSubmitAnswer()
        {
            if (currentQuestion == null || answerInput == null) return;

            string answer = answerInput.text.Trim();
            if (string.IsNullOrEmpty(answer)) return;

            Quiz.QuizResult result = quizManager.CheckAnswer(currentQuestion, answer, currentQuizType);

            if (resultText != null)
            {
                resultText.text = result.isCorrect ? "정답입니다! ✓" : "오답입니다. ✗";
                resultText.color = result.isCorrect ? Color.green : Color.red;
            }

            if (rewardText != null)
            {
                string rewardMsg = $"골드 +{result.goldReward}";
                if (result.hasItemReward)
                {
                    rewardMsg += $"\n아이템 획득: {result.itemName}";
                }
                rewardText.text = rewardMsg;
            }

            Invoke("CloseQuiz", 2f);
        }

        /// <summary>
        /// 퀴즈 닫기
        /// </summary>
        public void CloseQuiz()
        {
            if (quizPanel != null)
                quizPanel.SetActive(false);

            currentQuestion = null;
        }

        private void OnDestroy()
        {
            if (submitButton != null)
                submitButton.onClick.RemoveListener(OnSubmitAnswer);
        }
    }
}