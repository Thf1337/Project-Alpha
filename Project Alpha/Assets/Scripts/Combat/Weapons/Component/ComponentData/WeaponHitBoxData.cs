using System;
using UnityEngine;

namespace Combat.Weapons.Component.ComponentData
{
    public class WeaponHitBoxData : ComponentData
    {
        [field: SerializeField] public LayerMask DamageableLayers { get; private set; }
        [field: SerializeField] public Rect HitBox { get; private set; }
    }
}