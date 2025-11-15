using UnityEngine;
using System.Collections.Generic;

namespace EducationalRPG.Quest
{
    [CreateAssetMenu(fileName = "QuestDatabase", menuName = "Educational RPG/Quest Database")]
    public class QuestDatabase : ScriptableObject
    {
        public List<Quest> allQuests = new List<Quest>();

        public void InitializeQuests()
        {
            // === 메인 퀘스트 ===
            var mainQuest1 = ScriptableObject.CreateInstance<Quest>();
            mainQuest1.questName = "모험의 시작";
            mainQuest1.description = "마을 촌장과 대화하여 모험을 시작하세요.";
            mainQuest1.questLevel = 1;
            mainQuest1.isMainQuest = true;
            mainQuest1.objectives = new List<QuestObjective>
            {
                new QuestObjective
                {
                    description = "촌장과 대화하기",
                    type = QuestType.Talk,
                    targetID = "촌장",
                    requiredAmount = 1
                }
            };
            mainQuest1.expReward = 100;
            mainQuest1.goldReward = 50;
            
            var mainQuest2 = ScriptableObject.CreateInstance<Quest>();
            mainQuest2.questName = "슬라임 토벌";
            mainQuest2.description = "초보자 숲에서 슬라임 10마리를 처치하세요.";
            mainQuest2.questLevel = 1;
            mainQuest2.isMainQuest = true;
            mainQuest2.objectives = new List<QuestObjective>
            {
                new QuestObjective
                {
                    description = "슬라임 처치",
                    type = QuestType.Kill,
                    targetID = "슬라임",
                    requiredAmount = 10
                }
            };
            mainQuest2.expReward = 200;
            mainQuest2.goldReward = 100;
            mainQuest2.requiredLevel = 1;
            
            var mainQuest3 = ScriptableObject.CreateInstance<Quest>();
            mainQuest3.questName = "동굴 탐험";
            mainQuest3.description = "어두운 동굴을 탐험하고 동굴 지배자를 처치하세요.";
            mainQuest3.questLevel = 8;
            mainQuest3.isMainQuest = true;
            mainQuest3.objectives = new List<QuestObjective>
            {
                new QuestObjective
                {
                    description = "동굴 입구 찾기",
                    type = QuestType.Explore,
                    targetID = "어두운 동굴",
                    requiredAmount = 1
                },
                new QuestObjective
                {
                    description = "동굴 지배자 처치",
                    type = QuestType.Kill,
                    targetID = "동굴 지배자",
                    requiredAmount = 1
                }
            };
            mainQuest3.expReward = 800;
            mainQuest3.goldReward = 500;
            mainQuest3.requiredLevel = 8;
            
            var mainQuest4 = ScriptableObject.CreateInstance<Quest>();
            mainQuest4.questName = "화염 드래곤 토벌";
            mainQuest4.description = "화염 산맥의 드래곤을 처치하고 평화를 되찾으세요.";
            mainQuest4.questLevel = 18;
            mainQuest4.isMainQuest = true;
            mainQuest4.objectives = new List<QuestObjective>
            {
                new QuestObjective
                {
                    description = "화염 드래곤 처치",
                    type = QuestType.Kill,
                    targetID = "화염 드래곤",
                    requiredAmount = 1
                }
            };
            mainQuest4.expReward = 2000;
            mainQuest4.goldReward = 2000;
            mainQuest4.requiredLevel = 18;
            
            // === 서브 퀘스트 ===
            var sideQuest1 = ScriptableObject.CreateInstance<Quest>();
            sideQuest1.questName = "토끼 사냥";
            sideQuest1.description = "숲에서 토끼 5마리를 사냥하여 가죽을 수집하세요.";
            sideQuest1.questLevel = 2;
            sideQuest1.isMainQuest = false;
            sideQuest1.objectives = new List<QuestObjective>
            {
                new QuestObjective
                {
                    description = "토끼 가죽 수집",
                    type = QuestType.Collect,
                    targetID = "토끼 가죽",
                    requiredAmount = 5
                }
            };
            sideQuest1.expReward = 150;
            sideQuest1.goldReward = 80;
            
            var sideQuest2 = ScriptableObject.CreateInstance<Quest>();
            sideQuest2.questName = "약초 채집";
            sideQuest2.description = "회복 포션 제작을 위한 약초를 채집하세요.";
            sideQuest2.questLevel = 3;
            sideQuest2.isMainQuest = false;
            sideQuest2.objectives = new List<QuestObjective>
            {
                new QuestObjective
                {
                    description = "치유 약초 채집",
                    type = QuestType.Collect,
                    targetID = "치유 약초",
                    requiredAmount = 10
                }
            };
            sideQuest2.expReward = 180;
            sideQuest2.goldReward = 100;
            
            var sideQuest3 = ScriptableObject.CreateInstance<Quest>();
            sideQuest3.questName = "고블린 퇴치";
            sideQuest3.description = "마을을 위협하는 고블린들을 처치하세요.";
            sideQuest3.questLevel = 8;
            sideQuest3.isMainQuest = false;
            sideQuest3.objectives = new List<QuestObjective>
            {
                new QuestObjective
                {
                    description = "고블린 처치",
                    type = QuestType.Kill,
                    targetID = "고블린",
                    requiredAmount = 20
                }
            };
            sideQuest3.expReward = 600;
            sideQuest3.goldReward = 400;
            
            var sideQuest4 = ScriptableObject.CreateInstance<Quest>();
            sideQuest4.questName = "잃어버린 유물";
            sideQuest4.description = "고대 유적에서 잃어버린 유물을 찾아오세요.";
            sideQuest4.questLevel = 20;
            sideQuest4.isMainQuest = false;
            sideQuest4.objectives = new List<QuestObjective>
            {
                new QuestObjective
                {
                    description = "고대 유적 탐험",
                    type = QuestType.Explore,
                    targetID = "고대 유적",
                    requiredAmount = 1
                },
                new QuestObjective
                {
                    description = "고대 유물 획득",
                    type = QuestType.Collect,
                    targetID = "고대 유물",
                    requiredAmount = 1
                }
            };
            sideQuest4.expReward = 1500;
            sideQuest4.goldReward = 1200;

            allQuests = new List<Quest>
            {
                mainQuest1, mainQuest2, mainQuest3, mainQuest4,
                sideQuest1, sideQuest2, sideQuest3, sideQuest4
            };
        }
    }
}