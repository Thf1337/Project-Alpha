using Combat.Weapons.Component.ComponentData;
using Combat.Weapons.Component.ComponentData.AttackData;
using Combat.Weapons.Modifiers;
using UnityEngine;

namespace Combat.Weapons.Component
{
    public class WeaponDraw : WeaponComponent<WeaponDrawData, AttackDraw>
    {
        private WeaponModifiers weaponModifiers;
        private DrawModifier drawModifier = new DrawModifier();
        
    
        private void HandleInputChange(bool value)
        {
            if (!value)
            {
                var evaluatedValue = CurrentAttackData.Curve.Evaluate(Mathf.Clamp((Time.time - AttackStartTime) / CurrentAttackData.DrawTime, 0f, 1f));
                drawModifier.ModifierValue = evaluatedValue;
                weaponModifiers.AddModifier(drawModifier);
            }
        }

        protected override void Awake()
        {
            base.Awake();

            weaponModifiers = GetComponent<WeaponModifiers>();
        }

        public override void SetReferences()
        {
            base.SetReferences();

            weaponModifiers = GetComponent<WeaponModifiers>();
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