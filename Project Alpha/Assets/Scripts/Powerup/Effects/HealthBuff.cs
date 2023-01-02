using Combat;
using Combat.Player;
using General.Interfaces;
using UnityEngine;

namespace Powerup.Effects
{
    public class HealthBuff : PowerupComponent<HealthBuffData>
    {
        private Health _health;
    
        public override void Apply(GameObject target)
        {
            _health = target.GetComponent<PlayerHealth>();
            _health.AddMaxHealth(Data.addMaxHealth, Data.addMaxHealthMultiplier, Data.healHealth);
            
            base.Apply(target);
        }

        public override void Revert(GameObject target)
        { 
            _health.RemoveMaxHealth(Data.addMaxHealth, Data.addMaxHealthMultiplier);
        }
    }

    public class HealthBuffData : PowerupComponentData
    {
        public float addMaxHealth;
        public float addMaxHealthMultiplier;
        public bool healHealth;

        public HealthBuffData()
        {
            ComponentDependencies.Add(typeof(HealthBuff));
        }
    }
}
