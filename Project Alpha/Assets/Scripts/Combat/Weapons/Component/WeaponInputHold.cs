using Combat.Weapons.Component.ComponentData;
using UnityEngine;

namespace Combat.Weapons.Component
{
    public class WeaponInputHold : WeaponComponent
    {
        private bool _minHoldPassed;
        private bool _input;
        
        private void HandleInputChange(bool value)
        {
            _input = value;
            SetParams();
        }

        private void SetParams()
        {
            if (!_input && !_minHoldPassed) return;
            Weapon.Animator.SetBool(WeaponBoolAnimParameters.hold.ToString(), _input);
        }

        private void Enter()
        {
            _minHoldPassed = false;
        }

        private void MinHoldPassed()
        {
            _minHoldPassed = true;
            SetParams();
        }
        
        protected override void OnEnable()
        {
            base.OnEnable();

            Weapon.OnInputChange += HandleInputChange;
            Weapon.OnEnter += Enter;
            EventHandler.OnMinHold += MinHoldPassed;
        }

        protected override void OnDisable()
        {
            base.OnDisable();

            Weapon.OnInputChange -= HandleInputChange;
            Weapon.OnEnter -= Enter;
            EventHandler.OnMinHold -= MinHoldPassed;
        }
    }
}