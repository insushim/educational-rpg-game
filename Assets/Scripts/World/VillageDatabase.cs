using UnityEngine;
using System.Collections.Generic;

namespace EducationalRPG.World
{
    [System.Serializable]
    public class VillageData
    {
        public string villageName;
        public int requiredLevel;
        public Vector3 position;
        public float safeZoneRadius;
        public float healRate;
        public List<string> availableShops;
        public string description;
    }

    [CreateAssetMenu(fileName = "VillageDatabase", menuName = "Educational RPG/World/Village Database")]
    public class VillageDatabase : ScriptableObject
    {
        public List<VillageData> villages = new List<VillageData>()
        {
            new VillageData
            {
                villageName = "시작 마을",
                requiredLevel = 1,
                safeZoneRadius = 25f,
                healRate = 5f,
                availableShops = new List<string> { "일반 상점", "대장간" },
                description = "모험가들이 처음 발을 딛는 평화로운 마을"
            },
            new VillageData
            {
                villageName = "숲의 마을",
                requiredLevel = 5,
                safeZoneRadius = 30f,
                healRate = 7f,
                availableShops = new List<string> { "약초 상점", "마법 상점", "무기 상점" },
                description = "거대한 숲 속에 자리잡은 엘프들의 마을"
            },
            new VillageData
            {
                villageName = "산악 요새",
                requiredLevel = 10,
                safeZoneRadius = 35f,
                healRate = 10f,
                availableShops = new List<string> { "고급 장비 상점", "스킬 마스터", "연금술사" },
                description = "높은 산 위에 세워진 견고한 요새 마을"
            },
            new VillageData
            {
                villageName = "항구 도시",
                requiredLevel = 15,
                safeZoneRadius = 40f,
                healRate = 12f,
                availableShops = new List<string> { "무역 상점", "보석 상점", "선박 여관" },
                description = "바다와 맞닿은 번화한 무역 도시"
            },
            new VillageData
            {
                villageName = "마법사의 탑",
                requiredLevel = 20,
                safeZoneRadius = 50f,
                healRate = 15f,
                availableShops = new List<string> { "고급 마법 상점", "마법 도서관", "마법 연구소" },
                description = "고대 마법사들이 거주하는 신비로운 탑"
            }
        };
    }
}