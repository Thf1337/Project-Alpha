using Combat.Weapons.Component.ComponentData;
using Combat.Weapons.Component.ComponentData.AttackData;
using Combat.Weapons.Modifiers;
using UnityEngine;

namespace Combat.Weapons.Component
{
    public class WeaponDraw : WeaponComponent<WeaponDrawData, AttackDraw>
    {
        private WeaponModifiers _weaponModifiers;
        private DrawModifier _drawModifier = new DrawModifier();
        
        private void HandleInputChange(bool value)
        {
            if (!value)
            {
                var evaluatedValue = CurrentAttackData.Curve.Evaluate(Mathf.Clamp((Time.time - AttackStartTime) / CurrentAttackData.DrawTime, 0f, 1f));
                _drawModifier.ModifierValue = evaluatedValue;
                _weaponModifiers.AddModifier(_drawModifier);
            }
        }

        protected override void Awake()
        {
            base.Awake();

            _weaponModifiers = GetComponent<WeaponModifiers>();
        }

        public override void SetReferences()
        {
            base.SetReferences();

            _weaponModifiers = GetComponent<WeaponModifiers>();
        }


        protected override void OnEnable()
        {
            base.OnEnable();
      
            Weapon.OnInputChange += HandleInputChange;
        }

        protected override  void OnDisable()
        {
            base.OnDisable();
      
            Weapon.OnInputChange -= HandleInputChange;
      
        }
        
    }
}