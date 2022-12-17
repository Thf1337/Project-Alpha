using System;
using UnityEngine;

namespace Combat.Weapons.Component.ComponentData
{
    [Serializable]
    public class ComponentData
    {
        
    }

    [Serializable]
    public abstract class ComponentData<T> : ComponentData where T : AttackData.AttackData
    {
        [field: SerializeField] public T[] AttackData { get; private set; }
    }
}