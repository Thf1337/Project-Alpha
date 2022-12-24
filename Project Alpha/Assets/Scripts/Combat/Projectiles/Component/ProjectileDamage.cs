using System;
using Combat.Weapons.Component.ComponentData.AttackData;
using General.Interfaces;
using General.Utilities;
using UnityEngine;

namespace Combat.Projectiles.Component
{
    public class ProjectileDamage : ProjectileComponent<ProjectileDamageData>
    {
        private IHitBox[] _hitboxes = Array.Empty<IHitBox>();

        private AttackDamage _damageData;

        private StickInEnvironment _stickInEnvironment;

        public override void SetReferences()
        {
            base.SetReferences();

            _hitboxes = GetComponents<IHitBox>();
            _stickInEnvironment = GetComponent<StickInEnvironment>();

            Data = Projectile.Data.GetComponentData<ProjectileDamageData>();

            _damageData = new AttackDamage();
            _damageData.SetData(gameObject, Data.DamageAmount);

            OnEnable();
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

        private void CheckHits(RaycastHit2D[] hits)
        {
            if (!Projectile.CanDamage) return;
            foreach (var hit in hits)
            {
                if (!LayerMaskUtilities.IsLayerInLayerMask(hit, Data.LayerMask)) continue;
                if (CombatUtilities.CheckIfDamageable(hit, _damageData, out _))
                {
                    // Projectile.Disable();
                    _stickInEnvironment.TriggerOnStick(hit);
                }
            }
        }
    }

    public class ProjectileDamageData : ProjectileComponentData
    {
        public float DamageAmount;
        public LayerMask LayerMask;

        public ProjectileDamageData()
        {
            ComponentDependencies.Add(typeof(ProjectileDamage));
        }
    }
}