using General.Finite_State_Machine;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

namespace Controls.Enemy.FSM.Activity
{
    [CreateAssetMenu(menuName = "AI/FSM/Activity/PatrolActivity")]
    public class PatrolActivity : General.Finite_State_Machine.Activity
    {
        public float speed = 1;

        private PatrolPoints _patrolPoints;
        
        public override void Enter(BaseStateMachine stateMachine)
        {
            _patrolPoints = stateMachine.GetComponent<PatrolPoints>();
        }
        
        public override void Execute(BaseStateMachine stateMachine)
        {
            var movement = stateMachine.Movement;
            
            if (movement.canMove)
            {
                var x = _patrolPoints.GetTargetPointDirection();
                
                movement.CheckJump();
                if(!movement.isJumping)
                {
                    movement.SetVelocityX(speed * x);
                    movement.Animator.SetInteger("currentState", (int)States.Running);
                }

                if (movement.canFlip)
                {
                    if(movement.canFlip && x != movement.facingDirection) movement.Flip();
                }
            }
        }
        
        public override void Exit(BaseStateMachine stateMachine)
        {
            _patrolPoints.SetNextTargetPoint();
        }
    }
}