using System;
using Combat.Weapons.Component.ComponentData;
using UnityEngine;

namespace Combat.Weapons.Component
{
    public class WeaponSprite : WeaponComponent
    {
        private SpriteRenderer _baseSpriteRenderer;
        private SpriteRenderer _weaponSpriteRenderer;

        private int _currentWeaponSpriteIndex;

        private WeaponSpriteData _data;

        protected override void HandleEnter()
        {
            base.HandleEnter();
            
            _currentWeaponSpriteIndex = 0;
        }
        
        private void HandleBaseSpriteChange(SpriteRenderer sr)
        {
            if (!IsAttackActive)
            {
                _weaponSpriteRenderer.sprite = null;
                return;
            }

            var currentAttackSprites = _data.AttackData[Weapon.CurrentAttackCounter].Sprites;

            if (_currentWeaponSpriteIndex >= currentAttackSprites.Length)
            {
                Debug.LogWarning($"{Weapon.name} weapon sprites length mismatch");
                return;
            }

            if (Weapon.IsFlipped)
            {
                _weaponSpriteRenderer.flipX = true;
            }
            else
            {
                _weaponSpriteRenderer.flipX = false;
            }
            
            _weaponSpriteRenderer.sprite = currentAttackSprites[_currentWeaponSpriteIndex];
            
            _currentWeaponSpriteIndex++;
        }
        
        protected override void Awake()
        {
            base.Awake();

            _baseSpriteRenderer = transform.Find("Base").GetComponent<SpriteRenderer>();
            _weaponSpriteRenderer = transform.Find("WeaponSprite").GetComponent<SpriteRenderer>();

            _data = Weapon.Data.GetData<WeaponSpriteData>();

            // TODO: Fix this when we create weapon data
            // _baseSpriteRenderer = Weapon.BaseGameObject.GetComponent<SpriteRenderer>();
            // _weaponSpriteRenderer = Weapon.WeaponSpriteGameObject.GetComponent<SpriteRenderer>();
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            
            _baseSpriteRenderer.RegisterSpriteChangeCallback(HandleBaseSpriteChange);
            
            Weapon.OnEnter += HandleEnter;
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            
            _baseSpriteRenderer.UnregisterSpriteChangeCallback(HandleBaseSpriteChange);
            
            Weapon.OnEnter -= HandleEnter;
        }
    }
}
