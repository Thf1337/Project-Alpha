using Combat.Weapons.Component.ComponentData;
using Combat.Weapons.Component.ComponentData.AttackData;
using UnityEngine;

namespace Combat.Weapons.Component
{
    public class Movement : WeaponComponent<MovementData, AttackMovement>
    {

        private Vector2 _direction;
        private float _velocity;

        private bool _attackMovementActive;

        private bool _checkFlip;

        private bool _flipped;


        private void StartMovement()
        {
            if (Movement.isDashing) return;
            
            _velocity = CurrentAttackData.Velocity;
            _direction = CurrentAttackData.Direction;

            Movement.canMove = false;
            _attackMovementActive = true;
            
            Movement.SetVelocity(_velocity, _direction, Movement.facingDirection);
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

        // private void FixedUpdate()
        // {
        //     if (_attackMovementActive && !Movement.isDashing)
        //     {
        //         Movement.SetVelocity(_velocity, _direction, Movement.facingDirection);
        //     }
        // }

        private void StopMovement()
        {
            _velocity = 0f;
            _direction = Vector2.zero;
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