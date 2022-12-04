using Combat;
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

        public override void Damage(float damage)
        {
            base.Damage(damage);

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
            data.Angle.Normalize();
            _rigidbody.velocity = new Vector2(data.Strength * data.Angle.x * data.Direction, data.Strength * data.Angle.y);
        }
    }
}
