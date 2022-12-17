using System;
using UnityEngine;

namespace Combat.Weapons.Component.ComponentData.AttackData
{
    [Serializable]
    public class AttackHitBox : AttackData
    {
        [field: SerializeField] public LayerMask DamageableLayers { get; private set; }
        [field: SerializeField] public Rect HitBox { get; private set; }
    }
}