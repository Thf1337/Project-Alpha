using UnityEngine;

namespace Combat.Weapons.Component.ComponentData
{
    public class WeaponDamageData : ComponentData
    {
        [field: SerializeField] public float[] damageAmount;
        [field: SerializeField] public GameObject source;
    }
}