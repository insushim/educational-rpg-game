using UnityEngine;
using System.Collections.Generic;

namespace EducationalRPG.World
{
    public enum GroundType
    {
        Forest,
        Cave,
        Mountain,
        Desert,
        Dungeon,
        Ruins
    }

    [System.Serializable]
    public class HuntingGroundData
    {
        public string groundName;
        public GroundType groundType;
        public int minLevel;
        public int maxLevel;
        public float expMultiplier;
        public Color areaColor;
        public List<string> monsterTypes;
        public bool hasBoss;
        public string bossName;
        public string description;
    }

    [CreateAssetMenu(fileName = "HuntingGroundDatabase", menuName = "Educational RPG/World/Hunting Ground Database")]
    public class HuntingGroundDatabase : ScriptableObject
    {
        public List<HuntingGroundData> huntingGrounds = new List<HuntingGroundData>()
        {
            new HuntingGroundData
            {
                groundName = "초보자 숲",
                groundType = GroundType.Forest,
                minLevel = 1,
                maxLevel = 5,
                expMultiplier = 1.0f,
                areaColor = new Color(0.5f, 1f, 0.5f),
                monsterTypes = new List<string> { "슬라임", "토끼", "늑대" },
                hasBoss = false,
                description = "처음 모험을 시작하기에 적합한 평화로운 숲"
            },
            new HuntingGroundData
            {
                groundName = "어두운 동굴",
                groundType = GroundType.Cave,
                minLevel = 5,
                maxLevel = 10,
                expMultiplier = 1.2f,
                areaColor = new Color(0.3f, 0.3f, 0.3f),
                monsterTypes = new List<string> { "박쥐", "거미", "고블린" },
                hasBoss = true,
                bossName = "동굴 지배자",
                description = "어둠 속에 위험한 몬스터들이 숨어있는 동굴"
            },
            new HuntingGroundData
            {
                groundName = "독버섯 숲",
                groundType = GroundType.Forest,
                minLevel = 8,
                maxLevel = 13,
                expMultiplier = 1.3f,
                areaColor = new Color(0.8f, 0.3f, 0.8f),
                monsterTypes = new List<string> { "독버섯", "맹독 슬라임", "식인 나무" },
                hasBoss = true,
                bossName = "버섯 왕",
                description = "독성 포자가 가득한 위험한 숲"
            },
            new HuntingGroundData
            {
                groundName = "화염 산맥",
                groundType = GroundType.Mountain,
                minLevel = 12,
                maxLevel = 18,
                expMultiplier = 1.5f,
                areaColor = new Color(1f, 0.3f, 0f),
                monsterTypes = new List<string> { "파이어 엘리멘탈", "용암 골렘", "불도마뱀" },
                hasBoss = true,
                bossName = "화염 드래곤",
                description = "끓어오르는 용암과 불꽃이 가득한 산맥"
            },
            new HuntingGroundData
            {
                groundName = "얼어붙은 황무지",
                groundType = GroundType.Desert,
                minLevel = 15,
                maxLevel = 20,
                expMultiplier = 1.6f,
                areaColor = new Color(0.7f, 0.9f, 1f),
                monsterTypes = new List<string> { "아이스 골렘", "눈보라 정령", "서리 거인" },
                hasBoss = true,
                bossName = "얼음 여왕",
                description = "영원한 겨울이 지배하는 얼어붙은 땅"
            },
            new HuntingGroundData
            {
                groundName = "고대 유적",
                groundType = GroundType.Ruins,
                minLevel = 18,
                maxLevel = 25,
                expMultiplier = 2.0f,
                areaColor = new Color(0.8f, 0.7f, 0.3f),
                monsterTypes = new List<string> { "언데드 전사", "고대 가디언", "저주받은 마법사" },
                hasBoss = true,
                bossName = "리치 왕",
                description = "고대 문명의 잔재가 남아있는 신비로운 유적"
            },
            new HuntingGroundData
            {
                groundName = "심연의 던전",
                groundType = GroundType.Dungeon,
                minLevel = 20,
                maxLevel = 30,
                expMultiplier = 2.5f,
                areaColor = new Color(0.2f, 0f, 0.4f),
                monsterTypes = new List<string> { "악마", "그림자 암살자", "드래곤" },
                hasBoss = true,
                bossName = "마왕",
                description = "세상의 끝에 존재하는 절대 악의 던전"
            }
        };
    }
}