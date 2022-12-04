using Combat.Weapons.Component.ComponentData;
using General.Interfaces;
using UnityEngine;

namespace Combat.Weapons.Component
{
    public class DamageOnHitBoxAction : WeaponComponent
    {
        private WeaponActionHitBox _actionHitBox;

        private WeaponDamageData _data;

        private void HandleDetected(Collider2D[] detected)
        {
            foreach (var item in detected)
            {
                var itemHealth = item.GetComponent<IDamagable>();

                itemHealth?.Damage(_data.damageAmount[Weapon.CurrentAttackCounter]);
            }
        }

        protected override void Awake()
        {
            base.Awake();
            
            _actionHitBox = GetComponent<WeaponActionHitBox>();

            _data = Weapon.Data.GetData<WeaponDamageData>();
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            
            _actionHitBox.OnDetected += HandleDetected;
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            
            _actionHitBox.OnDetected -= HandleDetected;
        }
    }
}