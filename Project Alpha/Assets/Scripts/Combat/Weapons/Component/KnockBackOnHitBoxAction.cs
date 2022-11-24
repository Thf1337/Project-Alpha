using Combat.Weapons.Component.ComponentData;
using Controls;
using UnityEngine;

namespace Combat.Weapons.Component
{
    public class KnockBackOnHitBoxAction : WeaponComponent
    {
        private WeaponActionHitBox _actionHitBox;

        private WeaponKnockBackData _data;

        private void HandleDetected(Collider2D[] detected)
        {
            foreach (var item in detected)
            {
                var itemKnockBack = item.GetComponent<IKnockBackable>();

                if (itemKnockBack == null) continue;
                
                var knockBackData = new KnockBackData(_data.KnockBackAngle[Weapon.CurrentAttackCounter],
                    _data.KnockBackStrength[Weapon.CurrentAttackCounter], Movement.facingDirection, gameObject);
                    
                itemKnockBack.KnockBack(knockBackData);
            }
        }

        protected override void Awake()
        {
            base.Awake();
            
            _actionHitBox = GetComponent<WeaponActionHitBox>();

            _data = Weapon.Data.GetData<WeaponKnockBackData>();
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