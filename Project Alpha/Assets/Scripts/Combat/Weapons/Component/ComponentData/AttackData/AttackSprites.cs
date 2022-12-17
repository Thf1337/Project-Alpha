using System;
using UnityEngine;

namespace Combat.Weapons.Component.ComponentData.AttackData
{
    [Serializable]
    public class AttackSprites : AttackData
    {
        [field: SerializeField] public Sprite[] Sprites { get; private set; }
    }
}