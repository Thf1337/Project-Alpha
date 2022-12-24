namespace Combat.Projectiles.Component
{
    public class DisableDamageOnStick : ProjectileComponent<DisableDamageOnStickData>
    {
        private StickInEnvironment _stickInEnvironment;
        private bool _stickInEnvironmentFound;

        public override void SetReferences()
        {
            base.SetReferences();

            _stickInEnvironmentFound = TryGetComponent(out _stickInEnvironment);
            if (_stickInEnvironmentFound)
            {
                _stickInEnvironment.OnStick += HandleStick;
            }
        }

        private void HandleStick()
        {
            Projectile.SetCanDamage(false);
        }

        protected override void OnDisable()
        {
            base.OnDisable();

            if (_stickInEnvironmentFound) _stickInEnvironment.OnStick -= HandleStick;
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