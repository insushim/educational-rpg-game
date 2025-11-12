using UnityEngine;

namespace EducationalRPG.Inventory
{
    public enum ItemType
    {
        Consumable,
        Equipment,
        QuestItem,
        Material
    }

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
        [TextArea(3, 5)]
        public string description;
        public Sprite icon;
        public ItemType itemType;
        public int maxStack = 99;
        
        [Header("Value")]
        public int buyPrice = 100;
        public int sellPrice = 50;
        
        [Header("Equipment")]
        public EquipmentSlot equipmentSlot = EquipmentSlot.None;
        public int attackBonus = 0;
        public int defenseBonus = 0;
        public int healthBonus = 0;
        
        [Header("Consumable")]
        public int healAmount = 0;
        public int manaAmount = 0;
        public float buffDuration = 0f;
        public float attackBuffMultiplier = 1f;
        public float defenseBuffMultiplier = 1f;

        public virtual void Use(GameObject target)
        {
            switch (itemType)
            {
                case ItemType.Consumable:
                    UseConsumable(target);
                    break;
                case ItemType.Equipment:
                    EquipItem(target);
                    break;
            }
        }

        protected virtual void UseConsumable(GameObject target)
        {
            var combatStats = target.GetComponent<Combat.CombatStats>();
            if (combatStats != null && healAmount > 0)
            {
                combatStats.Heal(healAmount);
                Debug.Log($"Used {itemName}: Healed {healAmount} HP");
            }

            if (buffDuration > 0f)
            {
                var buffSystem = target.GetComponent<Combat.BuffSystem>();
                if (buffSystem != null)
                {
                    buffSystem.ApplyBuff(this);
                }
            }
        }

        protected virtual void EquipItem(GameObject target)
        {
            var equipmentManager = target.GetComponent<EquipmentManager>();
            if (equipmentManager != null)
            {
                equipmentManager.EquipItem(this);
            }
        }
    }
}