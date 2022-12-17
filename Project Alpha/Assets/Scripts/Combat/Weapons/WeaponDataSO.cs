using System.Collections.Generic;
using System.Linq;
using Combat.Weapons.Component.ComponentData;
using UnityEngine;

namespace Combat.Weapons
{
    [CreateAssetMenu(fileName = "newWeaponData", menuName = "Data/Weapon Data/Basic Weapon Data", order = 0)]
    public class WeaponDataSO : ScriptableObject
    {
        [field: SerializeField] public int NumberOfAttacks { get; private set; }
        
        [field: SerializeReference] public List<ComponentData> ComponentData { get; private set; }

        public T GetData<T>()
        {
            return ComponentData.OfType<T>().FirstOrDefault();
        }
        
        [ContextMenu("Add Sprite Data")]
        private void AddSpriteData() => ComponentData.Add(new WeaponSpriteData());
        
        [ContextMenu("Add Hit Box Data")]
        private void AddHitBoxData() => ComponentData.Add(new WeaponHitBoxData());
        
        [ContextMenu("Add Damage Data")]
        private void AddDamageData() => ComponentData.Add(new WeaponDamageData());
        
        [ContextMenu("Add Knock Back Data")]
        private void AddKnockBackData() => ComponentData.Add(new WeaponKnockBackData());
        
        [ContextMenu("Add Movement Data")]
        private void AddMovementData() => ComponentData.Add(new MovementData());
    }
}