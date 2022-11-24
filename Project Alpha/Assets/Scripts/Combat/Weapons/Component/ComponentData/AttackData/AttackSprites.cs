using System;
using UnityEngine;

namespace Combat.Weapons.Component.ComponentData.AttackData
{
    [Serializable]
    public class AttackSprites
    {
        [field: SerializeField] public Sprite[] Sprites { get; private set; }
    }
}