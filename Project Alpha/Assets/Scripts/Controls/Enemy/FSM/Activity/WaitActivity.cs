using General.Finite_State_Machine;
using UnityEngine;

namespace Controls.Enemy.FSM.Activity
{
    [CreateAssetMenu(menuName = "AI/FSM/Activity/WaitActivity")]
    public class WaitActivity : General.Finite_State_Machine.Activity
    {
        public override void Enter(BaseStateMachine stateMachine)
        {
            stateMachine.Rigidbody.velocity = Vector2.zero;
            stateMachine.Animator.SetInteger("currentState", (int) States.Idle);
        }
        
        public override void Execute(BaseStateMachine stateMachine) { }
        
        public override void Exit(BaseStateMachine stateMachine) { }
        
    }
}