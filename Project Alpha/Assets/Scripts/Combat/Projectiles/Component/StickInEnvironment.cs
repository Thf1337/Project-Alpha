using System;
using General.Interfaces;
using General.Utilities;
using UnityEngine;
using UnityEngine.Tilemaps;
using Random = UnityEngine.Random;

namespace Combat.Projectiles.Component
{
    public class StickInEnvironment : ProjectileComponent<StickInEnvironmentData>
    {
        private Rigidbody2D _rigidbody;
        private SpriteRenderer _spriteRenderer;

        private bool _isStuck;

        private IHitBox[] _hitBoxes;

        public event Action OnStick;
        
        public override void SetReferences()
        {
            base.SetReferences();

            _hitBoxes = GetComponents<IHitBox>();

            Data = Projectile.Data.GetComponentData<StickInEnvironmentData>();
            
            foreach (var hitBox in _hitBoxes)
            {
                hitBox.OnDetected += CheckHits;
            }
        }

        public void TriggerOnStick(RaycastHit2D hit)
        {
            var position = transform.position;
            var xDiff = position.x - position.x;
            var yDiff = position.y - position.y;

            var stickPos = new Vector3(hit.point.x - xDiff, hit.point.y - yDiff, 0f);

            _rigidbody.velocity = Vector2.zero;
            _rigidbody.bodyType = RigidbodyType2D.Static;

            position = stickPos;
                
            Debug.DrawRay(stickPos, Vector3.left, Color.red, 5f);
                
            transform.position = position;

            var currentRot = transform.rotation.eulerAngles.z;
            var randomRot = Quaternion.Euler(0f, 0f, Random.Range(currentRot - 10f, currentRot + 10f));

            transform.rotation = randomRot;

            _isStuck = true;
                
            OnStick?.Invoke();
        }

        protected override void OnDisable()
        {
            base.OnDisable();

            foreach (var hitBox in _hitBoxes)
            {
                hitBox.OnDetected -= CheckHits;
            }
        }

        private void CheckHits(RaycastHit2D[] hits)
        {
            if (_isStuck) return;
            
            foreach (var hit in hits)
            {
                if (!LayerMaskUtilities.IsLayerInLayerMask(hit, Data.LayerMask)) continue;
                
                if (hit.collider.TryGetComponent(out TilemapRenderer hitSR))
                {
                    _spriteRenderer.sortingLayerID = hitSR.sortingLayerID;
                    _spriteRenderer.sortingOrder = -1;
                }

                TriggerOnStick(hit);
            }
        }

        protected override void Awake()
        {
            base.Awake();

            _rigidbody = GetComponent<Rigidbody2D>();
            _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        }
    }

    public class StickInEnvironmentData : ProjectileComponentData
    {
        public LayerMask LayerMask;

        public StickInEnvironmentData()
        {
            ComponentDependencies.Add(typeof(StickInEnvironment));
        }
    }
}