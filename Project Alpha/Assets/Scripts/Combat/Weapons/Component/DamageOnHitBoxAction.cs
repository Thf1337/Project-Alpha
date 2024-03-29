﻿using Combat.Player;
using Combat.Weapons.Component.ComponentData;
using Combat.Weapons.Component.ComponentData.AttackData;
using General.Interfaces;
using UnityEngine;

namespace Combat.Weapons.Component
{
    public class DamageOnHitBoxAction : WeaponComponent<WeaponDamageData, AttackDamage>
    {
        private WeaponActionHitBox _actionHitBox;

        private void HandleDetected(Collider2D[] detected)
        {
            CurrentAttackData.damageAmount += Combat.CalculateDamage();
            CurrentAttackData.source = Combat;
            
            foreach (var item in detected)
            {
                var itemHealth = item.GetComponent<IDamagable>();

                itemHealth?.Damage(CurrentAttackData);
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