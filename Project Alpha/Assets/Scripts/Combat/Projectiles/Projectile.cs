﻿using System;
using Combat.Weapons;
using Combat.Weapons.Component.ComponentData;
using Controls;
using General.Utilities;
using UnityEngine;

namespace Combat.Projectiles
{
    public class Projectile : MonoBehaviour
    {
        [field: SerializeField] public ProjectileDataSO Data { get; private set; }

        public GameObject SpawningWeapon { get; private set; }
        public Combat Combat { get; private set; }
        public int FacingDirection { get; private set; }
        public float BaseDamage { get; private set; }
        public Vector3 SpawningWeaponPos { get; private set; }

        public event Action OnInit;

        public bool CanDamage { get; private set; }
        
        public bool CanHit { get; private set; }

        public event Action OnBeforeDisable;
        
        public Rigidbody2D Rigidbody { get; private set; }

        public void CreateProjectile(ProjectileDataSO data) 
        {
            Data = data;
            var comps = gameObject.AddDependenciesToGO<ProjectileComponent>(Data.GetAllDependencies());
            comps.ForEach(item => item.SetReferences());
        }
    
        public void Init(GameObject spawningEntity, int facingDirection, Combat combat)
        {
            SpawningWeapon = spawningEntity;
            SpawningWeaponPos = spawningEntity.transform.position;
            Combat = combat;
            BaseDamage = Combat.CalculateDamage();
            FacingDirection = facingDirection;
            SetCanDamage(true);
            SetCanHit(true);
            OnInit?.Invoke();
        }

        public void Disable()
        {
            OnBeforeDisable?.Invoke();
            Destroy(gameObject);
        }
        
        public void DisableHitBox()
        {
            SetCanHit(false);
        }
        
        private void Awake()
        {
            Rigidbody = GetComponent<Rigidbody2D>();
        }

        public void SetCanDamage(bool value) => CanDamage = value;

        public void SetCanHit(bool value) => CanHit = value;
    }
}