using Combat.Weapons.Modifiers;

namespace Combat.Projectiles.Modifier
{
    public class ComponentModifier<T> : ProjectileComponent<T> where T: ProjectileComponentData
    {
        protected ProjectileModifiers Modifiers;
        protected bool IsActive;

        public override void SetReferences()
        {
            base.SetReferences();
      
            IsActive = TryGetComponent(out Modifiers);
        }
    }
}