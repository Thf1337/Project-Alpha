using General.Interfaces;
using UnityEngine;

namespace Combat.Enemy
{
    public class EnemyCombat : MonoBehaviour, IKnockBackable
    {
        private Rigidbody2D _rigidbody;
        
        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
        }
        
        public void KnockBack(KnockBackData data)
        {
            data.angle.Normalize();
            _rigidbody.velocity = new Vector2(data.strength * data.angle.x * data.direction, data.strength * data.angle.y);
        }
    }
}