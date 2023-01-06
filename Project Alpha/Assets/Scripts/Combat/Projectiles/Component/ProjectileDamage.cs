using System;
using Combat.Weapons.Component.ComponentData.AttackData;
using General.Interfaces;
using General.Utilities;
using UnityEngine;

namespace Combat.Projectiles.Component
{
    public class ProjectileDamage : ProjectileComponent<ProjectileDamageData>, IProjectileCollisionEffect
    {
        private AttackDamage _damageData;

        public override void SetReferences()
        {
            base.SetReferences();
            
            Data = Projectile.Data.GetComponentData<ProjectileDamageData>();

            _damageData = new AttackDamage();
            _damageData.SetData(Projectile.Combat, Data.DamageAmount);

            OnEnable();
        }

        private void SetDamage()
        {
            _damageData.damageAmount = Data.DamageAmount + Projectile.BaseDamage;
            _damageData.source = Projectile.Combat;
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            
            Projectile.OnInit += SetDamage;
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            
            Projectile.OnInit -= SetDamage;
        }

        public bool CheckHit(RaycastHit2D hit)
        {
            if (!Projectile.CanDamage) return false;
            if (!LayerMaskUtilities.IsLayerInLayerMask(hit, Data.LayerMask)) return false;
            
            if (CombatUtilities.CheckIfDamageable(hit, _damageData, out _))
            {
                // Projectile.Disable();
                return true;
            }

            return false;
        }
    }

    public class ProjectileDamageData : ProjectileComponentData
    {
        public float DamageAmount;
        public LayerMask LayerMask;

        public ProjectileDamageData()
        {
            ComponentDependencies.Add(typeof(ProjectileDamage));
            ComponentDependencies.Add(typeof(CollisionEffect));
        }
    }
}