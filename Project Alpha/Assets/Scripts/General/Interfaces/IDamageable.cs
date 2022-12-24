using Combat.Weapons.Component.ComponentData.AttackData;

namespace General.Interfaces
{
    public interface IDamagable
    {        
        public void Heal(float heal);

        public void Damage(AttackDamage attackDamage);
    }
}
