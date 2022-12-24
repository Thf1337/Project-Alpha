using System;
using General.Interfaces;
using UnityEngine;

namespace Combat.Projectiles.Component
{
    public class HitLine : ProjectileComponent<HitLineData>, IHitBox
    {
        public event Action<RaycastHit2D[]> OnDetected;

        private RaycastHit2D[] _hits;

        protected override void Init()
        {
            base.Init();

            Data = Projectile.Data.GetComponentData<HitLineData>();

            if (!Data.DoInitialCheck) return;

            _hits = Physics2D.LinecastAll(transform.position, Projectile.SpawningEntityPos, Data.LayerMask);

            if (_hits.Length > 0) CheckHits();
        }

        private void FixedUpdate()
        {
            _hits = Physics2D.RaycastAll(
                transform.position,
                transform.right,
                Projectile.RB.velocity.magnitude * Time.deltaTime,
                Data.LayerMask
            );

            if (_hits.Length > 0) CheckHits();
        }

        private void CheckHits()
        {
            OnDetected?.Invoke(_hits);
        }
    }

    public class HitLineData : ProjectileComponentData
    {
        public LayerMask LayerMask;
        public bool DoInitialCheck = true;

        public HitLineData()
        {
            ComponentDependencies.Add(typeof(HitLine));
        }
    }
}