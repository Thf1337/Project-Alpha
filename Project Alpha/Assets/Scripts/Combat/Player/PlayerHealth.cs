using Combat.Weapons.Component.ComponentData.AttackData;
using Controls;
using UI;
using UnityEngine;

namespace Combat.Player
{
    public class PlayerHealth : Health
    {
        [SerializeField] private HealthBar healthBar;

        private Movement _playerMovement;
        private float _targetTime;

        private bool IsInvincible() => Time.time <= _targetTime;

        protected override void Awake()
        {
            base.Awake();

            _playerMovement = GetComponent<Movement>();

            healthBar.SetMaxHealth(MaxHealth);
            healthBar.SetHealth(health);
        }

        public override void Heal(float heal)
        {
            base.Heal(heal);
            
            healthBar.SetHealth(health);
        }

        public override void Damage(AttackDamage attackDamage, bool bypassDamageReduction = false)
        {
            if (IsInvincible())
            {
                return;
            }

            base.Damage(attackDamage, bypassDamageReduction);
            
            healthBar.SetHealth(health);

            _targetTime = Time.time + _playerMovement.invincibilityDuration;
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
