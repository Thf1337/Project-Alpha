using Combat.Projectiles.Component;
using Combat.Weapons.Modifiers;

namespace Combat.Projectiles.Modifier
{
    public class GravityDelayDrawModifier : ComponentModifier<GravityDelayDrawModifierData>
    {
        private DelayedGravity _delayedGravity;
        private DrawModifier _modifier;

        public override void SetReferences()
        {
            base.SetReferences();

            if (TryGetComponent(out _delayedGravity))
            {
                _delayedGravity.OnSetDelay += ModifyDelay;
            }
        }

        private float ModifyDelay(float delay)
        {
            if (!IsActive) return delay;
            
            print(delay);

            if (Modifiers.TryGetModifier(out _modifier))
            {
                print(delay);
                print(_modifier.ModifierValue);
                return delay * _modifier.ModifierValue;
            }

            return delay;
        }

        protected override void OnEnable()
        {
            if (!_delayedGravity) return;
            _delayedGravity.OnSetDelay += ModifyDelay;
        }

        protected override void OnDisable()
        {
            if (!_delayedGravity) return;
            _delayedGravity.OnSetDelay -= ModifyDelay;
        }
    }
  
    public class GravityDelayDrawModifierData: ProjectileComponentData{
        public GravityDelayDrawModifierData()
        {
            ComponentDependencies.Add(typeof(GravityDelayDrawModifier));
        }
    }
}