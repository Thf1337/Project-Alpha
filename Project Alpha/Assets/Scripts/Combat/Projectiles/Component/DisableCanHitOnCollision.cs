namespace Combat.Projectiles.Component
{
    public class DisableCanHitOnCollision : ProjectileComponent<DisableCanHitOnCollisionData>
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
            Projectile.SetCanHit(false);
        }

        protected override void OnDisable()
        {
            base.OnDisable();

            if (_collisionEffectFound) _collisionEffect.OnCollision -= HandleCollision;
        }
    }
    
    public class DisableCanHitOnCollisionData : ProjectileComponentData
    {
        public DisableCanHitOnCollisionData()
        {
            ComponentDependencies.Add(typeof(DisableCanHitOnCollision));
        }
    }
}