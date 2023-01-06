using System;
using Combat.Weapons.Component.ComponentData.AttackData;
using General.Interfaces;
using UnityEngine;

namespace Combat
{
    public class Health : MonoBehaviour, IDamagable
    {
        public float health;
        public float baseMaxHealth;
        public float maxHealthMultiplier;
        
        public float defense;

        public bool invulnerable;
    
        protected float MaxHealth;
        
        protected virtual void Awake()
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

        public virtual void Damage(AttackDamage attackDamage, bool bypassDamageReduction = false)
        {
            if (invulnerable) return;

            var damage = attackDamage.damageAmount;

            if (!bypassDamageReduction)
            {
                damage *= CalculateDamageReduction();
            }

            health -= damage;

            attackDamage.source.AddToTotalDamage(damage);
            
            print($"{damage} damage");
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
    
        public virtual void AddDefense(float amount)
        {
            defense += amount;
        }

        public virtual float CalculateDamageReduction()
        {
            return (float) Math.Pow(0.05, 0.01 * defense);
        }
    }
}