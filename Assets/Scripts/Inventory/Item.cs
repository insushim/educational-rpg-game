using UnityEngine;

namespace EducationalRPG.Inventory
{
    /// <summary>
    /// 아이템 인스턴스 (실제 인벤토리에 들어가는 아이템)
    /// </summary>
    [System.Serializable]
    public class Item
    {
        public ItemData itemData;
        public int quantity;

        public Item(ItemData data, int qty = 1)
        {
            itemData = data;
            quantity = qty;
        }

        /// <summary>
        /// 아이템 사용 (소모품)
        /// </summary>
        public bool Use(Player.PlayerStats playerStats)
        {
            if (itemData == null || playerStats == null) return false;

            switch (itemData.itemType)
            {
                case ItemType.Consumable:
                    // 체력 회복
                    if (itemData.healthRestore > 0)
                    {
                        playerStats.Heal(itemData.healthRestore);
                        Debug.Log($"Used {itemData.itemName}: +{itemData.healthRestore} HP");
                    }

                    // 마나 회복
                    if (itemData.manaRestore > 0)
                    {
                        playerStats.RestoreMana(itemData.manaRestore);
                        Debug.Log($"Used {itemData.itemName}: +{itemData.manaRestore} MP");
                    }

                    quantity--;
                    return true;

                case ItemType.Equipment:
                    Debug.Log("Equipment needs to be equipped, not used directly");
                    return false;

                default:
                    Debug.Log($"{itemData.itemName} cannot be used");
                    return false;
            }
        }

        /// <summary>
        /// 수량 추가
        /// </summary>
        public void AddQuantity(int amount)
        {
            quantity += amount;
            if (itemData != null && quantity > itemData.maxStackSize)
            {
                quantity = itemData.maxStackSize;
            }
        }
    }
}