using System;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;

namespace Combat.Weapons.Component.ComponentData.AttackData
{
    [Serializable]
    public class AttackMovement : AttackData
    {
        [field: SerializeField] public Vector2 Direction { get; private set; }
        [field: SerializeField] public float Velocity { get; private set; }
    }
}