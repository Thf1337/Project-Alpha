using System;
using Combat.Weapons.Component.ComponentData.AttackData;
using General.Interfaces;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Environment
{
    public class Spike : MonoBehaviour
    {
        public AttackDamage attackDamage;
        public KnockBackData knockBackData;
        public LayerMask targetLayer;
        public Sprite[] sprites;

        private SpriteRenderer _spriteRenderer;

        private int _spriteIndex;
        
        private int GetNextSpriteIndex() {
            if (sprites.Length-1 != _spriteIndex)
            {
                _spriteIndex++;
            }

            return _spriteIndex;
        }

        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        private void HitPlayer(GameObject player)
        {
            var health = player.GetComponent<IDamagable>();
            health?.Damage(attackDamage);
            
            knockBackData.direction = Random.Range(0, 2) * 2 - 1;
            var knockBack = player.GetComponent<IKnockBackable>();
            knockBack?.KnockBack(knockBackData);

            _spriteRenderer.sprite = sprites[GetNextSpriteIndex()];
        }

        private void OnCollisionStay2D(Collision2D collision)
        {
            var collisionGameObject = collision.gameObject;
            
            if ((targetLayer & (1 << collision.gameObject.layer)) == 0)
            {
                return;
            }
            
            foreach (ContactPoint2D hitPosition in collision.contacts)
            {
                // Check if Player collides on top of the spike
                if (hitPosition.normal.y < 0)
                {
                    HitPlayer(collisionGameObject);
                    return;
                }
            }
        }
    }
}