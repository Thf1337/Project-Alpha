using General.Finite_State_Machine;
using UnityEngine;

namespace Controls.Enemy.FSM.Decisions
{
    [CreateAssetMenu(menuName = "AI/FSM/Decisions/SlimeJumpDecision")]
    public class SlimeJumpDecision : Decision
    {
        private Transform _playerTransform;
        private Transform _slimeTransform;
        
        public override void Enter(BaseStateMachine stateMachine)
        {
            _playerTransform = stateMachine.Player.transform;
            _slimeTransform = stateMachine.transform;
        }

        public override bool Decide(BaseStateMachine stateMachine)
        {
            return !stateMachine.Movement.isJumping && _playerTransform.position.y > _slimeTransform.position.y;
        }
    }
}