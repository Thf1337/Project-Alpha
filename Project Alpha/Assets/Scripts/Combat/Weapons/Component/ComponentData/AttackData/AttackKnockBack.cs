using System;
using General.Interfaces;
using UnityEngine;

namespace Combat.Weapons.Component.ComponentData.AttackData
{
    [Serializable]
    public class AttackKnockBack : AttackData
    {
        [field: SerializeField] public KnockBackData KnockBackData;
    }
}