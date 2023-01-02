using System;
using Controls;
using UnityEngine;

namespace Powerup.Effects
{
    [Serializable]
    public class DashBuff : PowerupComponent<DashBuffData>
    {
        private Movement _movement;
    
        public override void Apply(GameObject target)
        {
            _movement = target.GetComponent<Movement>();
        
            _movement.baseDashingVelocity += Data.addBaseDashingVelocity;
            _movement.dashingVelocityMultiplier += Data.addDashingVelocityMultiplier;
            _movement.baseDashingTime += Data.addBaseDashingTime;
            _movement.dashingTimeMultiplier += Data.addDashingTimeMultiplier;
        }

        public override void Revert(GameObject target)
        {
            _movement.baseDashingVelocity -= Data.addBaseDashingVelocity;
            _movement.dashingVelocityMultiplier -= Data.addDashingVelocityMultiplier;
            _movement.baseDashingTime -= Data.addBaseDashingTime;
            _movement.dashingTimeMultiplier -= Data.addDashingTimeMultiplier;
        }
    }

    public class DashBuffData : PowerupComponentData
    {
        public Vector2 addBaseDashingVelocity;
        public float addDashingVelocityMultiplier;
        public float addBaseDashingTime;
        public float addDashingTimeMultiplier;

        public DashBuffData()
        {
            ComponentDependencies.Add(typeof(DashBuff));
        }
    }
}