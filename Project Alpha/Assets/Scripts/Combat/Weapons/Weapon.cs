using System;
using System.Collections.Generic;
using Combat.Player;
using General.Interfaces;
using Powerup;
using UnityEngine;

namespace Combat.Weapons
{
    public class Weapon : Item
    {
        public event Action OnExit;
        public event Action OnEnter;
        public event Action<bool> OnInputChange;

        public GameObject BaseGameObject { get; private set; }  
        public GameObject WeaponSpriteGameObject { get; private set; }
        
        public bool IsFlipped { get; private set; }
        
        public Animator Animator { get; private set; }
        
        private bool _currentInput;
        
        public Controls.Movement movement;
        
        public PlayerCombat combat;

        public bool CurrentInput
        {
            get => _currentInput;
            private set
            {
                if (value != _currentInput)
                    OnInputChange?.Invoke(value);
                _currentInput = value;
            }
        }

        [field: SerializeField] public WeaponDataSO Data { get; private set; }
        [SerializeField] private float attackCounterResetCooldown;
        [SerializeField] private SpriteRenderer characterSpriteRenderer;

        public int CurrentAttackCounter
        {
            get => _currentAttackCounter;
            private set => _currentAttackCounter = value >= Data.NumberOfAttacks ? 0 : value;
        }
      
        private SpriteRenderer _spriteRenderer;
        private static readonly int Active = Animator.StringToHash("active");
        private static readonly int Counter = Animator.StringToHash("counter");

        private AnimationEventHandler _eventHandler;

        private int _currentAttackCounter;
        private General.Utilities.Timer _attackCounterResetTimer;

        private List<IDamagable> detectedDamageables = new List<IDamagable>();

        public void SetMovement(Controls.Movement movementComponent)
        {
            movement = movementComponent;
        }
        
        public void SetCombat(PlayerCombat combatComponent)
        {
            combat = combatComponent;
        }

        public void SetInput(bool input) => CurrentInput = input;

        public void EnterPrimary(float attackSpeed)
        {
            if (Animator.GetBool(Active))
            {
                Animator.SetBool(Active, false);
                OnExit?.Invoke();
                return;
            }
            
            // IsFlipped = characterSpriteRenderer.flipX;
            //
            // if (IsFlipped)
            // {
            //     _spriteRenderer.flipX = true;
            // }
            // else
            // {
            //     _spriteRenderer.flipX = false;
            // }
        
            movement.canMove = false;
            _attackCounterResetTimer.StopTimer();
            Animator.speed = attackSpeed;
            Animator.SetBool(Active, true);
            Animator.SetInteger(Counter, CurrentAttackCounter);
            
            OnEnter?.Invoke();
            OnInputChange?.Invoke(CurrentInput);
        }
    
        public void EnterSecondary()
        {
            print($"{transform.name} enter secondary");
        }

        private void Exit()
        {
            movement.canMove = true;
            Animator.SetBool(Active, false);
            CurrentAttackCounter++;
            _attackCounterResetTimer.StartTimer();
        
            OnExit?.Invoke();
        }
    
        private void Awake()
        {
            BaseGameObject = transform.Find("Base").gameObject;
            WeaponSpriteGameObject = transform.Find("Weapon Sprite").gameObject;
            
            _spriteRenderer = BaseGameObject.GetComponent<SpriteRenderer>();
            Animator = BaseGameObject.GetComponent<Animator>();
            _eventHandler = BaseGameObject.GetComponent<AnimationEventHandler>();
        
            _attackCounterResetTimer = new General.Utilities.Timer(attackCounterResetCooldown);
        }

        private void Update()
        {
            _attackCounterResetTimer.Tick();

            SetInput(Input.GetButton("WeaponPrimary"));

            if (movement.canFlip)
            {
                movement.CheckFlip();
            }
        }

        private void ResetAttackCounter() => CurrentAttackCounter = 0;

        private void EnableFlip()
        {
            movement.EnableFlip();
        }
        
        private void DisableFlip()
        {
            movement.DisableFlip();
        }

        private void OnEnable()
        {
            _eventHandler.OnFinish += Exit;
            _eventHandler.OnEnableFlip += EnableFlip;
            _eventHandler.OnDisableFlip += DisableFlip;
            _attackCounterResetTimer.OnTimerDone += ResetAttackCounter;
        }

        private void OnDisable()
        {
            _eventHandler.OnFinish -= Exit;
            _eventHandler.OnEnableFlip -= movement.EnableFlip;
            _eventHandler.OnDisableFlip -= movement.DisableFlip;
            _attackCounterResetTimer.OnTimerDone -= ResetAttackCounter;
        }

        public void AddToDetected(Collider2D collision)
        {
            IDamagable damageable = collision.GetComponent<IDamagable>();

            if (damageable != null)
            {
                detectedDamageables.Add(damageable);
            }
        }

        public void RemoveFromDetected(Collider2D collision)
        {
            IDamagable damageable = collision.GetComponent<IDamagable>();

            if (damageable != null)
            {
                detectedDamageables.Remove(damageable);
            }
        }
    }
    
    public enum WeaponBoolAnimParameters
    {
        active,
        hold,
        cancel,
    }
}
