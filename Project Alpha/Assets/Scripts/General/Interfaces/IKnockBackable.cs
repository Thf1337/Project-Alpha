using System;
using Combat.Weapons.Component.ComponentData.AttackData;
using UnityEngine;

namespace General.Interfaces
{
    public interface IKnockBackable
    {
        void KnockBack(KnockBackData data);
    }
    
    [Serializable]
    public struct KnockBackData
    {
        public Vector2 angle;
        public float strength;

        public int direction;
        public GameObject source;

        public KnockBackData(Vector2 angle, float strength, int facingDirection, GameObject source)
        {
            this.angle = angle;
            this.strength = strength;
            direction = facingDirection;
            this.source = source;
        }
    }
}