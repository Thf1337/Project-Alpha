using General.Finite_State_Machine;
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
            
            stateMachine.Movement.Animator.SetInteger("currentState", (int) States.Jumping);
            _direction = _target.transform.position.x > stateMachine.transform.position.x ? 1 : -1;
        }
 
        public override void Execute(BaseStateMachine stateMachine)
        {
            var movement = stateMachine.Movement;
            
            if (movement.canMove)
            {
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
        }
 
        public override void Exit(BaseStateMachine stateMachine)
        {
            
        }
    }
}