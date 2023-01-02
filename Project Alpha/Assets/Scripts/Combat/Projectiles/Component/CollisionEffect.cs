using System;
using System.Collections.Generic;
using General.Interfaces;
using General.Utilities;
using Unity.VisualScripting;
using UnityEngine;

namespace Combat.Projectiles.Component
{
    public class CollisionEffect : ProjectileComponent<CollisionEffectData>
    {
        public event Action OnCollision;
        
        private IHitBox[] _hitboxes = Array.Empty<IHitBox>();

        private IProjectileCollisionEffect[] _effects;

        public override void SetReferences()
        {
            base.SetReferences();

            _hitboxes = GetComponents<IHitBox>();

            _effects = GetComponents<IProjectileCollisionEffect>();

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
            foreach (var hit in hits)
            {
                if (!Projectile.CanHit) return; 
                
                bool enemyHit = false;
                
                foreach (var effect in _effects)
                {
                    if (effect.CheckHit(hit))
                    {
                        enemyHit = true;
                    }
                }

                if (enemyHit)
                {
                    OnCollision?.Invoke();
                }
            }
        }
    }
    
    public class CollisionEffectData : ProjectileComponentData
    {
        public CollisionEffectData()
        {
            ComponentDependencies.Add(typeof(CollisionEffect));
        }
    }
}