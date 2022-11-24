using UnityEngine;

namespace Combat
{
    public class Health : MonoBehaviour, IDamagable
    {
        public float health;
        public float baseMaxHealth;
        public float maxHealthMultiplier;

        public bool invulnerable;
    
        protected float MaxHealth;
        
        protected virtual void Start()
        {
            MaxHealth = CalculateMaxHealth();
            health = MaxHealth;
        }

        private float CalculateMaxHealth()
        {
            return baseMaxHealth + baseMaxHealth * maxHealthMultiplier;
        }

        public float GetMaxHealth()
        {
            return MaxHealth;
        }
    
        public virtual void Heal(float heal)
        {
            health += heal;
        }

        public virtual void Damage(float damage)
        {
            if (invulnerable) return;
            
            health -= damage;
        }

        public virtual void AddMaxHealth(float addMaxHealth, float addMaxHealthMultiplier, bool healHealth) {
            baseMaxHealth += addMaxHealth;
            maxHealthMultiplier += addMaxHealthMultiplier;
            float difference = CalculateMaxHealth() - MaxHealth;
            MaxHealth += difference;

            if (healHealth)
            {
                Heal(difference);
            }
        }

        public virtual void RemoveMaxHealth(float removeMaxHealth, float removeMaxHealthMultiplier) {
            baseMaxHealth -= removeMaxHealth;
            maxHealthMultiplier -= removeMaxHealthMultiplier;
            MaxHealth = CalculateMaxHealth();

            if (health > MaxHealth)
            {
                health = MaxHealth;
            }
        }
    }
}