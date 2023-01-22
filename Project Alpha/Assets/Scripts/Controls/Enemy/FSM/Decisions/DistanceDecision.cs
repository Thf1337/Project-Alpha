using General.Finite_State_Machine;
using UnityEngine;

namespace Controls.Enemy.FSM.Decisions
{
    [CreateAssetMenu(menuName = "AI/FSM/Decisions/DistanceDecision")]
    public class DistanceDecision : Decision
    {
        private GameObject _target;
        public string targetTag;
        public float distanceThreshold = 3f;

        public override void Enter(BaseStateMachine stateMachine)
        {
            
        }

        public override bool Decide(BaseStateMachine stateMachine)
        {
            if (_target == null) _target = GameObject.FindWithTag(targetTag);
            return Vector3.Distance(stateMachine.transform.position, _target.transform.position) 
                   >= distanceThreshold;
        }
    }
}