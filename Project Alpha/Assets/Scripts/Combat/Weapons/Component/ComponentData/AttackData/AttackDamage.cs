using System;
using UnityEngine;

namespace Combat.Weapons.Component.ComponentData.AttackData
{
    [Serializable]
    public class AttackDamage : AttackData
    {
        [field: SerializeField] public float damageAmount;
        [field: SerializeField] public GameObject source;

        public void SetData(GameObject setSource, float setDamageAmount)
        {
            damageAmount = setDamageAmount;
            source = setSource;
        }
    }
}