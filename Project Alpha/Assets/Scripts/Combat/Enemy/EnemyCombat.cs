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
            data.Angle.Normalize();
            _rigidbody.velocity = new Vector2(data.Strength * data.Angle.x * data.Direction, data.Strength * data.Angle.y);
        }
    }
}