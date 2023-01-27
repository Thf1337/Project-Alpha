using General.Finite_State_Machine;
using General.Utilities;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

namespace Controls.Enemy.FSM.Activity
{
    [CreateAssetMenu(menuName = "AI/FSM/Activity/JumpActivity")]
    public class JumpActivity : General.Finite_State_Machine.Activity
    {
        private GameObject _target;
        public string targetTag;
        public float speedMultiplier = 0.3f;
        public float cooldown;

        private float _direction;
 
        public override void Enter(BaseStateMachine stateMachine)
        {
            if (targetTag == "Player")
            {
                _target = stateMachine.Player;
            }
            else
            {
                _target = GameObject.FindWithTag(targetTag);
            }

            stateMachine.dirX = 0f;
            _direction = _target.transform.position.x > stateMachine.transform.position.x ? 1 : -1;
            
            stateMachine.AddToTimers("Jump", cooldown);
        }
 
        public override void Execute(BaseStateMachine stateMachine)
        {
            var movement = stateMachine.Movement;
            
            movement.SetVelocityX(_direction * movement.MoveSpeed * speedMultiplier);
                
            if(!movement.isJumping)
            {
                movement.Jump(movement.JumpForce);
            }
            else
            {
                movement.CheckJump();
            }
        }
 
        public override void Exit(BaseStateMachine stateMachine)
        {
            
        }
    }
}