using Controls;
using UnityEngine;

namespace Powerup.Effects
{
    [CreateAssetMenu(menuName = "Powerups/JumpBuff")]
    public class JumpBuff : PowerupEffect
    {
        [Header("Base")]
        public float addJumpForce;
        public float addJumpTime;
        public int addJumpAmount;
    
        [Header("Multiplier")]
        public float addJumpForceMultiplier;
        public float addJumpTimeMultiplier;

        public override void Apply(GameObject target)
        {
            PlayerMovement playerMovement = target.GetComponent<PlayerMovement>();
        
            playerMovement.baseJumpForce += addJumpForce;
            playerMovement.jumpForceMultiplier += addJumpForceMultiplier;
            playerMovement.baseJumpTime += addJumpTime;
            playerMovement.jumpTimeMultiplier += addJumpTimeMultiplier;
            playerMovement.jumpAmount += addJumpAmount;
        
            playerMovement.ResetJumps();
        }

        public override void Revert(GameObject target)
        {
            PlayerMovement playerMovement = target.GetComponent<PlayerMovement>();

            playerMovement.baseJumpForce -= addJumpForce;
            playerMovement.jumpForceMultiplier -= addJumpForceMultiplier;
            playerMovement.baseJumpTime -= addJumpTime;
            playerMovement.jumpTimeMultiplier -= addJumpTimeMultiplier;
            playerMovement.jumpAmount -= addJumpAmount;
        
            playerMovement.ResetJumps();
        }
    }
}