using System;
using Combat.Weapons.Component.ComponentData.AttackData;
using Controls;
using General.Interfaces;
using General.Utilities;
using UnityEngine;

namespace Combat.Projectiles.Component
{
    public class ProjectileKnockBack : ProjectileComponent<ProjectileKnockBackData>, IProjectileCollisionEffect
    {
        public override void SetReferences()
        {
            base.SetReferences();
            
            Data = Projectile.Data.GetComponentData<ProjectileKnockBackData>();

            OnEnable();
        }

        public bool CheckHit(RaycastHit2D hit)
        {
            if (!LayerMaskUtilities.IsLayerInLayerMask(hit, Data.LayerMask)) return false;
            
            if (CombatUtilities.CheckIfKnockBackable(hit, Data.KnockBackData, out _))
            {
                // Projectile.DisableHitBox();
                return true;
            }

            return false;
        }

        private void SetDirection()
        {
            Data.KnockBackData.direction = Projectile.FacingDirection;
        }
        
        protected override void OnEnable()
        {
            base.OnEnable();
            
            Projectile.OnInit += SetDirection;
        }
        
        protected override void OnDisable()
        {
            base.OnDisable();
            
            Projectile.OnInit -= SetDirection;
        }
    }

    public class ProjectileKnockBackData : ProjectileComponentData
    {
        public LayerMask LayerMask;
        public KnockBackData KnockBackData;

        public ProjectileKnockBackData()
        {
            ComponentDependencies.Add(typeof(ProjectileKnockBack));
            ComponentDependencies.Add(typeof(CollisionEffect));
        }
    }
}