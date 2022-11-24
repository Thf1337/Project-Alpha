using Combat.Weapons.Component.ComponentData;
using UnityEngine;

namespace Combat.Weapons.Component
{
    public class AttackMovement : WeaponComponent
    {

        private float _velocity;

        private bool _attackMovementActive;

        private bool _checkFlip;

        private bool _flipped;
        
        private AttackMovementData _data;
    

        private void StartMovement()
        {
            _velocity = _data.Velocity[Weapon.CurrentAttackCounter];

            Movement.canMove = false;
            _attackMovementActive = true;
            
            if (Movement.isDashing) return;
            
            Movement.SetVelocityX(_velocity * Movement.facingDirection);
        }

        private void EnableFlip()
        {
            _flipped = false;
            _checkFlip = true;
        }
        
        private void Update()
        {
            if (_checkFlip)
            {
                if (Movement.CheckFlip())
                {
                    _flipped = true;
                }
            }
        }

        private void FixedUpdate()
        {
            if (_attackMovementActive && !Movement.isDashing)
            {
                Movement.SetVelocityX(_velocity * Movement.facingDirection);
            }
        }

        private void StopMovement()
        {
            _velocity = 0f;
            Movement.SetVelocityX(0f);
            Movement.canMove = true;
            _attackMovementActive = false;
        }

        private void DisableFlip()
        {
            _checkFlip = false;
        }

        private void RevertFlip()
        {
            if (_flipped)
            {
                Movement.Flip();
            }
            
            _flipped = false;
        }

        protected override void Awake()
        {
            base.Awake();

            _data = Weapon.Data.GetData<AttackMovementData>();
        }

        protected override void OnEnable()
        {
            base.OnEnable();

            EventHandler.OnStartMovement += StartMovement;
            EventHandler.OnStopMovement += StopMovement;

            EventHandler.OnEnableFlip += EnableFlip;
            EventHandler.OnDisableFlip += DisableFlip;

            EventHandler.OnFinish += RevertFlip;
        }

        protected override void OnDisable()
        {
            base.OnDisable();

            EventHandler.OnStartMovement -= StartMovement;
            EventHandler.OnStopMovement -= StopMovement;

            EventHandler.OnEnableFlip -= EnableFlip;
            EventHandler.OnDisableFlip -= DisableFlip;

            EventHandler.OnFinish -= RevertFlip;
        }
    }
}