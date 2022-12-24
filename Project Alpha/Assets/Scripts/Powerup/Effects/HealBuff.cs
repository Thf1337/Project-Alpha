using Combat;
using Combat.Player;
using Combat.Weapons.Component.ComponentData.AttackData;
using UnityEngine;

namespace Powerup.Effects
{
    [CreateAssetMenu(menuName = "Powerups/HealBuff")]
    public class HealBuff : PowerupEffect {
    
        public float healAmount;
        public float healPercentage;
    
        public override void Apply(GameObject target)
        {
            PlayerHealth playerHealth = target.GetComponent<PlayerHealth>();

            float heal = healAmount + playerHealth.GetMaxHealth() * healPercentage;
            playerHealth.Heal(heal);
        }

        public override void Revert(GameObject target)
        {
            PlayerHealth playerHealth = target.GetComponent<PlayerHealth>();

            float damage = healAmount + playerHealth.GetMaxHealth() * healPercentage;
            var attackDamage = new AttackDamage();
            attackDamage.SetData(null, damage);
            playerHealth.Damage(attackDamage);
        }
    
    }
}