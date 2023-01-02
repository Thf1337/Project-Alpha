using Combat.Weapons.Component.ComponentData;
using Combat.Weapons.Component.ComponentData.AttackData;
using Controls;
using General.Interfaces;
using UnityEngine;

namespace Combat.Weapons.Component
{
    public class KnockBackOnHitBoxAction : WeaponComponent<WeaponKnockBackData, AttackKnockBack>
    {
        private WeaponActionHitBox _actionHitBox;

        private void HandleDetected(Collider2D[] detected)
        {
            foreach (var item in detected)
            {
                var itemKnockBack = item.GetComponent<IKnockBackable>();

                if (itemKnockBack == null) continue;

                var knockBackData = CurrentAttackData.KnockBackData;
                knockBackData.direction = Movement.facingDirection;
                    
                itemKnockBack.KnockBack(knockBackData);
            }
        }

        protected override void Awake()
        {
            base.Awake();
            
            _actionHitBox = GetComponent<WeaponActionHitBox>();
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