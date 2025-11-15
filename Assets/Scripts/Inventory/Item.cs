using UnityEngine;

namespace EducationalRPG.Inventory
{
    public enum EquipmentSlot
    {
        None,
        Weapon,
        Armor,
        Helmet,
        Boots,
        Accessory
    }

    [CreateAssetMenu(fileName = "New Item", menuName = "Educational RPG/Item")]
    public class Item : ScriptableObject
    {
        [Header("Basic Info")]
        public string itemName;
        [TextArea(2,4)] public string description;
        public Sprite icon;
        public ItemRarity rarity = ItemRarity.Common;

        [Header("Shop")]
        public int buyPrice;
        public int sellPrice;

        [Header("Usage")]
        public bool isConsumable = false;
        public bool isEquipment = false;
        public EquipmentSlot equipmentSlot = EquipmentSlot.None;

        [Header("Consumable Effects")]
        public int healthRestore = 0;
        public int manaRestore = 0;
        public float buffDuration = 0f;
        public float attackBuffMultiplier = 1f;
        public float defenseBuffMultiplier = 1f;

        [Header("Equipment Bonuses")]
        public int attackBonus = 0;
        public int defenseBonus = 0;
        public int healthBonus = 0;
    }
}