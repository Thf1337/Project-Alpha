using General.Finite_State_Machine;
using UnityEngine;

namespace Controls.Enemy.FSM.Decisions
{
    [CreateAssetMenu(menuName = "AI/FSM/Decisions/WaitTimeDecision")]
    public class WaitTimeDecision : Decision
    {
        public float waitTime = 3f;
        private float _timer = 0;

        public override void Enter(BaseStateMachine stateMachine)
        {
            
        }

        public override bool Decide(BaseStateMachine stateMachine)
        {
            _timer += Time.deltaTime;
            if(_timer >= waitTime)
            {
                _timer = 0;
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}