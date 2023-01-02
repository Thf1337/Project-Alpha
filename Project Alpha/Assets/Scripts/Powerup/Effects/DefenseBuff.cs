using System;
using Combat;
using Combat.Player;
using UnityEngine;

namespace Powerup.Effects
{
    [Serializable]
    public class DefenseBuff : PowerupComponent<DefenseBuffData>
    {
        private Health _health;
        
        public override void SetReferences()
        {
            base.SetReferences();
            
            Data = Powerup.Data.GetComponentData<DefenseBuffData>();
        }

        public override void Apply(GameObject target)
        {
            _health = target.GetComponent<PlayerHealth>();
            
            _health.AddDefense(Data.DefenseAmount);
            
            base.Apply(target);
        }

        public override void Revert(GameObject target)
        {
            _health.AddDefense(-Data.DefenseAmount);
        }
    
    }

    public class DefenseBuffData : PowerupComponentData
    {
        public float DefenseAmount;

        public DefenseBuffData()
        {
            ComponentDependencies.Add(typeof(DefenseBuff));
        }
    }
}