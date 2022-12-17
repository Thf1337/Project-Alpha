using System;
using System.Collections.Generic;

namespace Combat.Projectiles
{
    [Serializable]
    public class ProjectileComponentData
    {
        private List<Type> _componentDependencies = new List<Type>();
        public List<Type> ComponentDependencies { get => _componentDependencies; protected set => _componentDependencies = value; }
    }
}