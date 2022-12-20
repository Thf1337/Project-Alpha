using System;
using System.Collections;
using Combat.Weapons;
using Controls;
using UnityEngine;

namespace Combat.Player
{
    public class PlayerCombat : MonoBehaviour
    {
        public float attackSpeed = 1.25f;
        public float baseDamage;
        
        private Weapon _weapon;
        private Movement _movement;
        private SpriteRenderer _spriteRenderer;

        private bool _isAttacking;

        private void Awake()
        {
            _weapon = transform.Find("Weapon").GetComponent<Weapon>();
            _movement = GetComponent<Movement>();
            _weapon.OnExit += ExitHandler;
            
            _weapon.SetMovement(_movement);
        }

        private void Start()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        private void EnterPrimary() => _weapon.EnterPrimary(attackSpeed);

        private void Update()
        {
            if (Input.GetButtonDown("WeaponPrimary") && !_isAttacking)
            {
                StartCoroutine(EnterWeapon(EnterPrimary));
            }
        }

        private IEnumerator EnterWeapon(Action weaponFunction)
        {
            yield return new WaitForEndOfFrame();
            
            _isAttacking = true;
            _movement.isAbilityDone = false;
            _spriteRenderer.enabled = false;
            //_weapon.SetInput(player.InputHandler.AttackInputsHold[(int) inputIndex]);
            weaponFunction?.Invoke();
        }

        private void ExitHandler()
        {
            _movement.isAbilityDone = true;
            _spriteRenderer.enabled = true;
            _isAttacking = false;
        }
    }
}
