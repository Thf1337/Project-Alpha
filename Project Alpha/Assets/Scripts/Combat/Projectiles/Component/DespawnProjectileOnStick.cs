using General.Utilities;
using UnityEngine;

namespace Combat.Projectiles.Component
{
    public class DespawnProjectileOnStick : ProjectileComponent<DespawnProjectileOnStickData>
    {
        private StickInLayer _stickInLayer;
        private bool _stickInEnvironmentFound;

        private TimerNotifier _despawnTimer;

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
            _despawnTimer = new TimerNotifier(Data.DespawnTime, false);
            _despawnTimer.OnTimerDone += Despawn;
        }

        private void Despawn()
        {
            Projectile.Disable();
        }

        private void Update()
        {
            _despawnTimer?.Tick();
        }

        protected override void OnDisable()
        {
            base.OnDisable();

            if (_stickInEnvironmentFound) _stickInLayer.OnStick -= HandleStick;
            
            if(_despawnTimer != null) _despawnTimer.OnTimerDone -= Despawn;
        }
    }
    
    public class DespawnProjectileOnStickData : ProjectileComponentData
    {
        [field: SerializeField] public float DespawnTime { get; private set; }

        public DespawnProjectileOnStickData()
        {
            ComponentDependencies.Add(typeof(DespawnProjectileOnStick));
        }
    }
}