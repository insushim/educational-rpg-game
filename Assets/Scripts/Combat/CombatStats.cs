using UnityEngine;
using UnityEngine.Events;

namespace EducationalRPG.Combat
{
    public class CombatStats : MonoBehaviour
    {
        [Header("Stats")]
        [SerializeField] private int maxHealth = 100;
        [SerializeField] private int currentHealth;
        [SerializeField] private int defense = 5;
        
        [Header("Events")]
        public UnityEvent<int, int> OnHealthChanged;
        public UnityEvent OnDeath;
        
        public bool IsDead { get; private set; }
        public int CurrentHealth => currentHealth;
        public int MaxHealth => maxHealth;

        private void Start()
        {
            currentHealth = maxHealth;
            OnHealthChanged?.Invoke(currentHealth, maxHealth);
        }

        public void TakeDamage(int damage)
        {
            if (IsDead) return;
            
            int actualDamage = Mathf.Max(1, damage - defense);
            currentHealth -= actualDamage;
            currentHealth = Mathf.Max(0, currentHealth);
            
            OnHealthChanged?.Invoke(currentHealth, maxHealth);
            
            if (currentHealth <= 0)
            {
                Die();
            }
            
            Debug.Log($"{gameObject.name} took {actualDamage} damage. HP: {currentHealth}/{maxHealth}");
        }

        public void Heal(int amount)
        {
            if (IsDead) return;
            
            currentHealth += amount;
            currentHealth = Mathf.Min(currentHealth, maxHealth);
            
            OnHealthChanged?.Invoke(currentHealth, maxHealth);
        }

        private void Die()
        {
            if (IsDead) return;
            
            IsDead = true;
            OnDeath?.Invoke();
            
            Debug.Log($"{gameObject.name} has died!");
        }

        public void Revive()
        {
            IsDead = false;
            currentHealth = maxHealth;
            OnHealthChanged?.Invoke(currentHealth, maxHealth);
        }
    }
}