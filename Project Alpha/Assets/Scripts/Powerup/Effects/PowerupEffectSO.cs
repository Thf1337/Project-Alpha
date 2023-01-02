using System;
using System.Collections.Generic;
using System.Linq;
using General.Interfaces;
using UnityEditor;
using UnityEngine;

namespace Powerup.Effects
{
    // [CreateAssetMenu(fileName = "newPowerupData", menuName = "Data/Items/Powerup Data")]
    public class PowerupEffectSO : ScriptableObject
    {
        [field: SerializeField] public GameObject PowerupPrefab { get; private set; }
        
        [field: SerializeField] public Sprite PowerupSprite { get; private set; }
        
        [field: SerializeReference] public List<PowerupDataSO> Powerups { get; private set; }
        
        
#if UNITY_EDITOR
        public void AddPowerup<T>(T powerup) where T : PowerupDataSO
        {
            if (Powerups.FirstOrDefault(item => item.GetType() == powerup.GetType()) != null) return;
            Powerups.Add(powerup);
        }
#endif
    }
    
    [CustomEditor(typeof(PowerupEffectSO))]
    public class PowerupEffectEditor : Editor
    {
        public List<Type> powerupTypes = new List<Type>();

        public override void OnInspectorGUI()
        {
            PowerupEffectSO data = target as PowerupEffectSO;

            foreach (Type T in powerupTypes)
            {
                if (GUILayout.Button(T.Name))
                {
                    var dataType = Activator.CreateInstance(T);

                    if (dataType.GetType().IsSubclassOf(typeof(PowerupDataSO)))
                    {
                        data.AddPowerup(dataType as PowerupDataSO);
                    }
                }
            }

            DrawDefaultInspector();
        }


        [ExecuteInEditMode]
        private void OnEnable()
        {
            var returned = GetAllProjectileComponentDatas();
            powerupTypes.Clear();

            returned.ToList().ForEach(item =>
            {
                powerupTypes.Add(item.GetType());
            });

        }

        private IEnumerable<PowerupDataSO> GetAllProjectileComponentDatas()
        {
            return AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(assembly => assembly.GetTypes())
                .Where(type => type.IsSubclassOf(typeof(PowerupDataSO)))
                .Select(type => Activator.CreateInstance(type) as PowerupDataSO);
        }
    }
}
