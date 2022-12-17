using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace General.Utilities
{
    public static class GameObjectUtilities
    {
        public static List<T> AddDependenciesToGO<T>(this GameObject gameObject, List<Type> dependencies)
            where T : Component
        {
            List<T> currentComps = gameObject.GetComponents<T>().ToList();
            List<T> addedComps = new List<T>();

            foreach (Type dependency in dependencies)
            {
                // See if dependency has already been added
                if (addedComps.FirstOrDefault(item => item.GetType() == dependency) != null) continue;

                // Check if dependency was already on GO
                var comp = currentComps.FirstOrDefault(item => item.GetType() == dependency);

                if (comp == null) // Component was not already on GO
                {
                    // Add comp to GO -- save ref to remove from lists
                    comp = (T)gameObject.AddComponent(dependency);
                }
                else
                {
                    // Remove comp from list as all comps in list will be destoryed later to remove uneeded comps
                    currentComps.Remove(comp);
                }

                addedComps.Add(comp);
            }
            
            return addedComps;
        }
    }
}