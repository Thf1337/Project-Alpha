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

        public override void Damage(float damage)
        {
            base.Damage(damage);
            print($"{damage} damage");
        }
    }
}
