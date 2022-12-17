using System;
using UnityEngine;

namespace Combat.Weapons.Component.ComponentData.AttackData
{
    [Serializable]
    public class AttackDamage : AttackData
    {
        [field: SerializeField] public float damageAmount;
        [field: SerializeField] public GameObject source;
    }
}