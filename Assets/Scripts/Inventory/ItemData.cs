using UnityEngine;

namespace EducationalRPG.Inventory
{
    /// <summary>
    /// 아이템 타입
    /// </summary>
    public enum ItemType
    {
        Consumable,  // 소모품 (포션 등)
        Equipment,   // 장비 (무기, 방어구)
        QuestItem,   // 퀘스트 아이템
        Misc         // 기타
    }

    /// <summary>
    /// 아이템 데이터 (ScriptableObject)
    /// </summary>
    [CreateAssetMenu(fileName = "NewItem", menuName = "Educational RPG/Item Data")]
    public class ItemData : ScriptableObject
    {
        [Header("Basic Info")]
        public int itemId;
        public string itemName;
        [TextArea(3, 5)]
        public string description;
        public Sprite icon;
        public ItemType itemType;

        [Header("Stack")]
        public bool isStackable = true;
        public int maxStackSize = 99;

        [Header("Value")]
        public int buyPrice = 0;
        public int sellPrice = 0;

        [Header("Effects (for Consumables)")]
        public int healthRestore = 0;
        public int manaRestore = 0;

        [Header("Stats (for Equipment)")]
        public int attackBonus = 0;
        public int defenseBonus = 0;
    }
}