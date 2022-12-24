using System;
using Combat.Weapons.Component.ComponentData.AttackData;
using General.Interfaces;
using General.Utilities;
using UnityEngine;

namespace Combat.Projectiles.Component
{
    public class ProjectileKnockBack : ProjectileComponent<ProjectileKnockBackData>
    {
        private IHitBox[] _hitboxes = Array.Empty<IHitBox>();
        
        public override void SetReferences()
        {
            base.SetReferences();
            
            _hitboxes = GetComponents<IHitBox>();
            Data = Projectile.Data.GetComponentData<ProjectileKnockBackData>();

            OnEnable();
        }

        private void CheckHits(RaycastHit2D[] hits)
        {
            foreach (var hit in hits)
            {
                if (!LayerMaskUtilities.IsLayerInLayerMask(hit, Data.LayerMask)) continue;
                if (CombatUtilities.CheckIfKnockBackable(hit, Data.KnockBackData, out _))
                {
                    // Projectile.Disable();
                }
            }
        }
        
        protected override void OnEnable()
        {
            base.OnEnable();
        
            foreach (var hitBox in _hitboxes)
            {
                hitBox.OnDetected += CheckHits;
            }
        }
        
        protected override void OnDisable()
        {
            base.OnDisable();
        
            foreach (var hitBox in _hitboxes)
            {
                hitBox.OnDetected -= CheckHits;
            }
        }
    }

    public class ProjectileKnockBackData : ProjectileComponentData
    {
        public LayerMask LayerMask;
        public KnockBackData KnockBackData;

        public ProjectileKnockBackData()
        {
            ComponentDependencies.Add(typeof(ProjectileKnockBack));
        }
    }
}