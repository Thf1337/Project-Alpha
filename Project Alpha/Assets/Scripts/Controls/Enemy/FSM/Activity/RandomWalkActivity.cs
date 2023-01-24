using General.Finite_State_Machine;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

namespace Controls.Enemy.FSM.Activity
{
    [CreateAssetMenu(menuName = "AI/FSM/Activity/RandomWalkActivity")]
    public class RandomWalkActivity : General.Finite_State_Machine.Activity
    {
        private int _direction;
 
        public override void Enter(BaseStateMachine stateMachine)
        {
            _direction = Random.Range(0, 2) * 2 - 1;

            var movement = stateMachine.Movement;
            if(movement.facingDirection != _direction)
            {
                movement.Flip();
            }
        }
 
        public override void Execute(BaseStateMachine stateMachine)
        {
            var movement = stateMachine.Movement;
            
            if (movement.canMove)
            {
                movement.Animator.SetInteger("currentState", (int) States.Running);

                movement.SetVelocityX(_direction * movement.MoveSpeed);
            }
        }
 
        public override void Exit(BaseStateMachine stateMachine)
        {
            
        }
    }
}