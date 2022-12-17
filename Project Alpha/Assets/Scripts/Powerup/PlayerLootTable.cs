using System;
using System.Collections.Generic;
using UnityEngine;

namespace Powerup
{
    public class PlayerLootTable : MonoBehaviour
    {
        public enum LootTableType { Chest }
        
        public Dictionary<int, LootTable> LootTables { get; private set; }

        private void Awake()
        {
            throw new NotImplementedException();
        }
    }
}