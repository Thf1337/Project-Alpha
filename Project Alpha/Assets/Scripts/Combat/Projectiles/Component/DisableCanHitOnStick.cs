namespace Combat.Projectiles.Component
{
    public class DisableCanHitOnStick : ProjectileComponent<DisableCanHitOnStickData>
    {
        private StickInLayer _stickInLayer;
        private bool _stickInEnvironmentFound;

        public override void SetReferences()
        {
            base.SetReferences();

            _stickInEnvironmentFound = TryGetComponent(out _stickInLayer);
            if (_stickInEnvironmentFound)
            {
                _stickInLayer.OnStick += HandleStick;
            }
        }

        private void HandleStick()
        {
            Projectile.SetCanHit(false);
        }

        protected override void OnDisable()
        {
            base.OnDisable();

            if (_stickInEnvironmentFound) _stickInLayer.OnStick -= HandleStick;
        }
    }
    
    public class DisableCanHitOnStickData : ProjectileComponentData
    {
        public DisableCanHitOnStickData()
        {
            ComponentDependencies.Add(typeof(DisableCanHitOnStick));
        }
    }
}