using UnityEngine;

namespace Combat.Enemy
{
    public class EnemyHealth : Health
    {
        protected override void Start()
        {
            base.Start();
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
