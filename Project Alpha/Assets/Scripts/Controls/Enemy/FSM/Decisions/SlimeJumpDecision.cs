using General.Finite_State_Machine;
using UnityEngine;

namespace Controls.Enemy.FSM.Decisions
{
    [CreateAssetMenu(menuName = "AI/FSM/Decisions/SlimeJumpDecision")]
    public class SlimeJumpDecision : Decision
    {
        private Transform _playerTransform;
        private Transform _slimeTransform;

        private static bool IsJumpUp(BaseStateMachine stateMachine)
        {
            return !stateMachine.Timers.ContainsKey("Jump");
        }

        public override void Enter(BaseStateMachine stateMachine)
        {
            _playerTransform = stateMachine.Player.transform;
            _slimeTransform = stateMachine.transform;
            
            
        }

        public override bool Decide(BaseStateMachine stateMachine)
        {
            return IsJumpUp(stateMachine) && !stateMachine.Movement.isJumping 
                                          && _playerTransform.position.y - 0.5f >= _slimeTransform.position.y;
        }
    }
}