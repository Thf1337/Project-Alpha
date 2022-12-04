using UI;
using UnityEngine;

namespace Combat.Player
{
    public class PlayerHealth : Health
    {
        [SerializeField] private HealthBar healthBar;

        protected override void Awake()
        {
            base.Awake();
        
            healthBar.SetMaxHealth(MaxHealth);
            healthBar.SetHealth(health);
        }

        public override void Heal(float heal)
        {
            base.Heal(heal);
            
            healthBar.SetHealth(health);
        }

        public override void Damage(float damage)
        {
            base.Damage(damage);
            
            healthBar.SetHealth(health);
        }

        public override void AddMaxHealth(float addMaxHealth, float addMaxHealthMultiplier, bool healHealth)
        {
            base.AddMaxHealth(addMaxHealth, addMaxHealthMultiplier, healHealth);
            
            healthBar.SetMaxHealth(MaxHealth);

            if (healHealth)
            {
                healthBar.SetHealth(health);
            }
        }

        public override void RemoveMaxHealth(float removeMaxHealth, float removeMaxHealthMultiplier)
        {
            base.RemoveMaxHealth(removeMaxHealth, removeMaxHealthMultiplier);
            
            healthBar.SetMaxHealth(MaxHealth);
            healthBar.SetHealth(health);
        }
    }
}
