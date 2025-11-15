using System.Collections.Generic;
using UnityEngine;
using EducationalRPG.Inventory;

namespace EducationalRPG.Enemy
{
    [System.Serializable]
    public class LootEntry
    {
        public Item item;
        [Range(0f,1f)] public float dropChance = 0.1f; // 0-1
        public int minAmount = 1;
        public int maxAmount = 1;
        public int weight = 1; // for weighted rarity selection
    }

    [CreateAssetMenu(fileName = "LootTable", menuName = "Educational RPG/Enemy/Loot Table")]
    public class LootTable : ScriptableObject
    {
        public List<LootEntry> entries = new List<LootEntry>();

        // Returns list of items to drop based on chances
        public List<Item> RollDrops()
        {
            List<Item> drops = new List<Item>();
            foreach (var e in entries)
            {
                if (e.item == null) continue;
                if (Random.value <= e.dropChance)
                {
                    int amount = Random.Range(e.minAmount, e.maxAmount + 1);
                    for (int i = 0; i < amount; i++)
                        drops.Add(e.item);
                }
            }
            return drops;
        }

        // Weighted selection example (returns single rare drop)
        public Item RollWeightedRare()
        {
            int total = 0;
            foreach (var e in entries) total += e.weight;
            if (total <= 0) return null;
            int pick = Random.Range(0, total);
            int cum = 0;
            foreach (var e in entries)
            {
                cum += e.weight;
                if (pick < cum) return e.item;
            }
            return null;
        }
    }
}