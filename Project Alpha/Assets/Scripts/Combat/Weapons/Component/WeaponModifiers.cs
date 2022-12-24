using System.Collections.Generic;
using System.Linq;
using Combat.Weapons.Component.ComponentData;
using Combat.Weapons.Component.ComponentData.AttackData;
using Combat.Weapons.Modifiers;
using UnityEngine;

namespace Combat.Weapons.Component
{
    public class WeaponModifiers : WeaponComponent
    {
        [SerializeReference] private List<AttackModifier> modifiers = new List<AttackModifier>();
        
        public List<AttackModifier> Modifiers { get => modifiers; private set => modifiers = value; }

        private RangedWeaponAttack _attackRanged;

        public bool TryGetModifier<T>(out T comp) where T : AttackModifier
        {
            comp = Modifiers.OfType<T>().FirstOrDefault();
            return comp != null;
        }

        public void AddModifier<T>(T value) where T : AttackModifier
        {
            if (Modifiers.OfType<T>().FirstOrDefault() == null)
            {
                Modifiers.Add(value);
            }
        }

        private void ResetModifiers() => Modifiers.Clear();
        private void SendModifiersToProjectiles(GameObject proj)
        {
            if (proj.TryGetComponent(out ProjectileModifiers comp))
            {
                comp.SetModifiers(Modifiers);
            }
        }

        protected override void Awake()
        {
            base.Awake();
            _attackRanged = GetComponent<RangedWeaponAttack>();
        }

        public override void SetReferences()
        {
            base.SetReferences();
            // _attackRanged = GetComponent<RangedWeaponAttack>();
        }

        protected override  void OnEnable()
        {
            base.OnEnable();
      
            Weapon.OnExit += ResetModifiers;
            
            if (_attackRanged)
            {
                _attackRanged.OnProjectileSpawned -= SendModifiersToProjectiles;
            }
        }

        protected override  void OnDisable()
        {
            base.OnDisable();
            
            Weapon.OnExit += ResetModifiers;
            
            if (_attackRanged)
            {
                _attackRanged.OnProjectileSpawned -= SendModifiersToProjectiles;
            }
        }
    }
}