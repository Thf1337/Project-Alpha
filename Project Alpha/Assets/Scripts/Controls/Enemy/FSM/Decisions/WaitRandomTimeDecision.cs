using General.Finite_State_Machine;
using UnityEngine;

namespace Controls.Enemy.FSM.Decisions
{
    [CreateAssetMenu(menuName = "AI/FSM/Decisions/WaitRandomTimeDecision")]
    public class WaitRandomTimeDecision : Decision
    {
        public float waitTimeLower;
        public float waitTimeUpper;

        private float _waitTime;
        private float _timer = 0;

        public override void Enter(BaseStateMachine stateMachine)
        {
            _waitTime = Random.Range(waitTimeLower, waitTimeUpper);
        }

        public override bool Decide(BaseStateMachine stateMachine)
        {
            _timer += Time.deltaTime;
            if(_timer >= _waitTime)
            {
                _timer = 0;
                return true;
            }
            
            return false;
        }
    }
}