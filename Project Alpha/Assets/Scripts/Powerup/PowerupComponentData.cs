using System;
using System.Collections.Generic;
using UnityEngine;

namespace Powerup
{
    [Serializable]
    public class PowerupComponentData
    {
        [SerializeField] public float PowerupTime;
        
        private List<Type> _componentDependencies = new List<Type>();
        public List<Type> ComponentDependencies { get => _componentDependencies; protected set => _componentDependencies = value; }

        public PowerupComponentData()
        {
            name = GetType().Name;
        }

        [SerializeField, HideInInspector]
        protected string name = "TEST";
    }
}