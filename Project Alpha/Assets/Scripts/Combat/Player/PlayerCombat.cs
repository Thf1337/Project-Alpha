using System;
using System.Collections;
using Combat.Weapons;
using Controls;
using General.Interfaces;
using UnityEngine;

namespace Combat.Player
{
    public class PlayerCombat : Combat
    {
        private Weapon _weapon;
        
        private void Update()
        {
            if (Input.GetButtonDown("WeaponPrimary") && !IsAttacking)
            {
                StartCoroutine(EnterWeapon(EnterPrimary));
            }
        }

        protected virtual void EnterPrimary() => _weapon.EnterPrimary(attackSpeed);

        protected override void Awake()
        {
            base.Awake();
            
            _weapon = transform.Find("Weapon").GetComponent<Weapon>();
            _weapon.OnExit += ExitHandler;
            
            _weapon.SetMovement(Movement);
            _weapon.SetCombat(this);
        }
    }
}
