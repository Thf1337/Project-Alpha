using System;
using Combat.Weapons.Component.ComponentData;
using Combat.Weapons.Component.ComponentData.AttackData;
using Controls;
using UnityEngine;

namespace Combat.Weapons.Component
{
    public abstract class WeaponComponent : MonoBehaviour
    {
        protected Weapon Weapon;
        protected AnimationEventHandler EventHandler;
        protected Controls.Movement Movement => Weapon.movement;
        protected float AttackStartTime;

        protected bool IsAttackActive;

        protected virtual void SetStartTime() => AttackStartTime = Time.time; 
        
        protected virtual void Awake()
        {
            Weapon = GetComponent<Weapon>();

            EventHandler = GetComponentInChildren<AnimationEventHandler>();
        }

        protected virtual void HandleEnter()
        {
            IsAttackActive = true;
            SetStartTime();
        }

        protected virtual void HandleExit()
        {
            IsAttackActive = false;
        }

        protected virtual void OnEnable()
        {
            Weapon.OnEnter += HandleEnter;
            Weapon.OnExit += HandleExit;
        }

        protected virtual void OnDisable()
        {
            Weapon.OnEnter -= HandleEnter;
            Weapon.OnExit -= HandleExit;
        }

        public virtual void SetReferences()
        {
            
        }
    }

    public abstract class WeaponComponent<T1, T2> : WeaponComponent where T1 : ComponentData<T2> where T2: AttackData
    {
        protected T1 Data;
        protected T2 CurrentAttackData;

        protected override void HandleEnter()
        {
            base.HandleEnter();

            CurrentAttackData = Data.AttackData[Weapon.CurrentAttackCounter];
        }

        protected override void Awake()
        {
            base.Awake();

            Data = Weapon.Data.GetData<T1>();
        }
    }
}
