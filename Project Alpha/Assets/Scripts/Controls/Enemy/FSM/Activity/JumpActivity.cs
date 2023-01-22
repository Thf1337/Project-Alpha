using General.Finite_State_Machine;
using UnityEngine;

namespace Controls.Enemy.FSM.Activity
{
    [CreateAssetMenu(menuName = "AI/FSM/Activity/JumpActivity")]
    public class JumpActivity : General.Finite_State_Machine.Activity
    {
        private GameObject _target;
        public string targetTag;
        public float speed = 1;
 
        public override void Enter(BaseStateMachine stateMachine)
        {
            _target = GameObject.FindWithTag(targetTag);
        }
 
        public override void Execute(BaseStateMachine stateMachine)
        {
            var movement = stateMachine.Movement;
            
            if (movement.canMove)
            {
                movement.Animator.SetInteger("currentState", (int) States.Jumping);
                var dir = _target.transform.position.x > stateMachine.transform.position.x ? 1 : -1;

                movement.SetVelocityX(dir * speed * 1/3);

                if(!movement.isJumping) movement.Jump(speed);
            }
        }
 
        public override void Exit(BaseStateMachine stateMachine)
        {
            
        }
    }
}