using Combat.Weapons.Component.ComponentData.AttackData;
using UnityEngine;

namespace Combat.Weapons.Component.ComponentData
{
    public class WeaponSpriteData : ComponentData
    {
        [field: SerializeField] public AttackSprites[] AttackData { get; private set; }
    }
}