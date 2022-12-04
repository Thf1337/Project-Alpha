using UnityEngine;

namespace General.Interfaces
{
    public interface IKnockBackable
    {
        void KnockBack(KnockBackData data);
    }
    
    public struct KnockBackData
    {
        public Vector2 Angle;
        public float Strength;

        public int Direction;
        public GameObject Source;

        public KnockBackData(Vector2 angle, float strength, int facingDirection, GameObject source)
        {
            Angle = angle;
            Strength = strength;
            Direction = facingDirection;
            Source = source;
        }
    }
}