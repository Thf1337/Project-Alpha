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
 
        public bool Execute(BaseStateMachine stateMachine, int index)
        {
            if(decision.Decide(stateMachine) && !(trueState is RemainInState))
            {
                stateMachine.currentStates[index].Exit(stateMachine);
                stateMachine.currentStates[index] = trueState;
                stateMachine.currentStates[index].Enter(stateMachine);
            }
            else if (!(falseState is RemainInState))
            {
                stateMachine.currentStates[index].Exit(stateMachine);
                stateMachine.currentStates[index] = falseState;
                stateMachine.currentStates[index].Enter(stateMachine);
            }
            else
            {
                return false;
            }

            return true;
        }
    }
}