using System;
using System.Collections.Generic;
using UnityEngine;

namespace Combat.Projectiles
{
    [Serializable]
    public class ProjectileComponentData
    {
        private List<Type> _componentDependencies = new List<Type>();
        public List<Type> ComponentDependencies { get => _componentDependencies; protected set => _componentDependencies = value; }
        
        public ProjectileComponentData()
        {
            name = GetType().Name;
        }

        [SerializeField, HideInInspector]
        protected string name = "TEST";
    }
}