using System;
using UnityEngine;

namespace Combat.Weapons.Component.ComponentData.AttackData
{
    [Serializable]
    public class AttackKnockBack : AttackData
    {
        [field: SerializeField] public Vector2 KnockBackAngle { get; private set; }
        [field: SerializeField] public float KnockBackStrength { get; private set; }
    }
}