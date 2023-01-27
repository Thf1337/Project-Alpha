using General.Finite_State_Machine;
using UnityEngine;

namespace Controls.Enemy.FSM.Activity
{
    [CreateAssetMenu(menuName = "AI/FSM/Activity/BasicAnimationActivity")]
    public class BasicAnimationActivity : General.Finite_State_Machine.Activity
    {
        private enum State
        {
            Idle, Running, Jumping, Falling
        }

        private State _currentState;
        private Movement _movement;
        private Rigidbody2D _rigidbody;
        
        public override void Enter(BaseStateMachine stateMachine)
        {
            _movement = stateMachine.Movement;
            _rigidbody = stateMachine.Rigidbody;
            _currentState = State.Idle;
        }

        public override void Execute(BaseStateMachine stateMachine)
        {
            var facing = _movement.facingDirection;
            var dirX = stateMachine.dirX;

            if (dirX > 0f)
            {
                facing = 1;
                _currentState = State.Running;
            }
            else if (dirX < 0f)
            {
                facing = -1;
                _currentState = State.Running;
            }
            else
            {
                _currentState = State.Idle;
            }

            if (_rigidbody.velocity.y > .1f)
            {
                _currentState = State.Jumping;
            }
            else if (_rigidbody.velocity.y < -.1f)
            {
                _currentState = State.Falling;
            }

            if (facing != _movement.facingDirection)
            {
                _movement.Flip();
            }
            
            stateMachine.Animator.SetInteger("currentState", (int) _currentState);
        }

        public override void Exit(BaseStateMachine stateMachine)
        {
            _currentState = State.Idle;
        }
    }
}