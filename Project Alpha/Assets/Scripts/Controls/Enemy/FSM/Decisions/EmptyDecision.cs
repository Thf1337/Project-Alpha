using General.Finite_State_Machine;
using UnityEngine;

namespace Controls.Enemy.FSM.Decisions
{
    [CreateAssetMenu(menuName = "AI/FSM/Decisions/EmptyDecision")]
    public class EmptyDecision : Decision
    {
        public override void Enter(BaseStateMachine stateMachine)
        {
            
        }

        public override bool Decide(BaseStateMachine stateMachine)
        {
            return true;
        }
    }
}