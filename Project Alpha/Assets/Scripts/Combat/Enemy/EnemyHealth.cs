using System;
using Combat.Weapons.Component.ComponentData.AttackData;
using UnityEngine;

namespace Combat.Enemy
{
    public class EnemyHealth : Health
    {
        public event Action OnDeath;
        
        public override void Damage(AttackDamage attackDamage, bool bypassDamageReduction = false)
        {
            base.Damage(attackDamage, bypassDamageReduction);

            if (isDead)
            {
                OnDeath?.Invoke();
            }
        }

    }
}
