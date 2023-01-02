using System;
using Combat;
using Combat.Player;
using Combat.Weapons.Component.ComponentData.AttackData;
using UnityEngine;

namespace Powerup.Effects
{
    [Serializable]
    public class HealBuff : PowerupComponent<HealBuffData>
    {
        private Health _health;
        
        public override void SetReferences()
        {
            base.SetReferences();
            
            Data = Powerup.Data.GetComponentData<HealBuffData>();
        }

        public override void Apply(GameObject target)
        {
            _health = target.GetComponent<PlayerHealth>();

            float heal = Data.HealAmount + _health.GetMaxHealth() * Data.HealPercentage;
            _health.Heal(heal);
            
            base.Apply(target);
        }

        public override void Revert(GameObject target)
        {
            float damage = Data.HealAmount + _health.GetMaxHealth() * Data.HealPercentage;
            var attackDamage = new AttackDamage();
            attackDamage.SetData(null, damage);
            _health.Damage(attackDamage, true);
        }
    
    }

    public class HealBuffData : PowerupComponentData
    {
        public float HealAmount;
        public float HealPercentage;

        public HealBuffData()
        {
            ComponentDependencies.Add(typeof(HealBuff));
        }
    }
}