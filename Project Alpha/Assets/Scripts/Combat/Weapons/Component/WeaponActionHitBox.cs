using System;
using System.Collections.Generic;
using System.Linq;
using Combat.Weapons.Component.ComponentData;
using Controls;
using UnityEngine;

namespace Combat.Weapons.Component
{
    public class WeaponActionHitBox : WeaponComponent
    {
        public event Action<Collider2D[]> OnDetected;

        private Vector2 _offset;

        private WeaponHitBoxData _data;

        private Collider2D[] _detected;
        
        private List<int> _alreadyHitIDs;
        
        private void CheckHitbox()
        {
            // Set hitbox offset based on current position
            _offset.Set(
                transform.position.x + _data.HitBox.position.x * Movement.facingDirection,
                transform.position.y + _data.HitBox.y
            );

            // Look for colliders in the hitbox
            _detected = Physics2D.OverlapBoxAll(_offset, _data.HitBox.size, 0f, _data.DamageableLayers);

            if (_detected.Length == 0) return;

            _detected = _detected.Where(item =>
            {
                int id = item.GetInstanceID();

                if (!_alreadyHitIDs.Contains(id))
                {
                    _alreadyHitIDs.Add(id);
                    return true;
                }
                
                return false;
            }).ToArray();

            OnDetected?.Invoke(_detected);
        }

        private void ResetDetected()
        {
            _alreadyHitIDs = new List<int>();
        }
        
        protected override void Awake()
        {
            base.Awake();

            _offset = new Vector2();
            
            _alreadyHitIDs = new List<int>();

            _data = Weapon.Data.GetData<WeaponHitBoxData>();
        }
        
        protected override void OnEnable()
        {
            base.OnEnable();

            EventHandler.OnFinish += ResetDetected;
            EventHandler.OnAttackAction += CheckHitbox;
        }

        protected override void OnDisable()
        {
            base.OnDisable();

            EventHandler.OnFinish -= ResetDetected;
            EventHandler.OnAttackAction -= CheckHitbox;
        }

    }
}