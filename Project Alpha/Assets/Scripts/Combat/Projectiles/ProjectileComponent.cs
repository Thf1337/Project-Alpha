﻿using Combat.Weapons.Component.ComponentData;
using UnityEngine;

namespace Combat.Projectiles
{
    public class ProjectileComponent : MonoBehaviour
    {
        private Projectile projectile;

        protected Projectile Projectile
        {
            get => projectile ? projectile : (projectile = GetComponent<Projectile>());
            private set => projectile = value;
        }
        
        protected virtual void Awake()
        {
            
        }
        
        public virtual void SetReferences()
        {
            Projectile = GetComponent<Projectile>();
        }

        protected virtual void Init()
        {
        }
        
        protected virtual void OnEnable()
        {
            Projectile.OnInit += Init;
        }

        protected virtual void OnDisable()
        {
            Projectile.OnInit -= Init;
        }
    }
    
    public class ProjectileComponent<T> : ProjectileComponent where T: ProjectileComponentData
    {
        protected T Data;

        public override void SetReferences()
        {
            base.SetReferences();
            
            Data = Projectile.Data.GetComponentData<T>();
        }

        protected override void OnEnable()
        {
            base.OnEnable();

            Data = Projectile.Data.GetComponentData<T>();
        }


    }
}