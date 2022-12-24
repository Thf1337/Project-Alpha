using System;
using UnityEngine;

namespace Combat.Weapons.Component.ComponentData.AttackData
{
    [Serializable]
    public class AttackSprites : AttackData
    {
        [field: SerializeField] public AttackPhase[] AttackPhases { get; private set; }
    }

    [Serializable]
    public class AttackPhase
    {
        [field: SerializeField] public Phase Phase { get; private set; }
        [field: SerializeField] public Sprite[] Sprites { get; private set; }
    }

    public enum Phase
    {
        Anticipation,
        Idle,
        Action,
        Cancel,
        Break,
        Parry
    }
}