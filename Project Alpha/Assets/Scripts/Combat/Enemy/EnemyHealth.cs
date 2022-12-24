using Combat.Weapons.Component.ComponentData.AttackData;
using UnityEngine;

namespace Combat.Enemy
{
    public class EnemyHealth : Health
    {
        protected override void Awake()
        {
            base.Awake();
        }

        public override void Heal(float heal)
        {
            base.Heal(heal);
        }

        public override void Damage(AttackDamage attackDamage)
        {
            base.Damage(attackDamage);
            print($"{attackDamage.damageAmount} damage");
        }
    }
}
