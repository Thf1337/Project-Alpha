using System;
using Controls;
using General.Interfaces;
using UnityEngine;

namespace Powerup.Effects
{
    [Serializable]
    public class JumpBuff : PowerupComponent<JumpBuffData>
    {
        private Movement _movement;
        
        public override void SetReferences()
        {
            base.SetReferences();
            
            Data = Powerup.Data.GetComponentData<JumpBuffData>();
        }

        public override void Apply(GameObject target)
        {
            _movement = target.GetComponent<Movement>();
        
            _movement.baseJumpForce += Data.addJumpForce;
            _movement.jumpForceMultiplier += Data.addJumpForceMultiplier;
            _movement.baseJumpTime += Data.addJumpTime;
            _movement.jumpTimeMultiplier += Data.addJumpTimeMultiplier;
            _movement.jumpAmount += Data.addJumpAmount;
        
            _movement.ResetJumps();
            
            base.Apply(target);
        }

        public override void Revert(GameObject target)
        {
            _movement.baseJumpForce -= Data.addJumpForce;
            _movement.jumpForceMultiplier -= Data.addJumpForceMultiplier;
            _movement.baseJumpTime -= Data.addJumpTime;
            _movement.jumpTimeMultiplier -= Data.addJumpTimeMultiplier;
            _movement.jumpAmount -= Data.addJumpAmount;
        
            _movement.ResetJumps();
        }
    }

    public class JumpBuffData : PowerupComponentData
    {
        [Header("Base")]
        public float addJumpForce;
        public float addJumpTime;
        public int addJumpAmount;
    
        [Header("Multiplier")]
        public float addJumpForceMultiplier;
        public float addJumpTimeMultiplier;

        public JumpBuffData()
        {
            ComponentDependencies.Add(typeof(JumpBuff));
        }
    }
}