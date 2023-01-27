using General.Finite_State_Machine;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

namespace Controls.Enemy.FSM.Decisions
{
    [CreateAssetMenu(menuName = "AI/FSM/Decisions/TouchedDecision")]
    public class TouchedDecision : Decision
    {
        public LayerMask layerMask;
        
        public override void Enter(BaseStateMachine stateMachine)
        {
            
        }

        public override bool Decide(BaseStateMachine stateMachine)
        {
            return stateMachine.Collider.IsTouchingLayers(layerMask);
        }
    }
}