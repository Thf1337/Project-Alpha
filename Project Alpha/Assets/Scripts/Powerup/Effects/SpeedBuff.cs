using Controls;
using UnityEngine;

namespace Powerup.Effects
{
    [CreateAssetMenu(menuName = "Powerups/SpeedBuff")]
    public class SpeedBuff : PowerupEffect
    {
        public float addMoveSpeed;
        public float addMoveSpeedMultiplier;
    
        public override void Apply(GameObject target)
        {
            PlayerMovement playerMovement = target.GetComponent<PlayerMovement>();
        
            playerMovement.baseMoveSpeed += addMoveSpeed;
            playerMovement.moveSpeedMultiplier += addMoveSpeedMultiplier;
        }

        public override void Revert(GameObject target)
        {
            PlayerMovement playerMovement = target.GetComponent<PlayerMovement>();
        
            playerMovement.baseMoveSpeed -= addMoveSpeed;
            playerMovement.moveSpeedMultiplier -= addMoveSpeedMultiplier;
        }
    }
}
