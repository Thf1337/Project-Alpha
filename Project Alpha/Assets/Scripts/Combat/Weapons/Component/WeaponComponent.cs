using System;
using Controls;
using UnityEngine;

namespace Combat.Weapons.Component
{
    public abstract class WeaponComponent : MonoBehaviour
    {
        protected Weapon Weapon;
        protected Movement Movement;
        protected AnimationEventHandler EventHandler;

        protected bool IsAttackActive;

        protected virtual void Awake()
        {
            Weapon = GetComponent<Weapon>();
            
            Movement = gameObject.GetComponentInParent(typeof(Movement)) as Movement;

            EventHandler = transform.Find("Base").GetComponent<AnimationEventHandler>();
        }

        protected virtual void HandleEnter()
        {
            IsAttackActive = true;
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
    }
}
