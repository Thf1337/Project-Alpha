using UnityEngine;

namespace Combat.Projectiles.Component
{
    public class RotateTowardsMovement : ProjectileComponent<RotateTowardsMovementData>
    {
        private Rigidbody2D _rigidbody;

        private void FixedUpdate()
        {
            if (_rigidbody.velocity == Vector2.zero) return;
            
            var velocity = _rigidbody.velocity;
            float angle = Mathf.Atan2(velocity.y, velocity.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }

        protected override void Awake()
        {
            base.Awake();
    
            _rigidbody = GetComponent<Rigidbody2D>();
        }
    }

    public class RotateTowardsMovementData : ProjectileComponentData
    {
        public RotateTowardsMovementData()
        {
            ComponentDependencies.Add(typeof(RotateTowardsMovement));
        }
    }
}