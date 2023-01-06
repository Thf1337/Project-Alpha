using System;
using UnityEngine;

namespace Combat.Weapons.Component.ComponentData.AttackData
{
    [Serializable]
    public class AttackDamage : AttackData
    {
        [field: SerializeField] public float damageAmount;
        [field: SerializeField] public Combat source;

        public void SetData(Combat setSource, float setDamageAmount)
        {
            damageAmount = setDamageAmount;
            source = setSource;
        }
    }
}