using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Powerup
{
    [CreateAssetMenu(fileName = "LootTable", menuName = "Loot Table")]
    public class LootTable : ScriptableObject
    {
        [SerializeField] private List<Item> items;
        
        [NonSerialized] private bool _isInitialized;

        private float _totalWeight;

        private void Initialize()
        {
            if (_isInitialized)
            {
                return;
            }
            
            _totalWeight = items.Sum(item => item.Weight);
            _isInitialized = true;
        }

        public Item GetRandomItem()
        {
            Initialize();

            float diceRoll = Random.Range(0f, _totalWeight);

            foreach (var item in items)
            {
                if (item.Weight >= diceRoll)
                {
                    return item;
                }

                diceRoll -= item.Weight;
            }

            throw new SystemException("Reward generation failed!");
        }
    }
}
