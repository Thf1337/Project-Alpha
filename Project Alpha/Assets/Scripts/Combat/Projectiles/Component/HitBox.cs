using System;
using General.Interfaces;
using UnityEngine;

namespace Combat.Projectiles.Component
{
    public class HitBox : ProjectileComponent<HitboxData>, IHitBox
    {
        public event Action<RaycastHit2D[]> OnDetected;
        
        private RaycastHit2D[] _hits;

        private Rigidbody2D _rigidbody;

        private bool enabled;

        public override void SetReferences()
        {
            base.SetReferences();
            
            Data = Projectile.Data.GetComponentData<HitboxData>();
        }

        protected override void Init()
        {
            base.Init();

            if (!Data.DoInitialCheck) return;
            _hits = Physics2D.LinecastAll(transform.position, Projectile.SpawningWeaponPos, Data.LayerMask);

            if(_hits.Length > 0) CheckHits();
        }

        private void FixedUpdate()
        {
            var dist = Data.CompensateForVelocity ? _rigidbody.velocity.magnitude * Time.deltaTime : 0; 
            
            _hits = Physics2D.BoxCastAll(
                transform.position + (Vector3) Data.Hitbox.center,
                Data.Hitbox.size,
                transform.rotation.eulerAngles.z,
                transform.right,
                dist,
                Data.LayerMask
            );
            
            if(_hits.Length > 0) CheckHits();
        }

        private void CheckHits()
        {
            OnDetected?.Invoke(_hits);
        }

        protected override void Awake()
        {
            base.Awake();
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        private void OnDrawGizmos()
        {
            if(Data == null) return;
            
            Gizmos.DrawWireCube(
                transform.position + (Vector3) Data.Hitbox.center,
                Data.Hitbox.size);
        }
    }
    
    public class HitboxData : ProjectileComponentData
    {
        public Rect Hitbox;
        public bool CompensateForVelocity;
        public LayerMask LayerMask;
        public bool DoInitialCheck = true;
        
        public HitboxData()
        {
            ComponentDependencies.Add(typeof(HitBox));
        }
    }
}