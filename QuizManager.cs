using System.Collections.Generic;
using UnityEngine;

namespace EducationalRPG.Quiz
{
    public class QuizManager : MonoBehaviour
    {
        [Header("Data")]
        public TextAsset vocabularyJson; // assign JSON file in inspector

        private List<VocabularyData> vocabList = new List<VocabularyData>();

        [Header("References")]
        public RewardManager rewardManager;

        private void Awake()
        {
            if (vocabularyJson != null)
            {
                vocabList = JsonHelper.FromJson<VocabularyData>(vocabularyJson.text);
            }
        }

        public VocabularyData GetRandomQuestion(string difficulty = "easy")
        {
            var filtered = vocabList.FindAll(v => v.difficulty == difficulty);
            if (filtered.Count == 0) filtered = vocabList;
            if (filtered.Count == 0) return null;
            return filtered[Random.Range(0, filtered.Count)];
        }

        public QuizResult CheckAnswer(VocabularyData question, string answer, QuizType type)
        {
            bool correct = false;
            if (type == QuizType.EnglishToKorean)
            {
                correct = string.Equals(answer.Trim(), question.korean.Trim(), System.StringComparison.OrdinalIgnoreCase);
            }
            else
            {
                correct = string.Equals(answer.Trim(), question.english.Trim(), System.StringComparison.OrdinalIgnoreCase);
            }

            // simple reward logic
            int gold = correct ? 10 : 2;
            bool dropItem = correct && Random.value < 0.2f;
            string itemName = dropItem ? "Small Potion" : "";

            var result = new QuizResult(correct, gold, dropItem, itemName);

            // give reward through manager if available
            if (rewardManager != null)
            {
                rewardManager.GiveQuizReward(result);
            }

            return result;
        }
    }

    // Helper for JsonUtility to parse arrays
    public static class JsonHelper
    {
        private class Wrapper<T>
        {
            public T[] Items;
        }

        public static List<T> FromJson<T>(string json)
        {
            // wrap array into object
            if (json.TrimStart().StartsWith("["))
            {
                json = "{"Items":