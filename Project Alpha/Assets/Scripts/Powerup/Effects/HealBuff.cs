using Combat;
using Combat.Player;
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
            playerHealth.Damage(damage);
        }
    
    }
}