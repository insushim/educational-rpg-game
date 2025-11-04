using System.Collections.Generic;
using UnityEngine;

namespace EducationalRPG.Inventory
{
    /// <summary>
    /// 인벤토리 관리 (20칸 고정 슬롯)
    /// </summary>
    public class InventoryManager : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] private int maxSlots = 20;

        private List<Item> items = new List<Item>();

        // Events
        public System.Action OnInventoryChanged;

        // Properties
        public List<Item> Items => items;
        public int MaxSlots => maxSlots;

        private void Awake()
        {
            // 빈 슬롯으로 초기화
            items = new List<Item>(maxSlots);
        }

        /// <summary>
        /// 아이템 추가
        /// </summary>
        public bool AddItem(ItemData itemData, int quantity = 1)
        {
            if (itemData == null) return false;

            // 스택 가능한 아이템인 경우 기존 아이템에 추가
            if (itemData.isStackable)
            {
                Item existingItem = items.Find(i => i != null && i.itemData == itemData);
                if (existingItem != null)
                {
                    existingItem.AddQuantity(quantity);
                    OnInventoryChanged?.Invoke();
                    Debug.Log($"Added {quantity}x {itemData.itemName} to existing stack");
                    return true;
                }
            }

            // 새 슬롯에 추가
            if (items.Count < maxSlots)
            {
                items.Add(new Item(itemData, quantity));
                OnInventoryChanged?.Invoke();
                Debug.Log($"Added {quantity}x {itemData.itemName} to inventory");
                return true;
            }

            Debug.Log("Inventory is full!");
            return false;
        }

        /// <summary>
        /// 아이템 제거
        /// </summary>
        public bool RemoveItem(ItemData itemData, int quantity = 1)
        {
            if (itemData == null) return false;

            Item item = items.Find(i => i != null && i.itemData == itemData);
            if (item != null)
            {
                item.quantity -= quantity;
                if (item.quantity <= 0)
                {
                    items.Remove(item);
                }
                OnInventoryChanged?.Invoke();
                Debug.Log($"Removed {quantity}x {itemData.itemName}");
                return true;
            }

            return false;
        }

        /// <summary>
        /// 아이템 사용
        /// </summary>
        public bool UseItem(int slotIndex)
        {
            if (slotIndex < 0 || slotIndex >= items.Count) return false;

            Item item = items[slotIndex];
            if (item == null) return false;

            var player = GameObject.FindGameObjectWithTag("Player");
            if (player == null) return false;

            var playerStats = player.GetComponent<Player.PlayerStats>();
            if (playerStats == null) return false;

            bool used = item.Use(playerStats);
            if (used && item.quantity <= 0)
            {
                items.RemoveAt(slotIndex);
            }

            OnInventoryChanged?.Invoke();
            return used;
        }

        /// <summary>
        /// 아이템 보유 확인
        /// </summary>
        public bool HasItem(ItemData itemData, int quantity = 1)
        {
            if (itemData == null) return false;

            Item item = items.Find(i => i != null && i.itemData == itemData);
            return item != null && item.quantity >= quantity;
        }

        /// <summary>
        /// 빈 슬롯 개수
        /// </summary>
        public int GetEmptySlotCount()
        {
            return maxSlots - items.Count;
        }

        /// <summary>
        /// 인벤토리 초기화 (테스트용)
        /// </summary>
        public void ClearInventory()
        {
            items.Clear();
            OnInventoryChanged?.Invoke();
            Debug.Log("Inventory cleared");
        }
    }
}