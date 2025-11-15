using UnityEngine;
using System.Collections.Generic;
using System.Collections;

namespace EducationalRPG.Combat
{
    [System.Serializable]
    public class Buff
    {
        public string buffName;
        public float duration;
        public float attackMultiplier;
        public float defenseMultiplier;
        public float timeRemaining;

        public Buff(string name, float dur, float atkMult, float defMult)
        {
            buffName = name;
            duration = dur;
            attackMultiplier = atkMult;
            defenseMultiplier = defMult;
            timeRemaining = dur;
        }
    }

    public class BuffSystem : MonoBehaviour
    {
        [SerializeField] private List<Buff> activeBuffs = new List<Buff>();

        private void Update()
        {
            UpdateBuffs();
        }

        public void ApplyBuff(Inventory.Item item)
        {
            if (item == null) return;
            if (item.buffDuration > 0f)
            {
                Buff newBuff = new Buff(
                    item.itemName,
                    item.buffDuration,
                    item.attackBuffMultiplier,
                    item.defenseBuffMultiplier
                );

                activeBuffs.Add(newBuff);
                Debug.Log($"Applied buff: {item.itemName} for {item.buffDuration}s");
            }
        }

        private void UpdateBuffs()
        {
            for (int i = activeBuffs.Count - 1; i >= 0; i--)
            {
                activeBuffs[i].timeRemaining -= Time.deltaTime;

                if (activeBuffs[i].timeRemaining <= 0f)
                {
                    Debug.Log($"Buff expired: {activeBuffs[i].buffName}");
                    activeBuffs.RemoveAt(i);
                }
            }
        }

        public float GetTotalAttackMultiplier()
        {
            float total = 1f;
            foreach (var buff in activeBuffs)
            {
                total *= buff.attackMultiplier;
            }
            return total;
        }

        public float GetTotalDefenseMultiplier()
        {
            float total = 1f;
            foreach (var buff in activeBuffs)
            {
                total *= buff.defenseMultiplier;
            }
            return total;
        }
    }
}