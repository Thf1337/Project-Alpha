using System;
using Combat.Player;
using UnityEngine;

namespace Powerup.Effects
{
    [Serializable]
    public class AttackBuff : PowerupComponent<AttackBuffData>
    {
        private PlayerCombat _combat;
    
        public override void Apply(GameObject target)
        {
            _combat = target.GetComponent<PlayerCombat>();
        
            _combat.baseDamage += Data.addBaseDamage;
            _combat.damageMultiplier += Data.addDamageMultiplier;
            _combat.attackSpeed += Data.addAttackSpeed;
            
            base.Apply(target);
        }

        public override void Revert(GameObject target)
        {
            _combat.baseDamage -= Data.addBaseDamage;
            _combat.damageMultiplier -= Data.addDamageMultiplier;
            _combat.attackSpeed -= Data.addAttackSpeed;
        }
    }

    public class AttackBuffData : PowerupComponentData
    {
        public float addBaseDamage;
        public float addDamageMultiplier;
        public float addAttackSpeed;

        public AttackBuffData()
        {
            ComponentDependencies.Add(typeof(AttackBuff));
        }
    }
}