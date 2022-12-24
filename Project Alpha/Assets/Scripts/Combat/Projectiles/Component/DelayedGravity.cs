using System;
using Combat.Weapons.Modifiers;
using General.Utilities;
using UnityEngine;

namespace Combat.Projectiles.Component
{
    
    public class DelayedGravity : ProjectileComponent<DelayedGravityData>
    {
        private Rigidbody2D _rigidbody;

        private TimerNotifier _timerNotifier;
        private DistanceNotifier _distanceNotifier;

        protected override void Init()
        {
            base.Init();

            Data = Projectile.Data.GetComponentData<DelayedGravityData>();

            SetDistanceDelay();
            SetTimeDelay();
        }

        private void SetTimeDelay()
        {
            switch (Data.TimeDelay)
            {
                case 0f:
                    _rigidbody.gravityScale = Data.Gravity;
                    break;
                case > 0f:
                    var delay = Data.TimeDelay;
                    var modDelay = OnSetDelay?.Invoke(delay);
                    _timerNotifier = new TimerNotifier(
                        modDelay != null ? (float) modDelay : delay,
                        false
                    );
                    _timerNotifier.OnTimerDone += () => _rigidbody.gravityScale = Data.Gravity;
                    break;
            }
        }

        public event Func<float, float> OnSetDelay;

        private void SetDistanceDelay()
        {
            switch (Data.DistanceDelay)
            {
                case 0f:
                    _rigidbody.gravityScale = Data.Gravity;
                    break;
                case > 0f:
                    var delay = Data.DistanceDelay;
                    var modDelay = OnSetDelay?.Invoke(delay);
                    _distanceNotifier = new DistanceNotifier(
                        transform.position,
                        modDelay != null ? (float) modDelay : delay,
                        false,
                        true);
                    _distanceNotifier.OnTarget += () => _rigidbody.gravityScale = Data.Gravity;
                    break;
            }
        }

        protected override void Awake()
        {
            base.Awake();

            _rigidbody = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            _timerNotifier?.Tick();
            _distanceNotifier?.Tick(transform.position);
        }
    }

    public class DelayedGravityData : ProjectileComponentData
    {
        public float Gravity = 4f;
        public float DistanceDelay = -1;
        public float TimeDelay = -1;

        public DelayedGravityData()
        {
            ComponentDependencies.Add(typeof(DelayedGravity));
            ComponentDependencies.Add(typeof(ProjectileModifiers));
        }
    }
}