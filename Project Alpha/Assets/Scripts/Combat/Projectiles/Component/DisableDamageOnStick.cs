namespace Combat.Projectiles.Component
{
    public class DisableDamageOnStick : ProjectileComponent<DisableDamageOnStickData>
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
            Projectile.SetCanDamage(false);
        }

        protected override void OnDisable()
        {
            base.OnDisable();

            if (_stickInEnvironmentFound) _stickInLayer.OnStick -= HandleStick;
        }
    }
    
    public class DisableDamageOnStickData : ProjectileComponentData
    {
        public DisableDamageOnStickData()
        {
            ComponentDependencies.Add(typeof(DisableDamageOnStick));
        }
    }
}