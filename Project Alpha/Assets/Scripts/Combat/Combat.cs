using System;
using System.Collections;
using Combat.Weapons;
using Controls;
using General.Interfaces;
using UnityEngine;

namespace Combat
{
    public class Combat : MonoBehaviour, IKnockBackable
    {
        public float attackSpeed = 1.25f;
        public float baseDamage;
        public float damageMultiplier;
        [SerializeField] protected bool isKnockBackAble;

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
            
            isKnockBackAble = true;
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
        
        public void KnockBack(KnockBackData data)
        {
            if (!isKnockBackAble) return;
            
            data.angle.Normalize();
            Rigidbody.velocity = new Vector2(data.strength * data.angle.x * data.direction, data.strength * data.angle.y);
        }
    }
}