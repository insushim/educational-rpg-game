// Complete inventory management system with item slots, gold system, and event handling

using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    private List<Item> itemSlots;
    private int goldAmount;

    // Event handling
    public delegate void OnInventoryChange();
    public event OnInventoryChange onInventoryChange;

    private void Start()
    {
        itemSlots = new List<Item>();
        goldAmount = 0;
    }

    public void AddItem(Item item)
    {
        itemSlots.Add(item);
        onInventoryChange?.Invoke(); // Notify listeners
    }

    public void RemoveItem(Item item)
    {
        itemSlots.Remove(item);
        onInventoryChange?.Invoke(); // Notify listeners
    }

    public void AddGold(int amount)
    {
        goldAmount += amount;
        onInventoryChange?.Invoke(); // Notify listeners
    }

    public void SpendGold(int amount)
    {
        if (goldAmount >= amount)
        {
            goldAmount -= amount;
            onInventoryChange?.Invoke(); // Notify listeners
        }
    }
}