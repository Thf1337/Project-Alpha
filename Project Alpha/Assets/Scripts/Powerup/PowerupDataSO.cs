using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using General.Interfaces;
using Powerup.Effects;
using Unity.VisualScripting.FullSerializer;
using UnityEditor;
using UnityEngine;
using UnityEngine.PlayerLoop;

namespace Powerup
{
    [CreateAssetMenu(fileName = "newPowerupData", menuName = "Data/Items/Powerups Data")]
    public class PowerupDataSO : ScriptableObject
    {
        [field: SerializeField] public GameObject PowerupPrefab { get; private set; }
        
        [field: SerializeField] public Sprite PowerupSprite { get; private set; }

        [field: SerializeField] public bool isPotion;
        
        [field: SerializeReference] public List<PowerupComponentData> ComponentData { get; private set; }
        
        public T GetComponentData<T>() where T : PowerupComponentData
        {
            return ComponentData.OfType<T>().FirstOrDefault();
        }
        
        public void SpawnPowerupAtTarget(Transform targetTransform, GameObject target = null)
        {
            var powerup = Instantiate(
                PowerupPrefab,
                targetTransform.position,
                new Quaternion(0, 0, 0, 1));
            
            var powerupScript = powerup.GetComponent<Powerup>();
            powerupScript.CreatePowerup(this);

            if(target)
            {
                powerupScript.applyDirectly = true;
                powerupScript.Interact(target);
            }
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
        
#if UNITY_EDITOR
        public void AddEffect<T>(T powerup) where T : PowerupComponentData
        {
            if (ComponentData.FirstOrDefault(item => item.GetType() == powerup.GetType()) != null) return;
            ComponentData.Add(powerup);
        }
#endif
        
    }
    
    [CustomEditor(typeof(PowerupDataSO))]
    public class PowerupDataEditor : Editor
    {
        public List<Type> powerupTypes = new List<Type>();

        public override void OnInspectorGUI()
        {
            PowerupDataSO data = target as PowerupDataSO;

            foreach (Type T in powerupTypes)
            {
                if (GUILayout.Button(T.Name))
                {
                    var dataType = Activator.CreateInstance(T);

                    if (dataType.GetType().IsSubclassOf(typeof(PowerupComponentData)))
                    {
                        data.AddEffect(dataType as PowerupComponentData);
                    }
                }
            }

            DrawDefaultInspector();
        }


        [ExecuteInEditMode]
        private void OnEnable()
        {
            var returned = GetAllComponentDatas();
            powerupTypes.Clear();

            returned.ToList().ForEach(item =>
            {
                powerupTypes.Add(item.GetType());
            });

        }

        private IEnumerable<PowerupComponentData> GetAllComponentDatas()
        {
            return AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(assembly => assembly.GetTypes())
                .Where(type => type.IsSubclassOf(typeof(PowerupComponentData)))
                .Select(type => Activator.CreateInstance(type) as PowerupComponentData);
        }
    }
}
