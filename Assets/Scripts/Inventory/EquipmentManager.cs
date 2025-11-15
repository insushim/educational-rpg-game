using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;

namespace EducationalRPG.Inventory
{
    public class EquipmentManager : MonoBehaviour
    {
        [Header("Equipment Slots")]
        private Item weapon;
        private Item armor;
        private Item helmet;
        private Item boots;
        private Item accessory;
        
        [Header("Stats")]
        [SerializeField] private int totalAttackBonus = 0;
        [SerializeField] private int totalDefenseBonus = 0;
        [SerializeField] private int totalHealthBonus = 0;
        
        [Header("Events")]
        public UnityEvent<Item> OnItemEquipped;
        public UnityEvent<Item> OnItemUnequipped;
        public UnityEvent OnStatsChanged;

        public static EquipmentManager Instance { get; private set; }

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        public void EquipItem(string itemType, Item item) {
            switch(itemType) {
                case "weapon":
                    if (weapon != null) UnequipItem("weapon");
                    weapon = item;
                    ApplyItemStats(item, true);
                    OnItemEquipped?.Invoke(item);
                    break;
                case "armor":
                    if (armor != null) UnequipItem("armor");
                    armor = item;
                    ApplyItemStats(item, true);
                    OnItemEquipped?.Invoke(item);
                    break;
                case "helmet":
                    if (helmet != null) UnequipItem("helmet");
                    helmet = item;
                    ApplyItemStats(item, true);
                    OnItemEquipped?.Invoke(item);
                    break;
                case "boots":
                    if (boots != null) UnequipItem("boots");
                    boots = item;
                    ApplyItemStats(item, true);
                    OnItemEquipped?.Invoke(item);
                    break;
                case "accessory":
                    if (accessory != null) UnequipItem("accessory");
                    accessory = item;
                    ApplyItemStats(item, true);
                    OnItemEquipped?.Invoke(item);
                    break;
                default:
                    Debug.LogWarning("Invalid item type: " + itemType);
                    break;
            }
        }

        public void UnequipItem(string itemType) {
            switch(itemType) {
                case "weapon":
                    if (weapon != null) { ApplyItemStats(weapon, false); InventoryManager.Instance?.AddItem(weapon); OnItemUnequipped?.Invoke(weapon); weapon = null; }
                    break;
                case "armor":
                    if (armor != null) { ApplyItemStats(armor, false); InventoryManager.Instance?.AddItem(armor); OnItemUnequipped?.Invoke(armor); armor = null; }
                    break;
                case "helmet":
                    if (helmet != null) { ApplyItemStats(helmet, false); InventoryManager.Instance?.AddItem(helmet); OnItemUnequipped?.Invoke(helmet); helmet = null; }
                    break;
                case "boots":
                    if (boots != null) { ApplyItemStats(boots, false); InventoryManager.Instance?.AddItem(boots); OnItemUnequipped?.Invoke(boots); boots = null; }
                    break;
                case "accessory":
                    if (accessory != null) { ApplyItemStats(accessory, false); InventoryManager.Instance?.AddItem(accessory); OnItemUnequipped?.Invoke(accessory); accessory = null; }
                    break;
                default:
                    Debug.LogWarning("Invalid item type: " + itemType);
                    break;
            }
        }

        public Item GetEquippedItem(string itemType) {
            switch(itemType) {
                case "weapon":
                    return weapon;
                case "armor":
                    return armor;
                case "helmet":
                    return helmet;
                case "boots":
                    return boots;
                case "accessory":
                    return accessory;
                default:
                    Debug.LogWarning("Invalid item type: " + itemType);
                    return null;
            }
        }

        private void ApplyItemStats(Item item, bool isEquipping)
        {
            if (item == null) return;
            int multiplier = isEquipping ? 1 : -1;

            totalAttackBonus += item.attackBonus * multiplier;
            totalDefenseBonus += item.defenseBonus * multiplier;
            totalHealthBonus += item.healthBonus * multiplier;

            var combatStats = GetComponent<Combat.CombatStats>();
            if (combatStats != null && item.healthBonus != 0)
            {
                if (isEquipping)
                {
                    combatStats.IncreaseMaxHealth(item.healthBonus);
                }
                else
                {
                    combatStats.DecreaseMaxHealth(item.healthBonus);
                }
            }

            OnStatsChanged?.Invoke();
        }

        public int GetTotalAttackBonus() => totalAttackBonus;
        public int GetTotalDefenseBonus() => totalDefenseBonus;
        public int GetTotalHealthBonus() => totalHealthBonus;
    }
}