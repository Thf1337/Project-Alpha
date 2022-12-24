using System;
using UnityEngine;

namespace Combat.Projectiles.Component
{
    public class ProjectileMovement : ProjectileComponent<ProjectileMovementData>
    {
        private Rigidbody2D _rigidbody;

        protected override void Init()
        {
            Data = Projectile.Data.GetComponentData<ProjectileMovementData>();
            _rigidbody.velocity = Data.Velocity * transform.right;
        }

        private void FixedUpdate()
        {
            if (Data.ApplyContinuously)
            {
                _rigidbody.velocity = Data.Velocity * transform.right;
            }
        }

        protected override void Awake()
        {
            base.Awake();

            _rigidbody = GetComponent<Rigidbody2D>();
        }
    }
    
    public class ProjectileMovementData : ProjectileComponentData
    {
        public ProjectileMovementData()
        {
            ComponentDependencies.Add(typeof(ProjectileMovement));
        }

        public bool ApplyContinuously;
        public float Velocity;
    }
}