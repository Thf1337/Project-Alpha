using System;
using General.Interfaces;
using General.Utilities;
using UnityEngine;
using UnityEngine.Tilemaps;
using Random = UnityEngine.Random;

namespace Combat.Projectiles.Component
{
    public class StickInLayer : ProjectileComponent<StickInLayerData>, IProjectileCollisionEffect
    {
        private Rigidbody2D _rigidbody;
        private SpriteRenderer _spriteRenderer;

        private bool _isStuck;
        
        public event Action OnStick;
        
        public override void SetReferences()
        {
            base.SetReferences();

            Data = Projectile.Data.GetComponentData<StickInLayerData>();
        }

        public void TriggerOnStick(RaycastHit2D hit)
        {
            var position = transform.position;
            var xDiff = position.x - position.x;
            var yDiff = position.y - position.y;

            var stickPos = new Vector3(hit.point.x - xDiff, hit.point.y - yDiff, 0f);

            _rigidbody.velocity = Vector2.zero;
            _rigidbody.bodyType = RigidbodyType2D.Static;
            _rigidbody.isKinematic = true;
            
            transform.position = stickPos;

            var currentRot = transform.rotation.eulerAngles.z;
            var randomRot = Quaternion.Euler(0f, 0f, Random.Range(currentRot - 10f, currentRot + 10f));

            transform.rotation = randomRot;

            _isStuck = true;

            transform.parent = hit.transform;
                
            OnStick?.Invoke();
        }

        protected override void OnDisable()
        {
            base.OnDisable();
        }

        public bool CheckHit(RaycastHit2D hit)
        {
            if (_isStuck) return false;
            
            if (!LayerMaskUtilities.IsLayerInLayerMask(hit, Data.LayerMask)) return false;

            if (hit.collider.TryGetComponent(out TilemapRenderer hitSR))
            {
                _spriteRenderer.sortingLayerID = hitSR.sortingLayerID;
                _spriteRenderer.sortingOrder = -1;
            }

            TriggerOnStick(hit);

            return true;
        }

        protected override void Awake()
        {
            base.Awake();

            _rigidbody = GetComponent<Rigidbody2D>();
            _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        }
    }

    public class StickInLayerData : ProjectileComponentData
    {
        public LayerMask LayerMask;

        public StickInLayerData()
        {
            ComponentDependencies.Add(typeof(StickInLayer));
        }
    }
}