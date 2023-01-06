using System;
using Controls;
using General.Interfaces;
using UnityEngine;

namespace Powerup.Effects
{
    [Serializable]
    public class SpeedBuff : PowerupComponent<SpeedBuffData>
    {
        private Movement _movement;
    
        public override void Apply(GameObject target)
        {
            _movement = target.GetComponent<Movement>();
        
            _movement.baseMoveSpeed += Data.addMoveSpeed;
            _movement.moveSpeedMultiplier += Data.addMoveSpeedMultiplier;
            
            base.Apply(target);
        }

        public override void Revert(GameObject target)
        {
            _movement.baseMoveSpeed -= Data.addMoveSpeed;
            _movement.moveSpeedMultiplier -= Data.addMoveSpeedMultiplier;
        }
    }

    public class SpeedBuffData : PowerupComponentData
    {
        public float addMoveSpeed;
        public float addMoveSpeedMultiplier;

        public SpeedBuffData()
        {
            ComponentDependencies.Add(typeof(SpeedBuff));
        }
    }
}
