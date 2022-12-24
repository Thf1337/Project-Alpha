using System;
using System.Linq;
using Combat.Weapons.Component.ComponentData;
using Combat.Weapons.Component.ComponentData.AttackData;
using UnityEngine;

namespace Combat.Weapons.Component
{
    public class WeaponSprite : WeaponComponent<WeaponSpriteData, AttackSprites>
    {
        private SpriteRenderer _baseSpriteRenderer;
        private SpriteRenderer _weaponSpriteRenderer;

        private int _currentWeaponSpriteIndex;

        private Phase _currentAttackPhase = Phase.Anticipation;

        protected override void HandleEnter()
        {
            base.HandleEnter();
            
            _currentWeaponSpriteIndex = 0;
        }

        private void SetPhase(Phase phase)
        {
            _currentAttackPhase = phase;
            _currentWeaponSpriteIndex = 0;
        }
        
        private void HandleBaseSpriteChange(SpriteRenderer sr)
        {
            if (!IsAttackActive)
            {
                _weaponSpriteRenderer.sprite = null;
                return;
            }

            var attackPhaseSprites = CurrentAttackData.AttackPhases;
            var sprites = attackPhaseSprites.FirstOrDefault(attack => attack.Phase == _currentAttackPhase)
                ?.Sprites;

            if (sprites == null)
            {
                return;
            }

            if (_currentWeaponSpriteIndex >= sprites.Length)
            {
                _currentWeaponSpriteIndex = 0;
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
            
            _weaponSpriteRenderer.sprite = sprites[_currentWeaponSpriteIndex];
            
            _currentWeaponSpriteIndex++;
        }
        
        protected override void Awake()
        {
            base.Awake();

            _baseSpriteRenderer = transform.Find("Base").GetComponent<SpriteRenderer>();
            _weaponSpriteRenderer = transform.Find("Weapon Sprite").GetComponent<SpriteRenderer>();

            // TODO: Fix this when we create weapon data
            // _baseSpriteRenderer = Weapon.BaseGameObject.GetComponent<SpriteRenderer>();
            // _weaponSpriteRenderer = Weapon.WeaponSpriteGameObject.GetComponent<SpriteRenderer>();
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            
            _baseSpriteRenderer.RegisterSpriteChangeCallback(HandleBaseSpriteChange);
            
            Weapon.OnEnter += HandleEnter;
            EventHandler.OnEnterAttackPhase += SetPhase;
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            
            _baseSpriteRenderer.UnregisterSpriteChangeCallback(HandleBaseSpriteChange);
            
            Weapon.OnEnter -= HandleEnter;
            EventHandler.OnEnterAttackPhase -= SetPhase;
        }
    }
}
