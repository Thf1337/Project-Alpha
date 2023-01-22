using UnityEngine;

namespace General.Finite_State_Machine
{
    [CreateAssetMenu(menuName = "AI/FSM/Transition")]
    public sealed class Transition : ScriptableObject
    {
        public Decision decision;
        public BaseState trueState;
        public BaseState falseState;

        public void Enter(BaseStateMachine stateMachine)
        {
            decision.Enter(stateMachine);
        }
 
        public void Execute(BaseStateMachine stateMachine)
        {
            if(decision.Decide(stateMachine) && !(trueState is RemainInState))
            {
                stateMachine.CurrentState.Exit(stateMachine);
                stateMachine.CurrentState = trueState;
                stateMachine.CurrentState.Enter(stateMachine);
            }
            else if (!(falseState is RemainInState))
            {
                stateMachine.CurrentState.Exit(stateMachine);
                stateMachine.CurrentState = falseState;
                stateMachine.CurrentState.Enter(stateMachine);
            }
        }
    }
}