using Combat.Weapons.Component.ComponentData;
using Combat.Weapons.Component.ComponentData.AttackData;
using UnityEngine;

namespace Combat.Weapons.Component
{
    public class OptionalAttachmentPoint : WeaponComponent<OptionalAttachmentPointData, AttachmentPoint>
    {
        
        private SpriteRenderer _optionalSpriteRenderer;
        
        protected override void Awake()
        {
            base.Awake();
      
            _optionalSpriteRenderer = transform.Find("Base/Optional Sprite")?.GetComponent<SpriteRenderer>();
            _optionalSpriteRenderer.enabled = false;
        }

        public override void SetReferences()
        {
            base.SetReferences();
      
            // _optionalSpriteRenderer = transform.Find("Base/Optional Sprite")?.GetComponent<SpriteRenderer>();
            // _optionalSpriteRenderer.enabled = false;
        }

        private void Enter()
        {
            if (CurrentAttackData.UseSprite)
            {
                _optionalSpriteRenderer.sprite = CurrentAttackData.Sprite;
            }
        }

        private void Exit()
        {
            _optionalSpriteRenderer.sprite = null;
        }

        private void EnableSpite() => _optionalSpriteRenderer.enabled = true;
        private void DisableSpite() => _optionalSpriteRenderer.enabled = false;

        protected override  void OnEnable()
        {
            base.OnEnable();

            EventHandler.OnEnableOptionalSprite += EnableSpite;
            EventHandler.OnDisableOptionalSprite += DisableSpite;
            Weapon.OnEnter += Enter;
            Weapon.OnExit += Exit;
        }

        protected override  void OnDisable()
        {
            base.OnDisable();
      
            EventHandler.OnEnableOptionalSprite -= EnableSpite;
            EventHandler.OnDisableOptionalSprite -= DisableSpite;
            Weapon.OnEnter -= Enter;
            Weapon.OnExit -= Exit;
        }
    }
}