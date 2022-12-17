using System;
using UnityEngine;

namespace Combat.Weapons.Component.ComponentData.AttackData
{
    [Serializable]
    public class AttackRanged : AttackData
    {
        public WeaponProjectileSpawnPoint[] AttackData;
    }
    
    
    [Serializable]
    public struct WeaponProjectileSpawnPoint
    {
        public Vector2 offset;
        public Vector2 direction;
        public ProjectileDataSO projectileData;
    }
}