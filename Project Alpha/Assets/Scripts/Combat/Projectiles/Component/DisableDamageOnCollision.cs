namespace Combat.Projectiles.Component
{
    public class DisableDamageOnCollision : ProjectileComponent<DisableDamageOnCollisionData>
    {
        private CollisionEffect _collisionEffect;
        private bool _collisionEffectFound;

        public override void SetReferences()
        {
            base.SetReferences();

            _collisionEffectFound = TryGetComponent(out _collisionEffect);
            if (_collisionEffectFound)
            {
                _collisionEffect.OnCollision += HandleCollision;
            }
        }

        private void HandleCollision()
        {
            Projectile.SetCanDamage(false);
        }

        protected override void OnDisable()
        {
            base.OnDisable();

            if (_collisionEffectFound) _collisionEffect.OnCollision -= HandleCollision;
        }
    }
    
    public class DisableDamageOnCollisionData : ProjectileComponentData
    {
        public DisableDamageOnCollisionData()
        {
            ComponentDependencies.Add(typeof(DisableDamageOnCollision));
        }
    }
}