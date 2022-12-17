using System;
using System.Collections.Generic;
using System.Linq;
using Combat.Projectiles;
using Combat.Weapons.Component.ComponentData;
using UnityEngine;

namespace Combat.Weapons
{
    [CreateAssetMenu(fileName = "newProjectileData", menuName = "Data/Projectile Data")]
    public class ProjectileDataSO : ScriptableObject
    {
        [field: SerializeField] public GameObject ProjectilePrefab { get; private set; }

        [field: SerializeField] public LayerMask InteractableLayers { get; private set; }

        [field: SerializeReference] public List<ProjectileComponentData> ComponentData { get; private set; }
        
        public T GetComponentData<T>() where T : ProjectileComponentData
        {
            return ComponentData.OfType<T>().FirstOrDefault();
        }
        
        public List<Type> GetAllDependencies()
        {
            List<Type> dependencies = new List<Type>();
            
            foreach (var item in ComponentData)
            {
                foreach (var dependency in item.ComponentDependencies)
                {
                    if (dependencies.FirstOrDefault(e => e.GetType() == dependency) == null)
                    {
                        dependencies.Add(dependency);
                    }
                }
            }

            return dependencies;
        }
    }
}