using General.Finite_State_Machine;
using UnityEngine;

namespace Controls.Enemy.FSM.Decisions
{
    [CreateAssetMenu(menuName = "AI/FSM/Decisions/InLineOfSightDecision")]
    public class InLineOfSightDecision : Decision
    {
        public LayerMask layerMask;
        public float distanceThreshold = 3f; 
        private Vector3 _prevPosition = Vector3.zero;
        private Vector3 _prevDir = Vector3.zero;

        public override void Enter(BaseStateMachine stateMachine)
        {
            
        }

        public override bool Decide(BaseStateMachine stateMachine)
        {
            var dir = (stateMachine.transform.position - _prevPosition).normalized;
            dir = dir.Equals(Vector3.zero) ? _prevDir : dir;
            
            var position = stateMachine.transform.position;
            var hit = Physics2D.Raycast(position, dir, distanceThreshold, layerMask);
            
            _prevPosition = position;
            _prevDir = dir;
            
            return hit.collider != null;
        }
    }
}