using System;
using System.Collections.Generic;
using General.Interfaces;
using UnityEngine;

namespace Combat.Weapons
{
    public class Weapon : MonoBehaviour
    {
        public event Action OnExit;
        public event Action OnEnter;
        
        
        public GameObject BaseGameObject { get; private set; }  
        public GameObject WeaponSpriteGameObject { get; private set; }
        
        public bool IsFlipped { get; private set; }

        [field: SerializeField] public WeaponDataSO Data { get; private set; }
        [SerializeField] private float attackCounterResetCooldown;
        [SerializeField] private SpriteRenderer characterSpriteRenderer;

        public int CurrentAttackCounter
        {
            get => _currentAttackCounter;
            private set => _currentAttackCounter = value >= Data.NumberOfAttacks ? 0 : value;
        }
      
        private SpriteRenderer _spriteRenderer;
        private Animator _animator;
        private static readonly int Active = Animator.StringToHash("active");
        private static readonly int Counter = Animator.StringToHash("counter");

        private AnimationEventHandler _eventHandler;

        private int _currentAttackCounter;
        private General.Utilities.Timer _attackCounterResetTimer;

        private List<IDamagable> detectedDamageables = new List<IDamagable>();

        public void EnterPrimary(float attackSpeed)
        {
            if (_animator.GetBool(Active))
            {
                _animator.SetBool(Active, false);
                OnExit?.Invoke();
                return;
            }
            
            IsFlipped = characterSpriteRenderer.flipX;
            
            if (IsFlipped)
            {
                _spriteRenderer.flipX = true;
            }
            else
            {
                _spriteRenderer.flipX = false;
            }
        
            _attackCounterResetTimer.StopTimer();
            _animator.speed = attackSpeed;
            _animator.SetBool(Active, true);
            _animator.SetInteger(Counter, CurrentAttackCounter);
            
            OnEnter?.Invoke();
        }
    
        public void EnterSecondary()
        {
            print($"{transform.name} enter secondary");
        }

        private void Exit()
        {
            _animator.SetBool(Active, false);
            CurrentAttackCounter++;
            _attackCounterResetTimer.StartTimer();
        
            OnExit?.Invoke();
        }
    
        private void Awake()
        {
            BaseGameObject = transform.Find("Base").gameObject;
            WeaponSpriteGameObject = transform.Find("WeaponSprite").gameObject;
            
            _spriteRenderer = BaseGameObject.GetComponent<SpriteRenderer>();
            _animator = BaseGameObject.GetComponent<Animator>();
            _eventHandler = BaseGameObject.GetComponent<AnimationEventHandler>();
        
            _attackCounterResetTimer = new General.Utilities.Timer(attackCounterResetCooldown);
        }

        private void Update()
        {
            _attackCounterResetTimer.Tick();
        }

        private void ResetAttackCounter() => CurrentAttackCounter = 0;

        private void OnEnable()
        {
            _eventHandler.OnFinish += Exit;
            _attackCounterResetTimer.OnTimerDone += ResetAttackCounter;
        }

        private void OnDisable()
        {
            _eventHandler.OnFinish -= Exit;
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
}
