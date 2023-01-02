using Combat;
using Combat.Weapons.Component.ComponentData.AttackData;
using General.Interfaces;
using UnityEngine;

namespace Environment
{
    public class Barrel : Health, IKnockBackable
    {
        [SerializeField] private UnityEngine.Object destructibleRef;

        private Rigidbody2D _rigidbody;

        protected override void Awake()
        {
            base.Awake();

            _rigidbody = GetComponent<Rigidbody2D>();
        }

        public override void Damage(AttackDamage attackDamage, bool bypassDamageReduction = false)
        {
            base.Damage(attackDamage, bypassDamageReduction);

            if (health <= 0)
            {
                Explode();
            }
        }

        private void Explode()
        {
            var destructible = (GameObject) Instantiate(destructibleRef);
            
            destructible.transform.position = transform.position;
            
            Destroy(gameObject);
        }
        
        public void KnockBack(KnockBackData data)
        {
            data.angle.Normalize();
            _rigidbody.velocity = new Vector2(data.strength * data.angle.x * data.direction, data.strength * data.angle.y);
        }
    }
}
