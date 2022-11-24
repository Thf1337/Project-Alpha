using Combat;
using Combat.Player;
using UnityEngine;

namespace Powerup.Effects
{
    [CreateAssetMenu(menuName = "Powerups/HealthBuff")]
    public class HealthBuff : PowerupEffect
    {
        public float addMaxHealth;
        public float addMaxHealthMultiplier;
        public bool healHealth;
    
        public override void Apply(GameObject target)
        {
            target.GetComponent<PlayerHealth>().AddMaxHealth(addMaxHealth, addMaxHealthMultiplier, healHealth);
        }

        public override void Revert(GameObject target)
        { 
            target.GetComponent<PlayerHealth>().RemoveMaxHealth(addMaxHealth, addMaxHealthMultiplier);
        }
    }
}
