using System;
using System.Collections;
using Combat.Weapons;
using Controls;
using General.Interfaces;
using UnityEngine;

namespace Combat
{
    public class Combat : MonoBehaviour
    {
        public float attackSpeed = 1.25f;
        public float baseDamage;
        public float damageMultiplier;

        public float totalDamageDealt;

        public event Action<float> OnDamageDealt;
        
        protected Movement Movement;
        protected Rigidbody2D Rigidbody;
        protected SpriteRenderer SpriteRenderer;

        protected bool IsAttacking;

        public void AddToTotalDamage(float damage)
        {
            totalDamageDealt += damage;
            OnDamageDealt?.Invoke(damage);
        }

        protected virtual void Awake()
        {
            Movement = GetComponent<Movement>();
            Rigidbody = GetComponent<Rigidbody2D>();
        }

        protected virtual void Start()
        {
            SpriteRenderer = GetComponent<SpriteRenderer>();
        }
        
        protected virtual IEnumerator EnterWeapon(Action weaponFunction)
        {
            yield return new WaitForEndOfFrame();
            
            IsAttacking = true;
            Movement.isAbilityDone = false;
            SpriteRenderer.enabled = false;
            //_weapon.SetInput(player.InputHandler.AttackInputsHold[(int) inputIndex]);
            weaponFunction?.Invoke();
        }

        protected virtual void ExitHandler()
        {
            Movement.isAbilityDone = true;
            SpriteRenderer.enabled = true;
            IsAttacking = false;
        }

        public virtual float CalculateDamage()
        {
            return baseDamage + baseDamage * damageMultiplier;
        }
    }
}