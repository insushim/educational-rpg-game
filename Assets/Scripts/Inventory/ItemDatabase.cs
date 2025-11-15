using UnityEngine;
using System.Collections.Generic;

namespace EducationalRPG.Inventory
{
    public enum ItemRarity
    {
        Common,      // 일반 (회색)
        Uncommon,    // 고급 (녹색)
        Rare,        // 희귀 (파란색)
        Epic,        // 영웅 (보라색)
        Legendary,   // 전설 (주황색)
        Mythic       // 신화 (빨간색)
    }

    [CreateAssetMenu(fileName = "ItemDatabase", menuName = "Educational RPG/Item Database")]
    public class ItemDatabase : ScriptableObject
    {
        public List<Item> allItems = new List<Item>();

        public void InitializeItems()
        {
            allItems = new List<Item>()
            {
                // === 소비 아이템 ===
                CreateConsumableItem("작은 회복 포션", "HP를 50 회복합니다", 20, 10, 50, 0),
                CreateConsumableItem("중간 회복 포션", "HP를 150 회복합니다", 80, 40, 150, 0),
                CreateConsumableItem("큰 회복 포션", "HP를 300 회복합니다", 200, 100, 300, 0),
                CreateConsumableItem("최상급 회복 포션", "HP를 500 회복합니다", 500, 250, 500, 0),
                
                CreateConsumableItem("작은 마나 포션", "MP를 30 회복합니다", 25, 12, 0, 30),
                CreateConsumableItem("중간 마나 포션", "MP를 100 회복합니다", 100, 50, 0, 100),
                CreateConsumableItem("큰 마나 포션", "MP를 200 회복합니다", 250, 125, 0, 200),
                
                // 버프 포션
                CreateBuffPotion("힘의 물약", "10분간 공격력 50% 증가", 150, 75, 600f, 1.5f, 1f),
                CreateBuffPotion("수호의 물약", "10분간 방어력 50% 증가", 150, 75, 600f, 1f, 1.5f),
                CreateBuffPotion("전투의 영약", "15분간 공격력과 방어력 30% 증가", 300, 150, 900f, 1.3f, 1.3f),
                CreateBuffPotion("신속의 물약", "5분간 이동속도 100% 증가", 100, 50, 300f, 1f, 1f),
                
                // === 무기 (Weapon) ===
                CreateWeapon("낡은 검", "초보자용 기본 검", ItemRarity.Common, 50, 25, 5, 0, 0)