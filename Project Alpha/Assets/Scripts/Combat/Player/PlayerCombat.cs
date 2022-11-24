using System;
using System.Collections;
using Combat.Weapons;
using Controls;
using UnityEngine;

namespace Combat.Player
{
    public class PlayerCombat : MonoBehaviour
    {
        public float attackSpeed = 1.0f;
        
        private Weapon _weapon;
        private Movement _movement;
        private SpriteRenderer _spriteRenderer;

        private bool _isAttacking;

        private void Awake()
        {
            _weapon = transform.Find("Weapon").GetComponent<Weapon>();
            _weapon.OnExit += ExitHandler;
        }

        private void Start()
        {
            _movement = GetComponent<Movement>();
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
