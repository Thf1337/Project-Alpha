using UnityEngine;

namespace Combat.Weapons.Component.ComponentData
{
    public class WeaponKnockBackData : ComponentData
    {
        [field: SerializeField] public Vector2[] KnockBackAngle { get; private set; }

        [field: SerializeField] public float[] KnockBackStrength { get; private set; }
    }
}